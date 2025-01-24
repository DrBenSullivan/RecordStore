namespace RecordStore.FrontEnd.Client.Models.Albums
{
	public class PostAlbum
	{
		public string Title { get; set; } = null!;
		public int ArtistId { get; set; }
		public int ReleaseYear { get; set; }
		public int GenreId { get; set; }
	}
}
