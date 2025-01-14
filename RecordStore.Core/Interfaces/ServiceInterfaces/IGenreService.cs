using RecordStore.Core.Models;

namespace RecordStore.Core.Interfaces.ServiceInterfaces
{
    public interface IGenreService
    {
        public Task<Genre?> FetchGenreByIdAsync(int id);
        public Task<List<Genre>> FetchAllGenresAsync();
    }
}
