using RecordStore.Core.Interfaces.RepositoryInterfaces;
using RecordStore.Infrastructure.Persistence;

namespace RecordStore.Application.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly ApplicationDbContext _db;

        public GenreRepository(ApplicationDbContext context)
        {
            _db = context;
        }
    }
}
