using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ObligatorioIntegrador2026.Models
{
    public class Analisis
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime FechaInicio { get; set; } = DateTime.Now;

        public DateTime? FechaFin { get; set; }

        public List<Inversion> Inversiones { get; set; } = new List<Inversion>();
        public List<Ganancia> Ganancias { get; set; } = new List<Ganancia>();

        [NotMapped]
        public double TotalInversion => Inversiones != null ? Inversiones.Sum(i => i.Precio) : 0;

        [NotMapped]
        public double GananciaBruta => Ganancias != null ? Ganancias.Sum(g => g.Monto) : 0;

        [NotMapped]
        public double BalanceNeto => GananciaBruta - TotalInversion;
    }
}
