using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RecordStore.Api.Controllers;
using RecordStore.Application.Extensions;
using RecordStore.Core.Interfaces.ServiceInterfaces;
using RecordStore.Core.Models;
using RecordStore.Shared.Dtos.AlbumDtos;

namespace RecordStore.Tests.ControllerTests
{
    public class AlbumsControllerTests
    {
        private Mock<IAlbumService> _mockAlbumService;
        private AlbumsController _albumController;

        [SetUp]
        public void SetUp()
        {
            _mockAlbumService = new Mock<IAlbumService>();
            _albumController = new AlbumsController(_mockAlbumService.Object);
        }

        [Test]
        public async Task GetAlbums_NoAlbums_ReturnsOkEmptyList()
        {
            // Arrange
            _mockAlbumService
                .Setup(s => s.FindAlbumsAsync(It.IsAny<AlbumFilterOptionsDto>()))
                .ReturnsAsync(() => []);

            // Act
            var actual = await _albumController.GetAllAlbums();

            // Assert
            actual.Should().BeOfType<OkObjectResult>();
            var okObjectResult = actual as OkObjectResult;
            okObjectResult?.Value.Should().BeOfType<List<AlbumResponseDto>>();
            var result = okObjectResult?.Value as List<AlbumResponseDto>;
            result.Should().BeEmpty();
        }

        [Test]
        public async Task GetAlbums_Albums_ReturnsOkExpectedList()
        {
            // Arrange
            var existingAlbums = new List<Album>()
            {
                new() { Id = 1, ArtistId = 1, GenreId = 1, ReleaseYear = DateTime.UtcNow.Year, Title = "TestAlbum1", Artist = new() { Name = "TestArtist1" } },
                new() { Id = 2, ArtistId = 2, GenreId = 2, ReleaseYear = DateTime.UtcNow.AddYears(-1).Year, Title = "TestAlbum2", Artist = new() { Name = "TestArtist2" } },
                new() { Id = 3, ArtistId = 3, GenreId = 3, ReleaseYear = DateTime.UtcNow.AddYears(-2).Year, Title = "TestAlbum3", Artist = new() { Name = "TestArtist3" } },
            };

            var expected = existingAlbums.Select(a => a.ToAlbumResponseDto()).ToList();

            _mockAlbumService
                .Setup(s => s.FindAlbumsAsync(It.IsAny<AlbumFilterOptionsDto>()))
                .ReturnsAsync(expected);

            // Act
            var actual = await _albumController.GetAllAlbums();

            // Assert
            actual.Should().BeOfType<OkObjectResult>();
            var okObjectResult = actual as OkObjectResult;
            okObjectResult?.Value.Should().BeOfType<List<AlbumResponseDto>>();
            var result = okObjectResult?.Value as List<AlbumResponseDto>;
            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task GetAlbumById_DoesNotExist_ReturnsNotFound()
        {
            // Arrange
            var testId = 1;
            var expectedErrorMessage = $"The album with id '{testId}' could not be found.";

            _mockAlbumService
                .Setup(s => s.FindAlbumByIdAsync(testId))
                .ReturnsAsync((int _) => null);

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
            var testAlbum = new Album { Id = testId, ArtistId = 1, GenreId = 1, ReleaseYear = DateTime.UtcNow.Year, Title = "TestAlbum1", Artist = new() { Name = "TestArtist1" } };
            var expected = testAlbum.ToAlbumResponseDto();

            _mockAlbumService
                .Setup(s => s.FindAlbumByIdAsync(testId))
                .ReturnsAsync(expected);

            // Act
            var actual = await _albumController.GetAlbumById(testId);

            // Assert
            actual.Should().BeOfType<OkObjectResult>();
            var notFoundObjectResult = actual as OkObjectResult;
            notFoundObjectResult?.Value.Should().BeOfType<AlbumResponseDto>();
            var result = notFoundObjectResult?.Value as AlbumResponseDto;
            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task PostAlbum_ValidAlbum_ReturnsCreatedAt()
        {
            // Arrange
            var testId = 1;
            var testAlbumDto = new PostAlbumDto { ArtistId = 1, GenreId = 1, ReleaseYear = DateTime.UtcNow.Year, Title = "TestAlbum1" };
            var testAlbum = testAlbumDto.ToAlbum();
            testAlbum.ArtistId = testId;
            testAlbum.Artist = new() { Name = "TestArtist1" };
            var expected = testAlbum.ToAlbumResponseDto();

            _mockAlbumService
                .Setup(s => s.AddAlbumAsync(It.IsAny<PostAlbumDto>()))
                .ReturnsAsync(expected);

            // Act
            var actual = await _albumController.PostAlbum(testAlbumDto);

            // Assert
            actual.Should().BeOfType<CreatedAtActionResult>();
            var createdAtActionResult = actual as CreatedAtActionResult;
            createdAtActionResult?.Value.Should().BeOfType<AlbumResponseDto>();
            var result = createdAtActionResult?.Value as AlbumResponseDto;
            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task PostAlbum_AlbumAlreadyExists_ReturnsConflictObjectResult()
        {
            // Arrange
            var testAlbumDto = new PostAlbumDto { ArtistId = 1, GenreId = 1, ReleaseYear = DateTime.UtcNow.Year, Title = "TestAlbum1" };
            var expectedErrorMessage = $"Unable to add album. An Album with Title '{testAlbumDto.Title}', Artist Id '{testAlbumDto.ArtistId}' and Release Year '{testAlbumDto.ReleaseYear}' already exists.";

            _mockAlbumService
                .Setup(s => s.AddAlbumAsync(It.IsAny<PostAlbumDto>()))
                .ReturnsAsync((PostAlbumDto _) => null);

            // Act
            var actual = await _albumController.PostAlbum(testAlbumDto);

            // Assert
            actual.Should().BeOfType<ConflictObjectResult>();
            var conflictObjectResult = actual as ConflictObjectResult;
            var result = conflictObjectResult?.Value as string;
            result.Should().Be(expectedErrorMessage);
        }

        [Test]
        public async Task PutAlbum_NoPropertiesOnDto_ReturnsBadRequest()
        {
            // Arrange
            var testId = 1;
            var testAlbumDto = new PutAlbumDto();
            var expectedErrorMessage = "Unable to update album. No new property values were provided.";

            // Act
            var actual = await _albumController.PutAlbum(testId, testAlbumDto);

            // Assert
            actual.Should().BeOfType<BadRequestObjectResult>();
            var badRequestObjectResult = actual as BadRequestObjectResult;
            var result = badRequestObjectResult?.Value as string;
            result.Should().Be(expectedErrorMessage);
        }

        [Test]
        public async Task PutAlbum_AlbumDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            var testId = 1;
            var expectedErrorMessage = $"Unable to update album. No Album with id '{testId}' exists.";
            var testAlbumDto = new PutAlbumDto { Title = "TestAlbum1" };

            _mockAlbumService
                .Setup(s => s.UpdateAlbumAsync(testId, testAlbumDto))
                .ReturnsAsync((int _, PutAlbumDto _) => null);

            // Act
            var actual = await _albumController.PutAlbum(testId, testAlbumDto);

            // Assert
            actual.Should().BeOfType<NotFoundObjectResult>();
            var notFoundObjectResult = actual as NotFoundObjectResult;
            var result = notFoundObjectResult?.Value as string;
            result.Should().Be(expectedErrorMessage);
        }

        [Test]
        public async Task PutAlbum_AlbumExists_ReturnsOkWithUpdatedAlbum()
        {
            // Arrange
            var testId = 1;
            var testAlbumDto = new PutAlbumDto { Title = "newTestAlbum1" };
            var existingAlbum = new Album { Id = testId, Title = "TestAlbum1", ArtistId = 1, GenreId = 1, ReleaseYear = DateTime.UtcNow.Year, Artist = new() { Name = "TestArtist1" } };
            var expected = existingAlbum.ToAlbumResponseDto();
            expected.AlbumId = testId;

            _mockAlbumService
                .Setup(s => s.UpdateAlbumAsync(testId, testAlbumDto))
                .ReturnsAsync(expected);

            // Act
            var actual = await _albumController.PutAlbum(testId, testAlbumDto);

            // Assert
            actual.Should().BeOfType<OkObjectResult>();
            var okObjectResult = actual as OkObjectResult;
            okObjectResult?.Value.Should().BeOfType<AlbumResponseDto>();
            var result = okObjectResult?.Value as AlbumResponseDto;
            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task DeleteAlbum_AlbumDoesNotExist_ReturnsNotFoundWithExpectedErrorMessage()
        {
            // Arrange
            var testId = 1;
            var expectedErrorMessage = $"Unable to delete album. No Album with id '{testId}' exists.";

            _mockAlbumService
                .Setup(s => s.RemoveAlbumByIdAsync(testId))
                .ReturnsAsync(-1);

            // Act
            var actual = await _albumController.DeleteAlbum(testId);

            // Assert
            actual.Should().BeOfType<NotFoundObjectResult>();
            var notFoundObjectResult = actual as NotFoundObjectResult;
            var actualErrorMessage = notFoundObjectResult?.Value as string;
            actualErrorMessage.Should().Be(expectedErrorMessage);
        }

        [Test]
        public async Task DeleteAlbum_AlbumExists_ReturnsNoContent()
        {
            // Arrange
            var testId = 1;

            _mockAlbumService
                .Setup(s => s.RemoveAlbumByIdAsync(testId))
                .ReturnsAsync(1);

            // Act
            var actual = await _albumController.DeleteAlbum(testId);

            // Assert
            actual.Should().BeOfType<NoContentResult>();
        }
    }
}
