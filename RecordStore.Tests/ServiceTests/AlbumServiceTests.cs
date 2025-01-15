using FluentAssertions;
using Moq;
using RecordStore.Application.Services;
using RecordStore.Core.Interfaces.RepositoryInterfaces;
using RecordStore.Core.Interfaces.ServiceInterfaces;
using RecordStore.Core.Models;
using RecordStore.Shared.Dtos;

namespace RecordStore.Tests.ServiceTests
{
    public class AlbumServiceTests
    {
        private Mock<IAlbumRepository> _albumRepositoryMock;
        private IAlbumService _albumService;

        [SetUp]
        public void Setup()
        {
            _albumRepositoryMock = new Mock<IAlbumRepository>();
            _albumService = new AlbumService(_albumRepositoryMock.Object);
        }

        [Test]
        public async Task FindAllAlbumsAsync_NoAlbums_ReturnsEmptyList()
        {
            // Arrange
            _albumRepositoryMock
                .Setup(r => r.FetchAllAlbumsAsync())
                .ReturnsAsync(() => []);

            // Act
            var actual = await _albumService.FindAllAlbumsAsync();

            // Assert
            actual.Should().BeEmpty();
        }

        [Test]
        public async Task FindAllAlbumsAsync_Albums_ReturnsExpectedList()
        {
            // Arrange
            var expected = new List<Album>()
            {
                new() { Id = 1, ArtistId = 1, GenreId = 1, ReleaseYear = DateTime.UtcNow.Year, Title = "TestAlbum1" },
                new() { Id = 2, ArtistId = 2, GenreId = 2, ReleaseYear = DateTime.UtcNow.AddYears(-1).Year, Title = "TestAlbum2" },
                new() { Id = 3, ArtistId = 3, GenreId = 3, ReleaseYear = DateTime.UtcNow.AddYears(-2).Year, Title = "TestAlbum3" },

            };

            _albumRepositoryMock
                .Setup(r => r.FetchAllAlbumsAsync())
                .ReturnsAsync(expected);

            // Act
            var actual = await _albumService.FindAllAlbumsAsync();

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task FindAlbumByIdAsync_DoesNotExist_ReturnsNull()
        {
            // Arrange
            int testId = 1;

            _albumRepositoryMock
                .Setup(r => r.FetchAlbumByIdAsync(testId))
                .ReturnsAsync((int _) => null);

            // Act
            var actual = await _albumService.FindAlbumByIdAsync(testId);

            // Assert
            actual.Should().BeNull();
        }

        [Test]
        public async Task FindAlbumByIdAsync_Exists_ReturnsExpectedAlbum()
        {
            // Arrange
            int testId = 1;
            var expected = new Album { Id = testId, ArtistId = 1, GenreId = 1, ReleaseYear = DateTime.UtcNow.Year, Title = "TestAlbum1" };

            _albumRepositoryMock
                .Setup(r => r.FetchAlbumByIdAsync(testId))
                .ReturnsAsync(expected);

            // Act
            var actual = await _albumService.FindAlbumByIdAsync(testId);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task AddAlbum_ValidAlbum_ReturnsUpdatedAlbum()
        {
            // Arrange
            int dbGeneratedId = 10;
            var testAlbumDto = new PostAlbumDto { ArtistId = 1, GenreId = 1, ReleaseYear = DateTime.UtcNow.Year, Title = "TestAlbum1" };

            _albumRepositoryMock
                .Setup(r => r.AddAlbumAsync(It.IsAny<Album>()))
                .ReturnsAsync((Album a) => 
                {
                    a.Id = dbGeneratedId;
                    return a;
                });

            // Act
            var actual = await _albumService.AddAlbumAsync(testAlbumDto);

            // Assert
            actual?.ArtistId.Should().Be(testAlbumDto.ArtistId);
            actual?.GenreId.Should().Be(testAlbumDto.GenreId);
            actual?.ReleaseYear.Should().Be(testAlbumDto.ReleaseYear);
            actual?.Title.Should().Be(testAlbumDto.Title);
            actual?.Id.Should().Be(dbGeneratedId);
        }

        [Test]
        public async Task AddAlbum_AlreadyExists_ReturnsNull()
        {
            // Arrange
            var testAlbumDto = new PostAlbumDto { ArtistId = 1, GenreId = 1, ReleaseYear = DateTime.UtcNow.Year, Title = "TestAlbum1" };

            _albumRepositoryMock
                .Setup(r => r.AddAlbumAsync(It.IsAny<Album>()))
                .ReturnsAsync((Album _) => null);

            // Act
            var actual = await _albumService.AddAlbumAsync(testAlbumDto);

            // Assert
            actual.Should().BeNull();
        }

        [Test]
        public async Task UpdateAlbumAsync_ValidProperties_ReturnsUpdatedAlbum()
        {
            // Arrange
            var testId = 1;
            var testAlbumDto = new PutAlbumDto { Title = "NewTestTitle" };
            var existingAlbum = new Album { Id = testId, Title = "TestTile1", ArtistId = 1, GenreId = 1, ReleaseYear = DateTime.UtcNow.Year };

            _albumRepositoryMock
                .Setup(r => r.FetchAlbumByIdAsync(testId))
                .ReturnsAsync(existingAlbum);

            _albumRepositoryMock
                .Setup(r => r.UpdateAlbumAsync(It.IsAny<Album>()))
                .ReturnsAsync((Album a) =>
                {
                    existingAlbum.Title = a.Title;
                    return existingAlbum;
                });

            // Act
            var result = await _albumService.UpdateAlbumAsync(testId, testAlbumDto);

            // Asset
            result?.Title.Should().Be(testAlbumDto.Title);
            result?.Id.Should().Be(existingAlbum.Id);
            result?.ArtistId.Should().Be(existingAlbum.ArtistId);
            result?.GenreId.Should().Be(existingAlbum.GenreId);
            result?.ReleaseYear.Should().Be(existingAlbum.ReleaseYear);
        }

        [Test]
        public async Task UpdateAlbumAsync_AlbumDoesNotExist_ReturnsNull()
        {
            // Arrange
            var testId = 1;
            var testAlbumDto = new PutAlbumDto() { Title = "NewTestTitle" };

            _albumRepositoryMock
                .Setup(r => r.FetchAlbumByIdAsync(testId))
                .ReturnsAsync((int _) => null);

            // Act
            var result = await _albumService.UpdateAlbumAsync(testId, testAlbumDto);

            // Assert
            result.Should().BeNull();
        }
    }
}