using RecordStore.Core.Models;

namespace RecordStore.Core.Interfaces.RepositoryInterfaces
{
    public interface IArtistRepository
    {
        public Task<List<Artist>> FetchAllArtists();
        public Task<Artist?> FetchArtistById(int id);
    }
}
