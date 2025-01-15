using RecordStore.Core.Models;
using RecordStore.Shared.Dtos.ArtistDtos;

namespace RecordStore.Application.Extensions
{
    public static class ArtistExtensions
    {
        public static ArtistResponseDto ToArtistResponseDto(this Artist artist)
        {
            return new ArtistResponseDto
            {
                ArtistId = artist.Id,
                ArtistName = artist.Name
            };
        }

        public static ArtistAlbumsResponseDto ToArtistAlbumsResponseDto(this Artist artist, List<Album> albums)
        {
            return new ArtistAlbumsResponseDto
            {
                ArtistId = artist.Id,
                ArtistName = artist.Name,
                Albums = albums.Select(a => a.ToAlbumResponseDto()).ToList()
            };
        }
    }
}
