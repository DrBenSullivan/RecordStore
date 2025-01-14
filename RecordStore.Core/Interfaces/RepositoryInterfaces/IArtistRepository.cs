using RecordStore.Core.Models;

namespace RecordStore.Core.Interfaces.RepositoryInterfaces
{
    public interface IArtistRepository
    {
        public Task<List<Artist>> FetchAllArtistsAsync();
        public Task<Artist?> FetchArtistByIdAsync(int id);
    }
}
