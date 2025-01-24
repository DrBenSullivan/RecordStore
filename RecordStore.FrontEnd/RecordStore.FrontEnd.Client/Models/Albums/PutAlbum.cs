namespace RecordStore.FrontEnd.Client.Models.Albums
{
	public class PutAlbum
	{
		public int Id { get; set; }
		public string? Title { get; set; }       
		public int? ArtistId { get; set; }
		public string? ArtistName { get; set; }
		public int? ReleaseYear { get; set; }
		public int? GenreId { get; set; }
		public string? GenreName { get; set; }
		public int Stock { get; set; }
	}
}
