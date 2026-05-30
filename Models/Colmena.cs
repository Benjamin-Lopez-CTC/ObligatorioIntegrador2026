using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ObligatorioIntegrador2026.Models
{
    public class Colmena
    {
        public int Id { get; set; }
        
        public string? Identificador { get; set; } // Ejemplo: "#HIVE-0042" (Opcional)
        
        [Required]
        [MaxLength(6)]
        public string CodigoEscaneo { get; set; } // Código numérico de 6 dígitos único
        
        public int ApiarioId { get; set; }
        public Apiario Apiario { get; set; }
        
        public string Estado { get; set; } // "Óptimo", "Alerta", "Crítico"
        
        public double PesoKg { get; set; }
        public double TemperaturaInterna { get; set; }
        public double HumedadInterna { get; set; } // En porcentaje, solo útil si EsPiloto = true
        
        public double ProduccionMielKg { get; set; } // Producción aportada por esta colmena
        
        public bool EsPiloto { get; set; } // Determina si se usa para el promedio de temperatura del apiario

        // Nuevas propiedades
        public int CantidadAbejas { get; set; }
        public string UbicacionIntraApiario { get; set; }
        public string EstadoReina { get; set; } = "Presente";
        public string ComportamientoAbejas { get; set; }

        public ICollection<NotaTecnica> NotasTecnicas { get; set; } = new List<NotaTecnica>();
    }
}
