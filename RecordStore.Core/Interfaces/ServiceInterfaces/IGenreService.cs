using RecordStore.Shared.Dtos.GenreDtos;

namespace RecordStore.Core.Interfaces.ServiceInterfaces
{
    public interface IGenreService
    {
        public Task<GenreResponseDto?> FindGenreByIdAsync(int id);
        public Task<List<GenreResponseDto>> FindAllGenresAsync();
    }
}
