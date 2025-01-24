namespace RecordStore.FrontEnd.Client.Models.Albums;

public class AlbumDetails
{
	public int Id { get; set; }
	public string Title { get; set; } = string.Empty;
	public int ArtistId { get; set; }
	public string ArtistName { get; set; } = null!;
	public int ReleaseYear { get; set; }
	public int? GenreId { get; set; }
	public string? GenreName { get; set; } = null;
	public int Stock { get; set; }

	public AlbumDetails Clone() => new()
	{
		Id = this.Id,
		Title = this.Title,
		ArtistId = this.ArtistId,
		ArtistName = this.ArtistName,
		ReleaseYear = this.ReleaseYear,
		GenreId = this.GenreId,
		GenreName = this.GenreName,
		Stock = this.Stock
	};
}