using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecordStore.Core.Models
{
    public class Album
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime ReleaseYear { get; set; }
        public int ArtistId { get; set; }
        public int? GenreId { get; set; }

        // Navigation Properties
        public required virtual Artist Artist { get; set; }
        public virtual Genre? Genre { get; set; }
    }
}
