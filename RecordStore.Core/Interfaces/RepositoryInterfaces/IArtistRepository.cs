using RecordStore.Core.Models;

namespace RecordStore.Core.Interfaces.RepositoryInterfaces
{
	public interface IArtistRepository
	{
		Task<List<Album>> FetchAlbumsByArtistAsync(int artistId);
		public Task<List<Artist>> FetchAllArtistsAsync();
		public Task<Artist?> FetchArtistByIdAsync(int id);
	}
}
