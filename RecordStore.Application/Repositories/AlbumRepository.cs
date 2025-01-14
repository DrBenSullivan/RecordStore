using RecordStore.Core.Interfaces.RepositoryInterfaces;
using RecordStore.Infrastructure.Persistence;

namespace RecordStore.Application.Repositories
{
    public class AlbumRepository : IAlbumRepository
    {
        private readonly ApplicationDbContext _db;

        public AlbumRepository(ApplicationDbContext context)
        {
            _db = context;
        }
    }
}
