﻿using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RecordStore.Core.Interfaces.ServiceInterfaces;

namespace RecordStore.Api.Controllers
{
	[ApiController]
	[EnableCors("AllowLocalhost")]
	[Route("api/[controller]")]
	public class ArtistsController : ControllerBase
	{
		private readonly IArtistService _artistService;

		public ArtistsController(IArtistService artistService)
		{
			_artistService = artistService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllArtists()
		{
			var result = await _artistService.FindAllArtistsAsync();

			return Ok(result);
		}

		[HttpGet("{artistId}")]
		public async Task<IActionResult> GetArtistById(int artistId)
		{
			var result = await _artistService.FindArtistByIdAsync(artistId);

			if (result == null) return NotFound($"The Artist with Id '{artistId}' could not be found.");

			return Ok(result);
		}

		[HttpGet("{artistId}/albums")]
		public async Task<IActionResult> GetAlbumsByArtistId(int artistId)
		{
			var result = await _artistService.FindAlbumsByArtistIdAsync(artistId);

			if (result == null) return NotFound($"The Artist with Id '{artistId}' could not be found.");

			return Ok(result);
		}

		[HttpGet("search/{artistName}")]
		public async Task<IActionResult> GetArtistByName(string artistName)
		{
			var result = await _artistService.FindArtistByNameAsync(artistName);

			if (result == null) return NotFound($"The Artist with Name '{artistName}' could not be found.");

			return Ok(result);
		}
	}
}
