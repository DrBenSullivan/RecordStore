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
            return await _db.Albums.FindAsync(id);
        }
    }
}
