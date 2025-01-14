using RecordStore.Core.Models;

namespace RecordStore.Core.Interfaces.RepositoryInterfaces
{
    public interface IAlbumRepository
    {
        public Task<List<Album>> FetchAllAlbumsAsync();
        public Task<Album?> FetchAlbumByIdAsync(int id);
        public Task<Album?> AddAlbum(Album album);
    }
}
