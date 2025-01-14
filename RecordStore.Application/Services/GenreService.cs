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

        public Task<List<Genre>> FetchAllGenresAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Genre?> FetchGenreByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
