using RecordStore.Core.Models;
using RecordStore.Shared.Dtos.GenreDtos;

namespace RecordStore.Application.Extensions
{
    public static class GenreExtensions
    {
        public static GenreResponseDto ToGenreResponseDto(this Genre genre)
        {
            return new()
            {
                GenreId = genre.Id,
                GenreName = genre.Name
            };
        }
    }
}
