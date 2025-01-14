using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecordStore.Core.Models
{
    public class Album
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public DateTime ReleaseYear { get; set; }

        [ForeignKey(nameof(Artist))]
        [Required]
        public int ArtistId { get; set; }
        public virtual Artist Artist { get; set; }

        [ForeignKey(nameof(Genre))]
        public int? GenreId { get; set; }
        public virtual Genre? Genre { get; set; }
    }
}
