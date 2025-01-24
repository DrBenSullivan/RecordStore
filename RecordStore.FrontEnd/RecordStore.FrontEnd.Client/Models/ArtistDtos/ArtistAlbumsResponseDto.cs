using RecordStore.FrontEnd.Client.Models.AlbumDtos;

namespace RecordStore.FrontEnd.Client.Models.ArtistDtos
{
	public class ArtistAlbumsResponseDto
	{
		public int ArtistId { get; set; }
		public string ArtistName { get; set; } = string.Empty;
		public List<AlbumResponseDto>? Albums { get; set; } = null;
	}
}
