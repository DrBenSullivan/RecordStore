using Microsoft.EntityFrameworkCore;
using RecordStore.Core.Interfaces.RepositoryInterfaces;
using RecordStore.Core.Models;
using RecordStore.Infrastructure.Persistence;
using RecordStore.Shared.Dtos.AlbumDtos;

namespace RecordStore.Infrastructure.Repositories
{
    public class AlbumRepository : IAlbumRepository
    {
        private readonly ApplicationDbContext _db;

        public AlbumRepository(ApplicationDbContext context)
        {
            _db = context;
        }

        public async Task<List<Album>> FetchAllAlbumsAsync()
        {
            return await GetAlbumsWithIncludedRelations().ToListAsync();
        }

        public async Task<Album?> FetchAlbumByIdAsync(int id)
        {
            return await GetAlbumsWithIncludedRelations().FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Album?> AddAlbumAsync(Album album)
        {
            //var existingAlbum = await _db.Albums
            //    .FirstOrDefaultAsync(a =>
            //        a.Title == album.Title &&
            //        a.ArtistId == album.ArtistId &&
            //        a.ReleaseYear == album.ReleaseYear);

            //if (existingAlbum != null) return null;

            await _db.Albums.AddAsync(album);
            await _db.SaveChangesAsync();

            return await GetAlbumsWithIncludedRelations().FirstOrDefaultAsync(a => a.Id == album.Id);
        }

        public async Task<Album?> UpdateAlbumAsync(Album album)
        {
            var existingAlbum = await _db.Albums.FindAsync(album.Id);

            if (existingAlbum == null) return null;

            _db.Entry(existingAlbum).CurrentValues.SetValues(album);

            await _db.SaveChangesAsync();

            return await GetAlbumsWithIncludedRelations().FirstOrDefaultAsync(a => a.Id == album.Id);
        }

        public async Task<int> RemoveAlbumByIdAsync(int id)
        {
            var existingAlbum = await _db.Albums.FindAsync(id);

            if (existingAlbum == null) return -1;

            _db.Albums.Remove(existingAlbum);

            return await _db.SaveChangesAsync();
        }

        public async Task<List<Album>> FetchAllInStockAlbumsAsync()
        {
            return await GetAlbumsWithIncludedRelations()
                .Where(s => s.Stock != null && s.Stock.Quantity > 0)
                .ToListAsync();
        }

        public async Task<List<Album>> FetchAlbumsAsync(AlbumFilterOptionsDto? filterOptions = null)
        {
            var albums = GetAlbumsWithIncludedRelations();

            if (filterOptions == null) return await albums.ToListAsync();

            if (filterOptions.InStock.HasValue && filterOptions.InStock.Value)
                albums = albums.Where(a => a.Stock != null && a.Stock.Quantity > 0);

            if (filterOptions.ReleaseYear.HasValue)
                albums = albums.Where(a => a.ReleaseYear == filterOptions.ReleaseYear);

            if (filterOptions.GenreId.HasValue)
                albums = albums.Where(a => a.GenreId == filterOptions.GenreId);

            return await albums.ToListAsync();
        }

        private IQueryable<Album> GetAlbumsWithIncludedRelations()
        {
            return _db.Albums
                .Include(a => a.Artist)
                .Include(a => a.Genre)
                .Include(a => a.Stock);
        }
    }
}
