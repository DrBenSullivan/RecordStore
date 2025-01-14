using RecordStore.Core.Interfaces.RepositoryInterfaces;
using RecordStore.Core.Interfaces.ServiceInterfaces;
using RecordStore.Core.Models;

namespace RecordStore.Application.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;

        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<List<Genre>> FindAllGenresAsync()
        {
            return await _genreRepository.FetchAllGenresAsync();
        }

        public async Task<Genre?> FindGenreByIdAsync(int id)
        {
            return await _genreRepository.FetchGenreByIdAsync(id);
        }
    }
}
