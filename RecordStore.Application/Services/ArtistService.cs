using RecordStore.Core.Interfaces.RepositoryInterfaces;
using RecordStore.Core.Interfaces.ServiceInterfaces;
using RecordStore.Core.Models;

namespace RecordStore.Application.Services
{
    public class ArtistService : IArtistService
    {
        private readonly IArtistRepository _artistRepository;

        public ArtistService(IArtistRepository artistRepository)
        {
            _artistRepository = artistRepository;
        }

        public async Task<List<Artist>> FindAllArtistsAsync()
        {
            return await _artistRepository.FetchAllArtistsAsync();
        }

        public async Task<Artist?> FindArtistByIdAsync(int id)
        {
            return await _artistRepository.FetchArtistByIdAsync(id);
        }
    }
}
