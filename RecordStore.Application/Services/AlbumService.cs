using RecordStore.Core.Interfaces.RepositoryInterfaces;
using RecordStore.Core.Interfaces.ServiceInterfaces;
using RecordStore.Core.Models;

namespace RecordStore.Application.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly IAlbumRepository _albumRepository;

        public AlbumService(IAlbumRepository albumRepository)
        {
            _albumRepository = albumRepository;
        }

        public Task<Album?> FindAlbumByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Album>> FindAllAlbumsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
