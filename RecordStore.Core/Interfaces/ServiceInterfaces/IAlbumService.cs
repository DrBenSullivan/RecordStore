using RecordStore.Core.Models;

namespace RecordStore.Core.Interfaces.ServiceInterfaces
{
    public interface IAlbumService
    {
        public Task<List<Album>> FindAllAlbumsAsync();
        public Task<Album?> FindAlbumByIdAsync(int id);
    }
}
