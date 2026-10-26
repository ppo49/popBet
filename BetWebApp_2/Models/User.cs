using System.ComponentModel.DataAnnotations;
namespace BetWebApp_2.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required,MaxLength(20)]
        public string Username { get; set; } = "";
        [Required]
        public string PasswordHash { get; set; } = "";
        public UserRole Role { get; set; } = UserRole.Standard;
        public List<Ticket> Tickets { get; set; } = new();
    }
}
