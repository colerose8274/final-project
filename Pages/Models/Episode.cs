using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models
{
    public class Episode
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = default!;

        [Required]
        public string Rating { get; set; } = default!;

        // Navigation property for ratings
        public List<Rating> Ratings { get; set; } = default!;
    }
}
