using FluentAssertions;
using Moq;
using RecordStore.Application.Services;
using RecordStore.Core.Interfaces.RepositoryInterfaces;
using RecordStore.Core.Interfaces.ServiceInterfaces;
using RecordStore.Core.Models;

namespace RecordStore.Tests.ServiceTests
{
    public class GenreServiceTests
    {
        private Mock<IGenreRepository> _genreRepositoryMock;
        private IGenreService _genreService;

        [SetUp]
        public void Setup()
        {
            _genreRepositoryMock = new Mock<IGenreRepository>();
            _genreService = new GenreService(_genreRepositoryMock.Object);
        }

        [Test]
        public async Task FindAllGenresAsync_NoGenres_ReturnsEmptyList()
        {
            // Arrange
            var expected = new List<Genre>();
            _genreRepositoryMock.Setup(r => r.FetchAllGenresAsync()).ReturnsAsync(expected);

            // Act
            var actual = await _genreService.FindAllGenresAsync();

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task FindAllGenresAsync_Genres_ReturnsExpectedList()
        {
            // Arrange
            var expected = new List<Genre>()
            {
                new() { Id = 1, Name = "TestGenre1" },
                new() { Id = 2, Name = "TestGenre2" },
                new() { Id = 3, Name = "TestGenre3" }
            };
            _genreRepositoryMock.Setup(r => r.FetchAllGenresAsync()).ReturnsAsync(expected);

            // Act
            var actual = await _genreService.FindAllGenresAsync();

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task FindGenreByIdAsync_DoesNotExist_ReturnsNull()
        {
            // Arrange
            Genre? expected = null;
            int testId = 1;
            _genreRepositoryMock.Setup(r => r.FetchGenreByIdAsync(testId)).ReturnsAsync(expected);

            // Act
            var actual = await _genreService.FindGenreByIdAsync(testId);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task FindGenreByIdAsync_Exists_ReturnsExpectedGenre()
        {
            // Arrange
            int testId = 1;
            var expected = new Genre { Id = testId, Name = "TestGenre1" };
            _genreRepositoryMock.Setup(r => r.FetchGenreByIdAsync(testId)).ReturnsAsync(expected);

            // Act
            var actual = await _genreService.FindGenreByIdAsync(testId);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}