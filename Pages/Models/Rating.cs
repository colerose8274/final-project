using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int EpisodeId { get; set; }

        [Range(1, 5)]
        public int Score { get; set; }

        // Navigation properties
        public User User { get; set; } = default!;
        public Episode Episode { get; set; } = default!;
    }
}
