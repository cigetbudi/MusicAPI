using Microsoft.AspNetCore.Mvc;
using MusicAPI.Data;
using MusicAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MusicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private ApiDbContext _db;
        public SongsController(ApiDbContext db)
        {
            _db = db;
        }
    
        // GET: api/<SongsController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_db.Songs);
        } 

        // GET api/<SongsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var song = _db.Songs.Find(id);
            return Ok(song);
        }

        // POST api/<SongsController>
        [HttpPost]
        public IActionResult Post([FromBody] Song song)
        {
            _db.Songs.Add(song);
            _db.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT api/<SongsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Song newSong)
        {
            var song = _db.Songs.Find(id);
            song.Title = newSong.Title;
            song.Language = newSong.Language;
            _db.SaveChanges();
            return Ok("Data telah berhasil dirubah");
        }

        // DELETE api/<SongsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var song = _db.Songs.Find(id);
            _db.Songs.Remove(song);
            _db.SaveChanges();
            return Ok("Data telah berhasil dihapus");
        }
    }
}
