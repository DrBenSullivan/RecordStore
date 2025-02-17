﻿using Microsoft.EntityFrameworkCore;
using RecordStore.Core.Interfaces.RepositoryInterfaces;
using RecordStore.Core.Models;
using RecordStore.Infrastructure.Persistence;

namespace RecordStore.Infrastructure.Repositories
{
	public class ArtistRepository : IArtistRepository
	{
		private readonly ApplicationDbContext _db;

		public ArtistRepository(ApplicationDbContext context)
		{
			_db = context;
		}

		public async Task<List<Artist>> FetchAllArtistsAsync()
		{
			return await _db.Artists.ToListAsync();
		}

		public async Task<Artist?> FetchArtistByIdAsync(int artistId)
		{
			return await _db.Artists.FindAsync(artistId);
		}

		public async Task<List<Album>> FetchAlbumsByArtistAsync(int artistId)
		{
			return await _db.Albums
				.Include(a => a.Artist)
				.Include(a => a.Genre)
				.Include(a => a.Stock)
				.Where(a => a.ArtistId == artistId)
				.ToListAsync();
		}

		public async Task<Artist?> FetchArtistByNameAsync(string artistName)
		{
			return await _db.Artists.FirstOrDefaultAsync(a => a.Name == artistName);
		}
	}
}
