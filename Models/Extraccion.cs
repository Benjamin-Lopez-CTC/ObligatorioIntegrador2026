using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ObligatorioIntegrador2026.Models
{
    public class Extraccion
    {
        public int Id { get; set; }
        
        [Required]
        public DateTime Fecha { get; set; } = DateTime.Now;
        
        public double KilosTotales { get; set; }
        public int CantidadColmenasCosechadas { get; set; }
        
        public int? GananciaId { get; set; }
        public Ganancia? Ganancia { get; set; }
        
        public string? Notas { get; set; }

        public ICollection<NotaTecnica> NotasTecnicas { get; set; } = new List<NotaTecnica>();
    }
}
