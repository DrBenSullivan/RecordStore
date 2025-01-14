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

        public Task<List<Artist>> FindAllArtistsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Artist?> FindArtistByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
