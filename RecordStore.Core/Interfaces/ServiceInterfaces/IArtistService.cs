using RecordStore.Shared.Dtos.ArtistDtos;

namespace RecordStore.Core.Interfaces.ServiceInterfaces
{
	public interface IArtistService
	{
		Task<ArtistAlbumsResponseDto?> FindAlbumsByArtistIdAsync(int artistId);
		Task<List<ArtistResponseDto>> FindAllArtistsAsync();
		Task<ArtistResponseDto?> FindArtistByIdAsync(int id);
	}
}
