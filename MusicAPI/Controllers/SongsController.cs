using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicAPI.Models;

namespace MusicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private static List<Song> songs = new List<Song>()
        {
            new Song(){Id =0, Title = "Masih", Language="Indonesia"},
            new Song(){Id =1, Title = "Setia", Language="Indonesia"},
        };

        [HttpGet]
        public IEnumerable<Song> Get()
        {
            return songs;
        }

        [HttpPost]
        public void Post([FromBody] Song song)
        {
            songs.Add(song);
        }
    }
}
