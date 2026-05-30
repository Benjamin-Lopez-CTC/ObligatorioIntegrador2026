using System;
using System.ComponentModel.DataAnnotations;

namespace ObligatorioIntegrador2026.Models
{
    public class NotaTecnica
    {
        public int Id { get; set; }

        [Required]
        public int ColmenaId { get; set; }
        
        public Colmena? Colmena { get; set; }

        [Required(ErrorMessage = "Los detalles de la nota son obligatorios.")]
        [StringLength(500)]
        public string Detalles { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string EstadoReina { get; set; } = "Presente";

        [Required]
        [StringLength(50)]
        public string EstadoColmena { get; set; } = "Óptimo";

        [Required]
        public DateTime Fecha { get; set; } = DateTime.Now;
    }
}
