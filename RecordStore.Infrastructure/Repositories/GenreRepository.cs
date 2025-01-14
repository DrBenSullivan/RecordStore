using Microsoft.EntityFrameworkCore;
using RecordStore.Core.Interfaces.RepositoryInterfaces;
using RecordStore.Core.Models;
using RecordStore.Infrastructure.Persistence;

namespace RecordStore.Infrastructure.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly ApplicationDbContext _db;

        public GenreRepository(ApplicationDbContext context)
        {
            _db = context;
        }

        public async Task<List<Genre>> FetchAllGenresAsync()
        {
            return await _db.Genres.ToListAsync();
        }

        public async Task<Genre?> FetchGenreByIdAsync(int id)
        {
            return await _db.Genres.FindAsync(id);
        }
    }
}
