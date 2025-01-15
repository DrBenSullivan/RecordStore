using RecordStore.Application.Extensions;
using RecordStore.Core.Interfaces.RepositoryInterfaces;
using RecordStore.Core.Interfaces.ServiceInterfaces;
using RecordStore.Core.Models;
using RecordStore.Shared.Dtos.GenreDtos;

namespace RecordStore.Application.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;

        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<List<GenreResponseDto>> FindAllGenresAsync()
        {
            var genres = await _genreRepository.FetchAllGenresAsync();

            return genres.Select(g => g.ToGenreResponseDto()).ToList();
        }

        public async Task<GenreResponseDto?> FindGenreByIdAsync(int id)
        {
            var genre = await _genreRepository.FetchGenreByIdAsync(id);

            if (genre == null) return null;

            return genre.ToGenreResponseDto();
        }
    }
}
