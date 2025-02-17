﻿using RecordStore.Core.Models;
using RecordStore.Shared.Dtos.AlbumDtos;

namespace RecordStore.Application.Extensions
{
	public static class AlbumExtensions
	{
		public static AlbumResponseDto ToAlbumResponseDto(this Album album)
		{
			return new AlbumResponseDto
			{
				AlbumId = album.Id,
				AlbumTitle = album.Title,
				Artist = album.Artist?.Name ?? throw new ApplicationException($"Tried to convert Album with no artist to Album Response Dto."),
				ReleaseYear = album.ReleaseYear,
				Genre = album.Genre?.Name,
				StockQuantity = album.Stock?.Quantity ?? 0
			};
		}

		public static AlbumDetailsDto ToAlbumDetailsDto(this Album album)
		{
			return new AlbumDetailsDto
			{
				Id = album.Id,
				Title = album.Title,
				ArtistId = album.Artist!.Id,
				ArtistName = album.Artist!.Name,
				GenreId = album.Genre!.Id,
				GenreName = album.Genre!.Name,
				ReleaseYear = album.ReleaseYear,
				Stock = album.Stock!.Quantity
			};
		}
	}
}
