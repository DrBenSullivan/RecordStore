using Microsoft.AspNetCore.Mvc;
using RecordStore.Core.Interfaces.ServiceInterfaces;

namespace RecordStore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArtistsController : ControllerBase
    {
        private readonly IArtistService _artistService;

        public ArtistsController(IArtistService artistService)
        {
            _artistService = artistService;
        }

        [HttpGet]
        public IActionResult GetAllArtists()
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id}")]
        public IActionResult GetArtistById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
