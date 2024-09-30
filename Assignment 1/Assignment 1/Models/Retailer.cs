using System.ComponentModel.DataAnnotations;

namespace Assignment_1.Models
{
    public class Retailer
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } //Hash
        public int RoleId { get; set; } //Hash
    }
}
