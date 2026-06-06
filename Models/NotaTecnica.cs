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

        // Nuevos campos para cosecha y ambiente
        public double? Temperatura { get; set; }
        public double? Humedad { get; set; }
        
        public int AlzasCosechadas { get; set; }
        public int MediasAlzasCosechadas { get; set; }
        public int AlzasTresCuartosCosechadas { get; set; }
        public double KilosCosechados { get; set; }

        public int? ExtraccionId { get; set; }
        public Extraccion? Extraccion { get; set; }
    }
}
