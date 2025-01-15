using RecordStore.Shared.Dtos.AlbumDtos;

namespace RecordStore.Core.Interfaces.ServiceInterfaces
{
    public interface IAlbumService
    {
        Task<AlbumResponseDto?> FindAlbumByIdAsync(int id);
        Task<AlbumResponseDto?> AddAlbumAsync(PostAlbumDto dto);
        Task<AlbumResponseDto?> UpdateAlbumAsync(int albumId, PutAlbumDto dto);
        Task<int> RemoveAlbumByIdAsync(int albumId);
        Task<List<AlbumResponseDto>> FindAlbumsAsync(AlbumFilterOptionsDto? albumFilterOptionsDto = null);
    }
}
