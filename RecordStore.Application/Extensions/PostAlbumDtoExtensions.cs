using RecordStore.Core.Models;
using RecordStore.Shared.Dtos;

namespace RecordStore.Application.Extensions
{
    public static class PostAlbumDtoExtensions
    {
        public static Album ToAlbum(this PostAlbumDto dto) => new()
        {
            Title = dto.Title,
            ArtistId = dto.ArtistId,
            ReleaseYear = dto.ReleaseYear,
            GenreId = dto.GenreId
        };
    }
}
