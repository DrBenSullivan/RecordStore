using FluentAssertions;
using Moq;
using RecordStore.Application.Services;
using RecordStore.Core.Interfaces.RepositoryInterfaces;
using RecordStore.Core.Interfaces.ServiceInterfaces;
using RecordStore.Core.Models;

namespace RecordStore.Tests.ServiceTests
{
    public class ArtistServiceTests
    {
        private Mock<IArtistRepository> _artistRepositoryMock;
        private IArtistService _artistService;

        [SetUp]
        public void Setup()
        {
            _artistRepositoryMock = new Mock<IArtistRepository>();
            _artistService = new ArtistService(_artistRepositoryMock.Object);
        }

        [Test]
        public async Task FindAllArtistsAsync_NoArtists_ReturnsEmptyList()
        {
            // Arrange
            var expected = new List<Artist>();
            _artistRepositoryMock.Setup(r => r.FetchAllArtistsAsync()).Returns(Task.FromResult(expected));

            // Act
            var actual = await _artistService.FindAllArtistsAsync();

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task FindAllArtistsAsync_Artists_ReturnsExpectedList()
        {
            // Arrange
            var expected = new List<Artist>()
            {
                new() { Id = 1, Name = "TestArtist1" },
                new() { Id = 2, Name = "TestArtist2" },
                new() { Id = 3, Name = "TestArtist3" }
            };
            _artistRepositoryMock.Setup(r => r.FetchAllArtistsAsync()).ReturnsAsync(expected);

            // Act
            var actual = await _artistService.FindAllArtistsAsync();

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task FindArtistByIdAsync_DoesNotExist_ReturnsNull()
        {
            // Arrange
            Artist? expected = null;
            int testId = 1;
            _artistRepositoryMock.Setup(r => r.FetchArtistByIdAsync(testId)).ReturnsAsync(expected);

            // Act
            var actual = await _artistService.FindArtistByIdAsync(testId);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task FindArtistByIdAsync_Exists_ReturnsExpectedAlbum()
        {
            // Arrange
            int testId = 1;
            var expected = new Artist { Id = testId, Name = "TestArtist1" };
            _artistRepositoryMock.Setup(r => r.FetchArtistByIdAsync(testId)).ReturnsAsync(expected);

            // Act
            var actual = await _artistService.FindArtistByIdAsync(testId);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}