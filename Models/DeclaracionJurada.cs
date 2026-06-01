using System;
using System.ComponentModel.DataAnnotations;

namespace ObligatorioIntegrador2026.Models
{
    public class DeclaracionJurada
    {
        public int Id { get; set; }
        
        [Required]
        public DateTime FechaEntrega { get; set; }
    }
}
