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
        private Mock<IArtistService> _artistService;
        private ArtistsController _artistsController;

        [SetUp]
        public void SetUp()
        {
            _artistService = new Mock<IArtistService>();
            _artistsController = new ArtistsController(_artistService.Object);
        }

        [Test]
        public async Task GetAllArtists_NoArtistss_ReturnsOkEmptyList()
        {
            // Arrange
            _artistService
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

            _artistService
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

            _artistService
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

            _artistService
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
    }
}
