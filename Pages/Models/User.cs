using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = default!;

        // Navigation property for ratings
        public ICollection<Rating> Ratings { get; set; } = default!;
    }
}
