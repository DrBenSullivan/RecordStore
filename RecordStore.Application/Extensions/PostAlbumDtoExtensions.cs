using RecordStore.Core.Models;
using RecordStore.Shared.Dtos.AlbumDtos;

namespace RecordStore.Application.Extensions
{
	public static class PostAlbumDtoExtensions
	{
		public static Album ToAlbum(this PostAlbumDto dto) => new()
		{
			Title = dto.Title!,
			ArtistId = dto.ArtistId!.Value,
			ReleaseYear = dto.ReleaseYear!.Value,
			GenreId = dto.GenreId
		};
	}
}
