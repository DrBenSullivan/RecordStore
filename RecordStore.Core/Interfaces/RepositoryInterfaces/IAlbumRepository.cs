using RecordStore.Core.Models;

namespace RecordStore.Core.Interfaces.RepositoryInterfaces
{
    public interface IAlbumRepository
    {
        Task<List<Album>> FetchAllAlbumsAsync();
        Task<Album?> FetchAlbumByIdAsync(int id);
        Task<Album?> AddAlbumAsync(Album album);
        Task<Album?> UpdateAlbumAsync(Album album);
    }
}
