using System.ComponentModel.DataAnnotations;

namespace inSession.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
