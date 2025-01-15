using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RecordStore.Api.Controllers;
using RecordStore.Application.Extensions;
using RecordStore.Core.Interfaces.ServiceInterfaces;
using RecordStore.Core.Models;
using RecordStore.Shared.Dtos.ArtistDtos;

namespace RecordStore.Tests.ControllerTests
{
    public class ArtistsControllerTests
    {
        private Mock<IArtistService> _mockArtistService;
        private ArtistsController _artistsController;

        [SetUp]
        public void SetUp()
        {
            _mockArtistService = new Mock<IArtistService>();
            _artistsController = new ArtistsController(_mockArtistService.Object);
        }

        [Test]
        public async Task GetAllArtists_NoArtistss_ReturnsOkEmptyList()
        {
            // Arrange
            _mockArtistService
                .Setup(s => s.FindAllArtistsAsync())
                .ReturnsAsync(() => []);

            // Act
            var actual = await _artistsController.GetAllArtists();

            // Assert
            actual.Should().BeOfType<OkObjectResult>();
            var okObjectResult = actual as OkObjectResult;
            okObjectResult?.Value.Should().BeOfType<List<ArtistResponseDto>>();
            var result = okObjectResult?.Value as List<ArtistResponseDto>;
            result.Should().BeEmpty();
        }

        [Test]
        public async Task GetAllArtists_Artists_ReturnsOkExpectedList()
        {
            // Arrange
            var existingArtists = new List<Artist>()
            {
                new() { Id = 1, Name = "TestArtist1" },
                new() { Id = 2, Name = "TestArtist2" },
                new() { Id = 3, Name = "TestArtist3" }
            };

            var expected = existingArtists.Select(a => a.ToArtistResponseDto()).ToList();

            _mockArtistService
                .Setup(s => s.FindAllArtistsAsync())
                .ReturnsAsync(expected);

            // Act
            var actual = await _artistsController.GetAllArtists();

            // Assert
            actual.Should().BeOfType<OkObjectResult>();
            var okObjectResult = actual as OkObjectResult;
            okObjectResult?.Value.Should().BeOfType<List<ArtistResponseDto>>();
            var result = okObjectResult?.Value as List<ArtistResponseDto>;
            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task GetArtistById_DoesNotExist_ReturnsNotFound()
        {
            // Arrange
            var testId = 1;
            var expectedErrorMessage = $"The Artist with Id '{testId}' could not be found.";

            _mockArtistService
                .Setup(s => s.FindArtistByIdAsync(testId))
                .ReturnsAsync((int _) => null);

            // Act
            var actual = await _artistsController.GetArtistById(testId);

            // Assert
            actual.Should().BeOfType<NotFoundObjectResult>();
            var notFoundObjectResult = actual as NotFoundObjectResult;
            var result = notFoundObjectResult?.Value;
            result.Should().BeEquivalentTo(expectedErrorMessage);
        }

        [Test]
        public async Task GetArtistById_Exists_ReturnsOkExpectedArtist()
        {
            // Arrange
            var testId = 1;
            var existingArtist = new Artist { Id = testId, Name = "TestArtist1" };
            var expected = existingArtist.ToArtistResponseDto();

            _mockArtistService
                .Setup(s => s.FindArtistByIdAsync(testId))
                .ReturnsAsync(expected);

            // Act
            var actual = await _artistsController.GetArtistById(testId);

            // Assert
            actual.Should().BeOfType<OkObjectResult>();
            var notFoundObjectResult = actual as OkObjectResult;
            notFoundObjectResult?.Value.Should().BeOfType<ArtistResponseDto>();
            var result = notFoundObjectResult?.Value as ArtistResponseDto;
            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task GetAlbumsByArtistId_ArtistDoesNotExist_ReturnsNotFoundWithExpectedErrorMessage()
        {
            // Arrange
            var testId = 1;
            var expectedErrorMessage = $"The Artist with Id '{testId}' could not be found.";

            _mockArtistService
                .Setup(s => s.FindAlbumsByArtistIdAsync(testId))
                .ReturnsAsync(() => null);

            // Act
            var actual = await _artistsController.GetAlbumsByArtistId(testId);

            // Assert
            actual.Should().BeOfType<NotFoundObjectResult>();
            var notFoundObjectResult = actual as NotFoundObjectResult;
            var result = notFoundObjectResult?.Value as string;
            result.Should().BeEquivalentTo(expectedErrorMessage);
        }

        [Test]
        public async Task GetAlbumsByArtistId_ArtistExists_ReturnsOkWithExpectedResult()
        {
            // Arrange
            var testId = 1;
            var testArtist = new Artist { Id = testId, Name = "TestArtist" };
            var testAlbums = new List<Album>
            {
                new() { Id = 1, ArtistId = 1, GenreId = 1, ReleaseYear = DateTime.UtcNow.Year, Title = "TestAlbum1", Artist = testArtist },
                new() { Id = 2, ArtistId = 1, GenreId = 2, ReleaseYear = DateTime.UtcNow.AddYears(-1).Year, Title = "TestAlbum2", Artist = testArtist },
                new() { Id = 3, ArtistId = 1, GenreId = 3, ReleaseYear = DateTime.UtcNow.AddYears(-2).Year, Title = "TestAlbum3", Artist = testArtist }
            };

            var expected = testArtist.ToArtistAlbumsResponseDto(testAlbums);

            _mockArtistService
                .Setup(s => s.FindAlbumsByArtistIdAsync(testId))
                .ReturnsAsync(expected);

            // Act
            var actual = await _artistsController.GetAlbumsByArtistId(testId);

            // Assert
            actual.Should().BeOfType<OkObjectResult>();
            var okObjectResult = actual as OkObjectResult;
            var result = okObjectResult?.Value as ArtistAlbumsResponseDto;
            result.Should().BeEquivalentTo(expected);
        }
    }
}
