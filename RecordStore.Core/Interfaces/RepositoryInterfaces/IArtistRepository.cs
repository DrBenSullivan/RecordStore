using RecordStore.Core.Models;

namespace RecordStore.Core.Interfaces.RepositoryInterfaces
{
	public interface IArtistRepository
	{
		Task<List<Album>> FetchAlbumsByArtistAsync(int artistId);
		Task<List<Artist>> FetchAllArtistsAsync();
		Task<Artist?> FetchArtistByIdAsync(int id);
		Task<Artist?> FetchArtistByNameAsync(string artistName);
	}
}
