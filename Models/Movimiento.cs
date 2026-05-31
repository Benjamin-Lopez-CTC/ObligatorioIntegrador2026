using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ObligatorioIntegrador2026.Models
{
    public class Movimiento
    {
        public int Id { get; set; }

        [Required]
        public int ColmenaId { get; set; }
        public Colmena Colmena { get; set; }

        [Required]
        public int ApiarioOrigenId { get; set; }
        public Apiario ApiarioOrigen { get; set; }

        [Required]
        public int ApiarioDestinoId { get; set; }
        public Apiario ApiarioDestino { get; set; }

        [Required]
        [MaxLength(200)]
        public string Razon { get; set; }

        [Required]
        public DateTime FechaSalida { get; set; }

        [Required]
        public DateTime FechaRegreso { get; set; }

        [Required]
        public string Estado { get; set; } // "Vigente", "Completado", "Cancelado"
    }
}
