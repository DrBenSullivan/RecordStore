using Microsoft.EntityFrameworkCore;
using RecordStore.Core.Interfaces.RepositoryInterfaces;
using RecordStore.Core.Models;
using RecordStore.Infrastructure.Persistence;

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
            var existingAlbum = await _db.Albums
                .FirstOrDefaultAsync(a =>
                    a.Title == album.Title &&
                    a.ArtistId == album.ArtistId &&
                    a.ReleaseYear == album.ReleaseYear);

            if (existingAlbum != null) return null;

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

        private IQueryable<Album> GetAlbumsWithIncludedRelations()
        {
            return _db.Albums
                .Include(a => a.Artist)
                .Include(a => a.Genre)
                .Include(a => a.Stock);
        }
    }
}
