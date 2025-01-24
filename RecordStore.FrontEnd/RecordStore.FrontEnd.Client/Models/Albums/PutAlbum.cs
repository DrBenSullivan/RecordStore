namespace RecordStore.FrontEnd.Client.Models.Albums
{
	public class PutAlbum
	{
		public string? Title { get; set; }
		public int? ArtistId { get; set; }
		public int? ReleaseYear { get; set; }
		public int? GenreId { get; set; }
	}
}
