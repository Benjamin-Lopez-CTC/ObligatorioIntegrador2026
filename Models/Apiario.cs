using System.ComponentModel.DataAnnotations;

namespace ObligatorioIntegrador2026.Models
{
    public class Apiario
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; } // Ejemplo: "Apiario Norte"

        public string StringIdentificador { get; set; } // Ejemplo: "#API-001"

        public string UbicacionTexto { get; set; } // Ejemplo: "Valle Central"
        public string UbicacionCoordenadas { get; set; } // Ejemplo: "34°05'12.1\"S 70°45'22.4\"W"
        
        public DateTime FechaCreacion { get; set; }
        
        [NotFutureDate(ErrorMessage = "La fecha de la última inspección no puede ser posterior a la fecha actual.")]
        public DateTime? UltimaInspeccion { get; set; }
        
        public string Responsable { get; set; }
        public string NotasEstado { get; set; }
        public DateTime? UltimaEdicionNota { get; set; }
        
        public double HumedadInterna { get; set; } // Usado si hay sensor en apiario, ej 58%

        // Navigation property
        public ICollection<Colmena> Colmenas { get; set; } = new List<Colmena>();
    }
}
