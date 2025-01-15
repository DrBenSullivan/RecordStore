using System.ComponentModel.DataAnnotations;

namespace RecordStore.Shared.Dtos.AlbumDtos
{
    public class AlbumFilterOptionsDto
    {
        public bool? InStock;
        [Range(1900, 2025)]
        public int? ReleaseYear;
        public int? GenreId;
    }
}
