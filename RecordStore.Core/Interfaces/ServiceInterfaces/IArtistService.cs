using RecordStore.Core.Models;

namespace RecordStore.Core.Interfaces.ServiceInterfaces
{
    public interface IArtistService
    {
        public Task<List<Artist>> FindAllArtistsAsync();
        public Task<Artist?> FindArtistByIdAsync(int id);
    }
}
