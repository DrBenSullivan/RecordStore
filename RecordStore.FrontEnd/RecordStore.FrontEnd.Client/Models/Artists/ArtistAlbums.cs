using RecordStore.FrontEnd.Client.Models.Albums;

namespace RecordStore.FrontEnd.Client.Models.Artists
{
	public class ArtistAlbums
	{
		public int ArtistId { get; set; }
		public string ArtistName { get; set; } = string.Empty;
		public List<Album>? Albums { get; set; } = null;
	}
}
