﻿using RecordStore.Application.Extensions;
using RecordStore.Core.Interfaces.RepositoryInterfaces;
using RecordStore.Core.Interfaces.ServiceInterfaces;
using RecordStore.Shared.Dtos.AlbumDtos;

namespace RecordStore.Application.Services
{
	public class AlbumService : IAlbumService
	{
		private readonly IAlbumRepository _albumRepository;

		public AlbumService(IAlbumRepository albumRepository)
		{
			_albumRepository = albumRepository;
		}

		public async Task<List<AlbumResponseDto>> FindAlbumsAsync(AlbumFilterOptionsDto? albumFilterOptionsDto = null)
		{
			var albums = await _albumRepository.FetchAlbumsAsync(albumFilterOptionsDto);

			return albums.Select(a => a.ToAlbumResponseDto()).ToList();
		}

		public async Task<AlbumResponseDto?> FindAlbumByIdAsync(int id)
		{
			var album = await _albumRepository.FetchAlbumByIdAsync(id);

			if (album == null) return null;

			return album.ToAlbumResponseDto();
		}

		public async Task<AlbumResponseDto?> AddAlbumAsync(PostAlbumDto postAlbumDto)
		{
			var album = postAlbumDto.ToAlbum();

			var result = await _albumRepository.AddAlbumAsync(album);

			if (result == null) return null;

			return result.ToAlbumResponseDto();
		}

		public async Task<AlbumResponseDto?> UpdateAlbumAsync(int albumId, PutAlbumDto dto)
		{
			var existingAlbum = await _albumRepository.FetchAlbumByIdAsync(albumId);

			if (existingAlbum == null) return null;

			var updatedAlbum = dto.ToUpdatedAlbum(existingAlbum);

			var result = await _albumRepository.UpdateAlbumAsync(updatedAlbum);

			if (result == null) return null;

			return result.ToAlbumResponseDto();
		}

		public async Task<int> RemoveAlbumByIdAsync(int albumId)
		{
			return await _albumRepository.RemoveAlbumByIdAsync(albumId);
		}

		public async Task<AlbumDetailsDto?> FindAlbumDetailsById(int albumId)
		{
			var result = await _albumRepository.FetchAlbumByIdAsync(albumId);

			if (result == null) return null;

			return result.ToAlbumDetailsDto();
		}

		public async Task<AlbumDetailsDto?> UpdateAlbumDetailsAsync(AlbumDetailsDto albumDetailsDto)
		{
			var result = await _albumRepository.UpdateAlbumDetailsAsync(albumDetailsDto);

			if (result == null) return null;

			return result.ToAlbumDetailsDto();
		}
	}
}
