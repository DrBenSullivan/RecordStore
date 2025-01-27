namespace RecordStore.FrontEnd.Client.Models.Albums
{
	public class PostAlbum
	{
		public string? Title { get; set; } = null!;
		public int? ArtistId { get; set; } = null;
		public int? ReleaseYear { get; set; } = null;
		public int? GenreId { get; set; } = null;
	}
}
