using RecordStore.Shared.Dtos.ArtistDtos;

namespace RecordStore.Core.Interfaces.ServiceInterfaces
{
    public interface IArtistService
    {
        Task<List<ArtistAlbumsResponseDto>> FindAlbumsByArtistIdAsync(int artistId);
        Task<List<ArtistResponseDto>> FindAllArtistsAsync();
        public Task<ArtistResponseDto?> FindArtistByIdAsync(int id);
    }
}
