using System.Collections.Generic;

namespace ObligatorioIntegrador2026.Models
{
    public class MovimientosViewModel
    {
        public List<Movimiento> Vigentes { get; set; } = new List<Movimiento>();
        public List<Movimiento> Pasados { get; set; } = new List<Movimiento>();
        public List<Apiario> Apiarios { get; set; } = new List<Apiario>();
    }
}
