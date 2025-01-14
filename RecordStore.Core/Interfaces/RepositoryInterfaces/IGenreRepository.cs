using RecordStore.Core.Models;

namespace RecordStore.Core.Interfaces.RepositoryInterfaces
{
    public interface IGenreRepository
    {
        public Task<List<Genre>> FetchAllGenresAsync();
        public Task<Genre?> FetchGenreByIdAsync(int id);
    }
}
