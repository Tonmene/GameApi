using System;
using System.ComponentModel.DataAnnotations;

namespace GamesCrudApi.Models
{
    public class Game
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = "";

        [Required]
        public string Genre { get; set; } = "";

        public DateTime ReleaseDate { get; set; }

        public string? Description { get; set; }
    }
}
