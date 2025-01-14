using Microsoft.AspNetCore.Mvc;
using RecordStore.Core.Interfaces.ServiceInterfaces;

namespace RecordStore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlbumsController : ControllerBase
    {
        private readonly IAlbumService _albumService;

        public AlbumsController(IAlbumService albumService)
        {
            _albumService = albumService;
        }

        [HttpGet]
        public IActionResult GetAllAlbums()
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id}")]
        public IActionResult GetAlbumById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
