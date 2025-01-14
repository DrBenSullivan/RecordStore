using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using RecordStore.Application.Services;
using RecordStore.Core.Interfaces.RepositoryInterfaces;
using RecordStore.Core.Interfaces.ServiceInterfaces;
using RecordStore.Core.Models;

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
            var expected = new List<Album>();
            _albumRepositoryMock.Setup(r => r.FetchAllAlbumsAsync()).Returns(Task.FromResult(expected));

            // Act
            var actual = await _albumService.FindAllAlbumsAsync();

            // Assert
            actual.Should().BeEquivalentTo(expected);
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
            _albumRepositoryMock.Setup(r => r.FetchAllAlbumsAsync()).ReturnsAsync(expected);

            // Act
            var actual = await _albumService.FindAllAlbumsAsync();

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task FindAlbumByIdAsync_DoesNotExist_ReturnsNull()
        {
            // Arrange
            Album? expected = null;
            int testId = 1;
            _albumRepositoryMock.Setup(r => r.FetchAlbumByIdAsync(testId)).ReturnsAsync(expected);

            // Act
            var actual = await _albumService.FindAlbumByIdAsync(testId);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task FindAlbumByIdAsync_Exists_ReturnsExpectedAlbum()
        {
            // Arrange
            int testId = 1;
            var expected = new Album { Id = testId, ArtistId = 1, GenreId = 1, ReleaseYear = DateTime.UtcNow.Year, Title = "TestAlbum1" };
            _albumRepositoryMock.Setup(r => r.FetchAlbumByIdAsync(testId)).ReturnsAsync(expected);

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
            var testAlbum = new Album { ArtistId = 1, GenreId = 1, ReleaseYear = DateTime.UtcNow.Year, Title = "TestAlbum1" };
            _albumRepositoryMock.Setup(r => r.AddAlbum(It.IsAny<Album>()))
                .Callback<Album>(a => a.Id = dbGeneratedId)
                .ReturnsAsync((Album a) => a);

            // Act
            var actual = await _albumService.AddAlbum(testAlbum);

            // Assert
            actual?.ArtistId.Should().Be(testAlbum.ArtistId);
            actual?.GenreId.Should().Be(testAlbum.GenreId);
            actual?.ReleaseYear.Should().Be(testAlbum.ReleaseYear);
            actual?.Title.Should().Be(testAlbum.Title);
            actual?.Id.Should().Be(dbGeneratedId);
        }

        [Test]
        public async Task AddAlbum_AlreadyExists_ReturnsNull()
        {
            // Arrange
            var testAlbum = new Album { ArtistId = 1, GenreId = 1, ReleaseYear = DateTime.UtcNow.Year, Title = "TestAlbum1" };
            _albumRepositoryMock.Setup(r => r.AddAlbum(It.IsAny<Album>())).Throws<DbUpdateException>();

            // Act
            var action = async () => await _albumService.AddAlbum(testAlbum);

            // Assert
            await action.Should().ThrowAsync<DbUpdateException>();
        }
    }
}