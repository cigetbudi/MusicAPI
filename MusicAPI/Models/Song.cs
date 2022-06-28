using System.ComponentModel.DataAnnotations;

namespace MusicAPI.Models
{
    public class Song
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Judul tidak boleh kosong")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Bahasa tidak boleh kosong")]
        public string Language { get; set; }
        [Required(ErrorMessage = "Mohon untuk mengisi durasi lagu")]
        public string Duration { get; set; }
    }
}
