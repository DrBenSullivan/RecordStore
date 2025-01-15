using RecordStore.Core.Models;
using RecordStore.Shared.Dtos;

namespace RecordStore.Core.Interfaces.ServiceInterfaces
{
    public interface IAlbumService
    {
        Task<List<Album>> FindAllAlbumsAsync();
        Task<Album?> FindAlbumByIdAsync(int id);
        Task<Album?> AddAlbumAsync(PostAlbumDto dto);
        Task<Album?> UpdateAlbumAsync(int albumId, PutAlbumDto dto);
    }
}
