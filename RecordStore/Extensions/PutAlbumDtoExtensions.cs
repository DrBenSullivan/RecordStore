using RecordStore.Shared.Dtos.AlbumDtos;

namespace RecordStore.Api.Extensions
{
	public static class PutAlbumDtoExtensions
	{
		public static bool HasNoProperties(this PutAlbumDto dto) => string.IsNullOrEmpty(dto.Title) && dto.ArtistId == null && dto.ReleaseYear == null && dto.GenreId == null;
	}
}
