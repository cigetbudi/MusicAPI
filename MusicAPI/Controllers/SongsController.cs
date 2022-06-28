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
        public IEnumerable<Song> Get()
        {
            return _db.Songs;
        } 

        // GET api/<SongsController>/5
        [HttpGet("{id}")]
        public Song Get(int id)
        {
            var song = _db.Songs.Find(id);
            return song;
        }

        // POST api/<SongsController>
        [HttpPost]
        public void Post([FromBody] Song song)
        {
            _db.Songs.Add(song);
            _db.SaveChanges();
        }

        // PUT api/<SongsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Song newSong)
        {
            var song = _db.Songs.Find(id);
            //song = newSong;
            song.Title = newSong.Title;
            song.Language = newSong.Language;
            _db.SaveChanges();
        }

        // DELETE api/<SongsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var song = _db.Songs.Find(id);
            _db.Songs.Remove(song);
            _db.SaveChanges();
        }
    }
}
