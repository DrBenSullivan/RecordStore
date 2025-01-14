using Microsoft.EntityFrameworkCore;
using RecordStore.Core.Interfaces.RepositoryInterfaces;
using RecordStore.Core.Models;
using RecordStore.Infrastructure.Persistence;

namespace RecordStore.Application.Repositories
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly ApplicationDbContext _db;

        public ArtistRepository(ApplicationDbContext context)
        {
            _db = context;
        }

        public async Task<List<Artist>> FetchAllArtists()
        {
            return await _db.Artists.ToListAsync();
        }

        public async Task<Artist?> FetchArtistById(int id)
        {
            return await _db.Artists.FindAsync(id);
        }
    }
}
