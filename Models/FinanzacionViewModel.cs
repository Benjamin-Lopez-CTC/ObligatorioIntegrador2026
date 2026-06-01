using System.Collections.Generic;

namespace ObligatorioIntegrador2026.Models
{
    public class FinanzacionViewModel
    {
        public Analisis? AnalisisActivo { get; set; }
        public List<Analisis> HistorialAnalisis { get; set; } = new List<Analisis>();
    }
}
