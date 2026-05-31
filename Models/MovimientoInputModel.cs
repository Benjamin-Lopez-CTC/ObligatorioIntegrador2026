using System;
using System.ComponentModel.DataAnnotations;

namespace ObligatorioIntegrador2026.Models
{
    public class MovimientoInputModel
    {
        [Required]
        public int ApiarioOrigenId { get; set; }
        
        [Required]
        public int ColmenaId { get; set; }

        [Required]
        public int ApiarioDestinoId { get; set; }

        [Required]
        public string Razon { get; set; }

        [Required]
        public DateTime FechaRegreso { get; set; }
    }
}
