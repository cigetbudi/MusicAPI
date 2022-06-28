using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> Get()
        {
            return Ok(await _db.Songs.ToListAsync());
        } 

        // GET api/<SongsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var song = await _db.Songs.FindAsync(id);
            if (song == null)
            {
                return NotFound("Lagu dengan Id tersebut tidak ditemukan");
            }
            return Ok(song);
        }

        // POST api/<SongsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Song song)
        {
            await _db.Songs.AddAsync(song);
            await _db.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT api/<SongsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Song newSong)
        {
            var song = await _db.Songs.FindAsync(id);
            if (song == null)
            {
                return NotFound("Lagu dengan Id tersebut tidak ditemukan");
            }
            else
            {
                song.Title = newSong.Title;
                song.Language = newSong.Language;
                song.Duration = newSong.Duration;
                await _db.SaveChangesAsync();
                return Ok("Data telah berhasil dirubah");
            }
        }

        // DELETE api/<SongsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var song = await _db.Songs.FindAsync(id);
            if (song ==null)
            {
                return NotFound("Lagu dengan Id tersebut tidak ditemukan");
            }
            else
            {
                _db.Songs.Remove(song);
                await _db.SaveChangesAsync();
                return Ok("Data telah berhasil dihapus");
            }
        }
    }
}
