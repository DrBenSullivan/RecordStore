namespace RecordStore.Core
{
    public class Album
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public Guid ArtistId { get; set; }
        public Guid GenreId { get; set; }
        public DateOnly ReleaseYear { get; set; }
    }
}
