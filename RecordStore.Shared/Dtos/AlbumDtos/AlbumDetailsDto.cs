using System.ComponentModel.DataAnnotations;

namespace RecordStore.Shared.Dtos.AlbumDtos
{
	public class AlbumDetailsDto
	{
		[Required]
		public int Id { get; set; }

		[Required]
		public string Title { get; set; } = null!;

		[Required]
		public int ArtistId { get; set; }

		[Required]
		public string ArtistName { get; set; } = null!;

		[Required]
		[Range(1900, 2025)]
		public int ReleaseYear { get; set; }

		[Required]
		public int GenreId { get; set; }

		[Required]
		public string GenreName { get; set; } = null!;

		[Required]
		public int Stock { get; set; }
	}
}