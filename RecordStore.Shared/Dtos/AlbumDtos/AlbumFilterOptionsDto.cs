using System.ComponentModel.DataAnnotations;

namespace RecordStore.Shared.Dtos.AlbumDtos
{
	public class AlbumFilterOptionsDto
	{
		public bool? InStock = null;
		[Range(1900, 2025)]
		public int? ReleaseYear = null;
		public int? GenreId = null;
		public string? AlbumTitle = null;
	}
}
