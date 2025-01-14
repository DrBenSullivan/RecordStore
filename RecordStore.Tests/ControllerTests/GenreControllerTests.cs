using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RecordStore.Api.Controllers;
using RecordStore.Core.Interfaces.ServiceInterfaces;
using RecordStore.Core.Models;

namespace RecordStore.Tests.ControllerTests
{
    public class GenresControllerTests
    {
        private Mock<IGenreService> _genreService;
        private GenresController _genresController;

        [SetUp]
        public void SetUp()
        {
            _genreService = new Mock<IGenreService>();
            _genresController = new GenresController(_genreService.Object);
        }

        [Test]
        public async Task GetAllGenres_NoGenres_ReturnsOkEmptyList()
        {
            // Arrange
            var expected = new List<Genre>();
            _genreService.Setup(s => s.FindAllGenresAsync()).ReturnsAsync(expected);

            // Act
            var actual = await _genresController.GetAllGenres();

            // Assert
            actual.Should().BeOfType<OkObjectResult>();
            var okObjectResult = actual as OkObjectResult;
            okObjectResult?.Value.Should().BeOfType<List<Genre>>();
            var result = okObjectResult?.Value as List<Genre>;
            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task GetAllGenres_Genres_ReturnsOkExpectedList()
        {
            // Arrange
            var expected = new List<Genre>()
            {
                new() { Id = 1, Name = "TestGenre1" },
                new() { Id = 2, Name = "TestGenre2" },
                new() { Id = 3, Name = "TestGenre3" }

            };
            _genreService.Setup(s => s.FindAllGenresAsync()).ReturnsAsync(expected);

            // Act
            var actual = await _genresController.GetAllGenres();

            // Assert
            actual.Should().BeOfType<OkObjectResult>();
            var okObjectResult = actual as OkObjectResult;
            okObjectResult?.Value.Should().BeOfType<List<Genre>>();
            var result = okObjectResult?.Value as List<Genre>;
            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task GetGenreById_DoesNotExist_ReturnsNotFound()
        {
            // Arrange
            var testId = 1;
            Genre? testGenre = null;
            var expectedErrorMessage = $"The genre with id '{testId}' could not be found.";
            _genreService.Setup(s => s.FindGenreByIdAsync(testId)).ReturnsAsync(testGenre);

            // Act
            var actual = await _genresController.GetGenreById(testId);

            // Assert
            actual.Should().BeOfType<NotFoundObjectResult>();
            var notFoundObjectResult = actual as NotFoundObjectResult;
            var result = notFoundObjectResult?.Value;
            result.Should().BeEquivalentTo(expectedErrorMessage);
        }

        [Test]
        public async Task GetGenreById_Exists_ReturnsOkExpectedGenre()
        {
            // Arrange
            var testId = 1;
            var testGenre = new Genre { Id = testId, Name = "TestGenre1" };
            _genreService.Setup(s => s.FindGenreByIdAsync(testId)).ReturnsAsync(testGenre);

            // Act
            var actual = await _genresController.GetGenreById(testId);

            // Assert
            actual.Should().BeOfType<OkObjectResult>();
            var notFoundObjectResult = actual as OkObjectResult;
            notFoundObjectResult?.Value.Should().BeOfType<Genre>();
            var result = notFoundObjectResult?.Value as Genre;
            result.Should().BeEquivalentTo(testGenre);
        }
    }
}
