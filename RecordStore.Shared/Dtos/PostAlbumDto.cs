using System.ComponentModel.DataAnnotations;

namespace RecordStore.Shared.Dtos
{
    public class PostAlbumDto
    {
        [Required]
        [Length(1, 255, ErrorMessage = "Album title must be between 1 and 255 characters in length.")]
        public string Title { get; set; } = string.Empty;

        [Required]
        public int ArtistId { get; set; }

        [Required]
        [Range(1900, 2025, ErrorMessage = "Album release year must be between 1900 and 2025.")]
        public int ReleaseYear { get; set; }

        public int? GenreId { get; set; }
    }
}
