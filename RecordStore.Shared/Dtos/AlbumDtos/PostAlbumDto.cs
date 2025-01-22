using System.ComponentModel.DataAnnotations;

namespace RecordStore.Shared.Dtos.AlbumDtos
{
	public class PostAlbumDto
	{
		[Required(ErrorMessage = "Album Title must be provided.")]
		[Length(1, 255, ErrorMessage = "Album title must be between 1 and 255 characters in length.")]
		public string? Title { get; set; } = string.Empty;

		[Required(AllowEmptyStrings = false, ErrorMessage = "Album Artist Id must be provided.")]
		public int? ArtistId { get; set; }

		[Required(ErrorMessage = "Album release year must be provided.")]
		[Range(1900, 2025, ErrorMessage = "Album release year must be between 1900 and 2025.")]
		public int? ReleaseYear { get; set; }

		public int? GenreId { get; set; }
	}
}
