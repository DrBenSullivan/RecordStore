using RecordStore.Core.Models;
using RecordStore.Shared.Dtos;

namespace RecordStore.Application.Extensions
{
    public static class PutAlbumDtoExtensions
    {
        public static Album ToUpdatedAlbum(this PutAlbumDto dto, Album existingAlbum) => new()
        {
            Id = existingAlbum.Id,
            Title = dto.Title ?? existingAlbum.Title,
            ArtistId = dto.ArtistId ?? existingAlbum.ArtistId,
            ReleaseYear = dto.ReleaseYear ?? existingAlbum.ReleaseYear,
            GenreId = dto.GenreId ?? existingAlbum.GenreId
        };
    }
}
