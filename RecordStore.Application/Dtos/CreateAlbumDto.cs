using System.ComponentModel.DataAnnotations;
using RecordStore.Core.Models;

namespace RecordStore.Application.Dtos
{
    public class CreateAlbumDto
    {
        [Required]
        [Length(1, 255)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public int ArtistId { get; set; }

        [Required]
        public int ReleaseYear { get; set; }

        public int? GenreId { get; set; }

        public Album ToAlbum() => new()
        {
            Title = Title,
            ArtistId = ArtistId,
            ReleaseYear = ReleaseYear,
            GenreId = GenreId
        };
    }
}
