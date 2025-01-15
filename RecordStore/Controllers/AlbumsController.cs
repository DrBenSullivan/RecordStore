﻿using Microsoft.AspNetCore.Mvc;
using RecordStore.Api.Dtos;
using RecordStore.Core.Interfaces.ServiceInterfaces;

namespace RecordStore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlbumsController : ControllerBase
    {
        private readonly IAlbumService _albumService;

        public AlbumsController(IAlbumService albumService)
        {
            _albumService = albumService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAlbums()
        {
            var result = await _albumService.FindAllAlbumsAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAlbumById(int id)
        {
            var result = await _albumService.FindAlbumByIdAsync(id);

            if (result == null) return NotFound($"The album with id '{id}' could not be found.");

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostAlbum(PostAlbumDto dto)
        {
            var album = dto.ToAlbum();

            var result = await _albumService.AddAlbumAsync(album);

            if (result == null) return Conflict($"Unable to add album. An Album with Title '{dto.Title}', Artist Id '{dto.ArtistId}' and Release Year '{dto.ReleaseYear}' already exists.");

            return CreatedAtAction("GetAlbumById", new { album.Id }, result);
        }
    }
}
