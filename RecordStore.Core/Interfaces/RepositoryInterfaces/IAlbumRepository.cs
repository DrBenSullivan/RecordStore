using RecordStore.Core.Models;
using RecordStore.Shared.Dtos.AlbumDtos;

namespace RecordStore.Core.Interfaces.RepositoryInterfaces
{
	public interface IAlbumRepository
	{
		Task<List<Album>> FetchAllAlbumsAsync();
		Task<Album?> FetchAlbumByIdAsync(int id);
		Task<Album?> AddAlbumAsync(Album album);
		Task<Album?> UpdateAlbumAsync(Album album);
		Task<int> RemoveAlbumByIdAsync(int id);
		Task<List<Album>> FetchAlbumsAsync(AlbumFilterOptionsDto? filterOptions = null);
		Task<Album?> UpdateAlbumDetailsAsync(AlbumDetailsDto albumDetailsDto);
	}
}
