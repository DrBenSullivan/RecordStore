using RecordStore.Application.Extensions;
using RecordStore.Core.Interfaces.RepositoryInterfaces;
using RecordStore.Core.Interfaces.ServiceInterfaces;
using RecordStore.Core.Models;
using RecordStore.Shared.Dtos.ArtistDtos;

namespace RecordStore.Application.Services
{
    public class ArtistService : IArtistService
    {
        private readonly IArtistRepository _artistRepository;

        public ArtistService(IArtistRepository artistRepository)
        {
            _artistRepository = artistRepository;
        }

        public async Task<List<ArtistResponseDto>> FindAllArtistsAsync()
        {
            var artists = await _artistRepository.FetchAllArtistsAsync();

            return artists.Select(a => a.ToArtistResponseDto()).ToList();
        }

        public async Task<ArtistResponseDto?> FindArtistByIdAsync(int artistId)
        {
            var artist = await _artistRepository.FetchArtistByIdAsync(artistId);

            if (artist == null) return null;

            return artist.ToArtistResponseDto();
        }

        public async Task<List<ArtistAlbumsResponseDto>> FindAlbumsByArtistIdAsync(int artistId)
        {
            throw new NotImplementedException();
        }
    }
}
