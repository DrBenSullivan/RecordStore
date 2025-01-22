namespace RecordStore.Shared.Dtos.AlbumDtos
{
	public class AlbumResponseDto
	{
		public int AlbumId { get; set; }
		public string AlbumTitle { get; set; } = string.Empty;
		public string Artist { get; set; } = string.Empty;
		public int ReleaseYear { get; set; }
		public string? Genre { get; set; }
		public int StockQuantity { get; set; }
	}
}
