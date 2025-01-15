﻿using RecordStore.Application.Extensions;
using RecordStore.Core.Interfaces.RepositoryInterfaces;
using RecordStore.Core.Interfaces.ServiceInterfaces;
using RecordStore.Core.Models;
using RecordStore.Shared.Dtos;

namespace RecordStore.Application.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly IAlbumRepository _albumRepository;

        public AlbumService(IAlbumRepository albumRepository)
        {
            _albumRepository = albumRepository;
        }

        public async Task<Album?> AddAlbumAsync(PostAlbumDto dto)
        {
            var album = dto.ToAlbum();
            return await _albumRepository.AddAlbumAsync(album);
        }

        public async Task<Album?> FindAlbumByIdAsync(int id)
        {
            return await _albumRepository.FetchAlbumByIdAsync(id);
        }

        public async Task<List<Album>> FindAllAlbumsAsync()
        {
            return await _albumRepository.FetchAllAlbumsAsync();
        }

        public async Task<Album?> UpdateAlbumAsync(int albumId, PutAlbumDto dto)
        {
            var existingAlbum = await FindAlbumByIdAsync(albumId);

            if (existingAlbum == null) return null;

            var updatedAlbum = dto.ToUpdatedAlbum(existingAlbum);

            return await _albumRepository.UpdateAlbumAsync(updatedAlbum);
        }

        public async Task<int> RemoveAlbumByIdAsync(int albumId)
        {
            throw new NotImplementedException();
        }
    }
}
