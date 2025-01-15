﻿using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RecordStore.Api.Controllers;
using RecordStore.Application.Extensions;
using RecordStore.Core.Interfaces.ServiceInterfaces;
using RecordStore.Core.Models;
using RecordStore.Shared.Dtos;

namespace RecordStore.Tests.ControllerTests
{
    public class AlbumsControllerTests
    {
        private Mock<IAlbumService> _albumService;
        private AlbumsController _albumController;

        [SetUp]
        public void SetUp()
        {
            _albumService = new Mock<IAlbumService>();
            _albumController = new AlbumsController(_albumService.Object);
        }

        [Test]
        public async Task GetAllAlbums_NoAlbums_ReturnsOkEmptyList()
        {
            // Arrange
            var expected = new List<Album>();
            _albumService.Setup(s => s.FindAllAlbumsAsync()).ReturnsAsync(expected);

            // Act
            var actual = await _albumController.GetAllAlbums();

            // Assert
            actual.Should().BeOfType<OkObjectResult>();
            var okObjectResult = actual as OkObjectResult;
            okObjectResult?.Value.Should().BeOfType<List<Album>>();
            var result = okObjectResult?.Value as List<Album>;
            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task GetAllAlbums_Albums_ReturnsOkExpectedList()
        {
            // Arrange
            var expected = new List<Album>()
            {
                new() { Id = 1, ArtistId = 1, GenreId = 1, ReleaseYear = DateTime.UtcNow.Year, Title = "TestAlbum1" },
                new() { Id = 2, ArtistId = 2, GenreId = 2, ReleaseYear = DateTime.UtcNow.AddYears(-1).Year, Title = "TestAlbum2" },
                new() { Id = 3, ArtistId = 3, GenreId = 3, ReleaseYear = DateTime.UtcNow.AddYears(-2).Year, Title = "TestAlbum3" },

            };
            _albumService.Setup(s => s.FindAllAlbumsAsync()).ReturnsAsync(expected);

            // Act
            var actual = await _albumController.GetAllAlbums();

            // Assert
            actual.Should().BeOfType<OkObjectResult>();
            var okObjectResult = actual as OkObjectResult;
            okObjectResult?.Value.Should().BeOfType<List<Album>>();
            var result = okObjectResult?.Value as List<Album>;
            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task GetAlbumById_DoesNotExist_ReturnsNotFound()
        {
            // Arrange
            var testId = 1;
            Album? testAlbum = null;
            var expectedErrorMessage = $"The album with id '{testId}' could not be found.";
            _albumService.Setup(s => s.FindAlbumByIdAsync(testId)).ReturnsAsync(testAlbum);

            // Act
            var actual = await _albumController.GetAlbumById(testId);

            // Assert
            actual.Should().BeOfType<NotFoundObjectResult>();
            var notFoundObjectResult = actual as NotFoundObjectResult;
            var result = notFoundObjectResult?.Value;
            result.Should().BeEquivalentTo(expectedErrorMessage);
        }

        [Test]
        public async Task GetAlbumById_Exists_ReturnsOkAlbum()
        {
            // Arrange
            var testId = 1;
            var testAlbum = new Album { Id = testId, ArtistId = 1, GenreId = 1, ReleaseYear = DateTime.UtcNow.Year, Title = "TestAlbum1" };
            _albumService.Setup(s => s.FindAlbumByIdAsync(testId)).ReturnsAsync(testAlbum);

            // Act
            var actual = await _albumController.GetAlbumById(testId);

            // Assert
            actual.Should().BeOfType<OkObjectResult>();
            var notFoundObjectResult = actual as OkObjectResult;
            notFoundObjectResult?.Value.Should().BeOfType<Album>();
            var result = notFoundObjectResult?.Value as Album;
            result.Should().BeEquivalentTo(testAlbum);
        }

        [Test]
        public async Task PostAlbum_ValidAlbum_ReturnsCreatedAt()
        {
            // Arrange
            var testId = 1;
            var testAlbumDto = new PostAlbumDto { ArtistId = 1, GenreId = 1, ReleaseYear = DateTime.UtcNow.Year, Title = "TestAlbum1" };
            _albumService
                .Setup(s => s.AddAlbumAsync(It.IsAny<PostAlbumDto>()))
                .ReturnsAsync((PostAlbumDto d) =>
                {
                    var album = d.ToAlbum();
                    album.Id = testId;
                    return album;
                });
            var expected = new Album { Id = testId, ArtistId = testAlbumDto.ArtistId, GenreId = testAlbumDto.GenreId, ReleaseYear = testAlbumDto.ReleaseYear, Title = testAlbumDto.Title };

            // Act
            var actual = await _albumController.PostAlbum(testAlbumDto);

            // Assert
            actual.Should().BeOfType<CreatedAtActionResult>();
            var createdAtActionResult = actual as CreatedAtActionResult;
            createdAtActionResult?.Value.Should().BeOfType<Album>();
            var result = createdAtActionResult?.Value as Album;
            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task PostAlbum_AlbumAlreadyExists_ReturnsConflictObjectResult()
        {
            // Arrange
            Album? expected = null;
            var testAlbumDto = new PostAlbumDto { ArtistId = 1, GenreId = 1, ReleaseYear = DateTime.UtcNow.Year, Title = "TestAlbum1" };
            _albumService.Setup(s => s.AddAlbumAsync(It.IsAny<PostAlbumDto>())).ReturnsAsync(expected);
            var expectedErrorMessage = $"Unable to add album. An Album with Title '{testAlbumDto.Title}', Artist Id '{testAlbumDto.ArtistId}' and Release Year '{testAlbumDto.ReleaseYear}' already exists.";

            // Act
            var actual = await _albumController.PostAlbum(testAlbumDto);

            // Assert
            actual.Should().BeOfType<ConflictObjectResult>();
            var conflictObjectResult = actual as ConflictObjectResult;
            var result = conflictObjectResult?.Value as string;
            result.Should().Be(expectedErrorMessage);
        }
    }
}
