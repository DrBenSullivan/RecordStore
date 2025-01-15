using FluentAssertions;
using Moq;
using RecordStore.Application.Extensions;
using RecordStore.Application.Services;
using RecordStore.Core.Interfaces.RepositoryInterfaces;
using RecordStore.Core.Interfaces.ServiceInterfaces;
using RecordStore.Core.Models;
using RecordStore.Shared.Dtos.AlbumDtos;

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
        public async Task FindAlbumsAsync_NoAlbums_ReturnsEmptyList()
        {
            // Arrange
            _albumRepositoryMock
                .Setup(r => r.FetchAlbumsAsync(null))
                .ReturnsAsync(() => []);

            // Act
            var actual = await _albumService.FindAlbumsAsync();

            // Assert
            actual.Should().BeEmpty();
        }

        [Test]
        public async Task FindAlbumsAsync_Albums_ReturnsExpectedList()
        {
            // Arrange
            var existingAlbums = new List<Album>()
            {
                new() { Id = 1, ArtistId = 1, GenreId = 1, ReleaseYear = DateTime.UtcNow.Year, Title = "TestAlbum1", Artist = new() { Name = "TestArtist1" } },
                new() { Id = 2, ArtistId = 2, GenreId = 2, ReleaseYear = DateTime.UtcNow.AddYears(-1).Year, Title = "TestAlbum2", Artist = new() { Name = "TestArtist2" } },
                new() { Id = 3, ArtistId = 3, GenreId = 3, ReleaseYear = DateTime.UtcNow.AddYears(-2).Year, Title = "TestAlbum3", Artist = new() { Name = "TestArtist3" } }
            };

            var expected = existingAlbums.Select(a => a.ToAlbumResponseDto()).ToList();

            _albumRepositoryMock
                .Setup(r => r.FetchAlbumsAsync(null))
                .ReturnsAsync(existingAlbums);

            // Act
            var actual = await _albumService.FindAlbumsAsync();

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
                .ReturnsAsync(() => null);

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
            var existingAlbum = new Album { Id = testId, ArtistId = 1, GenreId = 1, ReleaseYear = DateTime.UtcNow.Year, Title = "TestAlbum1", Artist = new() { Name = "TestArtist1" } };
            var expected = existingAlbum.ToAlbumResponseDto();

            _albumRepositoryMock
                .Setup(r => r.FetchAlbumByIdAsync(testId))
                .ReturnsAsync(existingAlbum);

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
            var existingAlbum = testAlbumDto.ToAlbum();
            existingAlbum.Id = dbGeneratedId;
            existingAlbum.Artist = new() { Name = "TestArtist1" };
            var expected = existingAlbum.ToAlbumResponseDto();

            _albumRepositoryMock
                .Setup(r => r.AddAlbumAsync(It.IsAny<Album>()))
                .ReturnsAsync(existingAlbum);

            // Act
            var actual = await _albumService.AddAlbumAsync(testAlbumDto);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task AddAlbum_AlreadyExists_ReturnsNull()
        {
            // Arrange
            var testAlbumDto = new PostAlbumDto { ArtistId = 1, GenreId = 1, ReleaseYear = DateTime.UtcNow.Year, Title = "TestAlbum1" };

            _albumRepositoryMock
                .Setup(r => r.AddAlbumAsync(It.IsAny<Album>()))
                .ReturnsAsync(() => null);

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
            var existingAlbum = new Album { Id = testId, Title = "TestTile1", ArtistId = 1, GenreId = 1, ReleaseYear = DateTime.UtcNow.Year, Artist = new() { Name = "TestArtist1" } };
            var expected = existingAlbum.ToAlbumResponseDto();

            _albumRepositoryMock
                .Setup(r => r.FetchAlbumByIdAsync(testId))
                .ReturnsAsync(existingAlbum);

            _albumRepositoryMock
                .Setup(r => r.UpdateAlbumAsync(It.IsAny<Album>()))
                .ReturnsAsync((Album a) =>
                {
                    existingAlbum.Title = a.Title;
                    expected = existingAlbum.ToAlbumResponseDto();
                    return existingAlbum;
                });


            // Act
            var result = await _albumService.UpdateAlbumAsync(testId, testAlbumDto);

            // Asset
            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task UpdateAlbumAsync_AlbumDoesNotExist_ReturnsNull()
        {
            // Arrange
            var testId = 1;
            var testAlbumDto = new PutAlbumDto() { Title = "NewTestTitle" };

            _albumRepositoryMock
                .Setup(r => r.FetchAlbumByIdAsync(testId))
                .ReturnsAsync(() => null);

            // Act
            var result = await _albumService.UpdateAlbumAsync(testId, testAlbumDto);

            // Assert
            result.Should().BeNull();
        }

        [Test]
        public async Task RemoveAlbumByIdAsync_AlbumDoesNotExist_ReturnsMinusOne()
        {
            // Arrange
            var testId = 1;
            var expected = -1;

            _albumRepositoryMock
                .Setup(r => r.RemoveAlbumByIdAsync(testId))
                .ReturnsAsync(expected);

            // Act
            var result = await _albumService.RemoveAlbumByIdAsync(testId);

            // Assert
            result.Should().Be(expected);
        }

        [Test]
        public async Task RemoveAlbumByIdAsync_AlbumExists_ReturnsOne()
        {
            // Arrange
            var testId = 1;
            var expected = 1;

            _albumRepositoryMock
                .Setup(r => r.RemoveAlbumByIdAsync(testId))
                .ReturnsAsync(expected);

            // Act
            var result = await _albumService.RemoveAlbumByIdAsync(testId);

            // Assert
            result.Should().Be(expected);
        }

        [Test]
        public async Task FindAllAlbumsInStock_AlbumsInStock_ReturnsExpectedList()
        {
            // Arrange
            var albumsInStock = new List<Album>
            {
                new() { Id = 1, ArtistId = 1, GenreId = 1, ReleaseYear = DateTime.UtcNow.Year, Title = "TestAlbum1", Artist = new() { Name = "TestArtist1" }, Stock = new() { AlbumId = 1, Quantity = 10} },
                new() { Id = 2, ArtistId = 2, GenreId = 2, ReleaseYear = DateTime.UtcNow.AddYears(-1).Year, Title = "TestAlbum2", Artist = new() { Name = "TestArtist2" }, Stock = new() { AlbumId = 2, Quantity = 20} },
                new() { Id = 3, ArtistId = 3, GenreId = 3, ReleaseYear = DateTime.UtcNow.AddYears(-2).Year, Title = "TestAlbum3", Artist = new() { Name = "TestArtist3" }, Stock = new() { AlbumId = 3, Quantity = 30} }
            };
            var filterOptions = new AlbumFilterOptionsDto { InStock = true };
            var expected = albumsInStock.Select(a => a.ToAlbumResponseDto()).ToList();

            _albumRepositoryMock
                .Setup(r => r.FetchAlbumsAsync(filterOptions))
                .ReturnsAsync(albumsInStock);

            // Act
            var actual = await _albumService.FindAlbumsAsync(filterOptions);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task FindAllAlbumsInStock_NoAlbumsInStock_ReturnsEmptyList()
        {
            // Arrange
            var filterOptions = new AlbumFilterOptionsDto { InStock = true };
            _albumRepositoryMock
                .Setup(r => r.FetchAlbumsAsync(filterOptions))
                .ReturnsAsync(() => []);

            // Act
            var actual = await _albumService.FindAlbumsAsync(filterOptions);

            // Assert
            actual.Should().BeOfType<List<AlbumResponseDto>>();
            actual.Should().BeEmpty();
        }
    }
}