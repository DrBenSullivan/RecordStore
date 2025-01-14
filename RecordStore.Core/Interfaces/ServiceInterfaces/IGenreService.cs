using RecordStore.Core.Models;

namespace RecordStore.Core.Interfaces.ServiceInterfaces
{
    public interface IGenreService
    {
        public Task<Genre?> FindGenreByIdAsync(int id);
        public Task<List<Genre>> FindAllGenresAsync();
    }
}
