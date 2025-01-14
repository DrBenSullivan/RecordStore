using Microsoft.AspNetCore.Mvc;
using RecordStore.Core.Interfaces.ServiceInterfaces;

namespace RecordStore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet]
        public IActionResult GetAllGenres()
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id}")]
        public IActionResult GetGenreById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
