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
            return await _db.Albums
                .Include(a => a.Genre)
                .Include(a => a.Artist)
                .ToListAsync();
        }

        public async Task<Album?> FetchAlbumByIdAsync(int id)
        {
            return await _db.Albums
                .FindAsync(id);
        }

        public async Task<Album?> AddAlbumAsync(Album album)
        {
            if (_db.Albums.Any(a => a.Title == album.Title && a.ArtistId == album.ArtistId && a.ReleaseYear == album.ReleaseYear))
                return null;

            await _db.Albums.AddAsync(album);
            await _db.SaveChangesAsync();
            return album;
        }

        public async Task<Album?> UpdateAlbumAsync(Album album)
        {
            throw new NotImplementedException();
        }
    }
}
