using System.ComponentModel.DataAnnotations;

namespace ObligatorioIntegrador2026.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Username { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string Password { get; set; } = string.Empty; // En un escenario real, esto debería ser un hash

        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string Role { get; set; } = string.Empty;
    }
}
