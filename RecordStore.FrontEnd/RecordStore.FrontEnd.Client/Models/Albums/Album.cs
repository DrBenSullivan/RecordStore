namespace RecordStore.FrontEnd.Client.Models.Albums
{
	public class Album
	{
		public int AlbumId { get; set; }
		public string AlbumTitle { get; set; } = string.Empty;
		public string Artist { get; set; } = string.Empty;
		public int ReleaseYear { get; set; }
		public string? Genre { get; set; }
		public int StockQuantity { get; set; } = 0;
	}
}
