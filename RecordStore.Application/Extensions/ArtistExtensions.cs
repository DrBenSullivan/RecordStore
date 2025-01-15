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
    }
}
