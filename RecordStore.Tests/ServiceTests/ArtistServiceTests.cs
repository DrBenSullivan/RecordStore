using FluentAssertions;
using Moq;
using RecordStore.Application.Extensions;
using RecordStore.Application.Services;
using RecordStore.Core.Interfaces.RepositoryInterfaces;
using RecordStore.Core.Interfaces.ServiceInterfaces;
using RecordStore.Core.Models;

namespace RecordStore.Tests.ServiceTests
{
    public class ArtistSericeTests
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
            _artistRepositoryMock
                .Setup(r => r.FetchAllArtistsAsync())
                .ReturnsAsync(() => []);

            // Act
            var actual = await _artistService.FindAllArtistsAsync();

            // Assert
            actual.Should().BeEmpty();
        }

        [Test]
        public async Task FindAllArtistsAsync_Artists_ReturnsExpectedList()
        {
            // Arrange
            var existingArtists = new List<Artist>()
            {
                new() { Id = 1, Name = "TestArtist1" },
                new() { Id = 2, Name = "TestArtist2" },
                new() { Id = 3, Name = "TestArtist3" }
            };

            var expected = existingArtists.Select(a => a.ToArtistResponseDto()).ToList();

            _artistRepositoryMock
                .Setup(r => r.FetchAllArtistsAsync())
                .ReturnsAsync(existingArtists);

            // Act
            var actual = await _artistService.FindAllArtistsAsync();

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task FindArtistByIdAsync_DoesNotExist_ReturnsNull()
        {
            // Arrange
            int testId = 1;

            _artistRepositoryMock
                .Setup(r => r.FetchArtistByIdAsync(testId))
                .ReturnsAsync((int _) => null);

            // Act
            var actual = await _artistService.FindArtistByIdAsync(testId);

            // Assert
            actual.Should().BeNull();
        }

        [Test]
        public async Task FindArtistByIdAsync_Exists_ReturnsExpectedAlbum()
        {
            // Arrange
            int testId = 1;
            var existingArtist = new Artist { Id = testId, Name = "TestArtist1" };
            var expected = existingArtist.ToArtistResponseDto();

            _artistRepositoryMock
                .Setup(r => r.FetchArtistByIdAsync(testId))
                .ReturnsAsync(existingArtist);

            // Act
            var actual = await _artistService.FindArtistByIdAsync(testId);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}