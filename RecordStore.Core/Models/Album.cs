namespace RecordStore.Core.Models
{
    public class Album
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int ArtistId { get; set; }
        public int ReleaseYear { get; set; }
        public int? GenreId { get; set; }

        // Navigation Properties
        public virtual Artist? Artist { get; set; }
        public virtual Genre? Genre { get; set; }
    }
}
