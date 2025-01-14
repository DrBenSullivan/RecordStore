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

        public Task<Album?> AddAlbum(Album album)
        {
            throw new NotImplementedException();
        }

        public async Task<Album?> FindAlbumByIdAsync(int id)
        {
            return await _albumRepository.FetchAlbumByIdAsync(id);
        }

        public async Task<List<Album>> FindAllAlbumsAsync()
        {
            return await _albumRepository.FetchAllAlbumsAsync();
        }
    }
}
