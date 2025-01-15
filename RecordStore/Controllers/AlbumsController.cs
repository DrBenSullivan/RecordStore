using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using RecordStore.Api.Extensions;
using RecordStore.Application.Extensions;
using RecordStore.Core.Interfaces.ServiceInterfaces;
using RecordStore.Shared.Dtos.AlbumDtos;

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

        [HttpGet("{albumId}")]
        public async Task<IActionResult> GetAlbumById(int albumId)
        {
            var result = await _albumService.FindAlbumByIdAsync(albumId);

            if (result == null) return NotFound($"The album with id '{albumId}' could not be found.");

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostAlbum(PostAlbumDto postAlbumDto)
        {
            var result = await _albumService.AddAlbumAsync(postAlbumDto);

            if (result == null) return Conflict($"Unable to add album. An Album with Title '{postAlbumDto.Title}', Artist Id '{postAlbumDto.ArtistId}' and Release Year '{postAlbumDto.ReleaseYear}' already exists.");

            return CreatedAtAction("GetAlbumById", new { result.AlbumId }, result);
        }

        [HttpPut("{albumId}")]
        public async Task<IActionResult> PutAlbum(int albumId, PutAlbumDto putAlbumDto)
        {
            if (putAlbumDto.HasNoProperties()) return BadRequest("Unable to update album. No new property values were provided.");

            var result = await _albumService.UpdateAlbumAsync(albumId, putAlbumDto);

            if (result == null) return NotFound($"Unable to update album. No Album with id '{albumId}' exists.");

            return Ok(result);
        }

        [HttpDelete("{albumId}")]
        public async Task<IActionResult> DeleteAlbum(int albumId)
        {
            var result = await _albumService.RemoveAlbumByIdAsync(albumId);

            if (result == -1) return NotFound($"Unable to delete album. No Album with id '{albumId}' exists.");

            return NoContent();
        }
    }
}
