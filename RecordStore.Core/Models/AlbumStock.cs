namespace RecordStore.Core.Models
{
    public class AlbumStock
    {
        public int AlbumId { get; set; }
        public int Quantity { get; set; }

        // Navigation properties
        public Album Album { get; set; } = null!;
    }
}
