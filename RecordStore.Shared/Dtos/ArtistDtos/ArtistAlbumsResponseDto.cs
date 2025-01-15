using RecordStore.Shared.Dtos.AlbumDtos;

namespace RecordStore.Shared.Dtos.ArtistDtos
{
    public class ArtistAlbumsResponseDto
    {
        public int ArtistId { get; set; }
        public string ArtistName { get; set; } = string.Empty;
        public List<AlbumResponseDto>? Albums { get; set; } = null;
    }
}
