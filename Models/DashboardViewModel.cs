using System.Collections.Generic;

namespace ObligatorioIntegrador2026.Models
{
    public class DashboardViewModel
    {
        public int TotalApiarios { get; set; }
        public int TotalColmenas { get; set; }
        public double TotalProduccionMielKg { get; set; }
        public int AlertasActivas { get; set; }

        public List<Movimiento> MovimientosVigentes { get; set; } = new List<Movimiento>();
        public List<Equipment> InventarioBajoStock { get; set; } = new List<Equipment>();
        public List<Colmena> ColmenasEnAlerta { get; set; } = new List<Colmena>();

        public List<ProduccionMensual> TendenciaProduccion { get; set; } = new List<ProduccionMensual>();
    }

    public class ProduccionMensual
    {
        public string Mes { get; set; } = string.Empty;
        public double CantidadKg { get; set; }
        public double PorcentajeAltura { get; set; }
        public bool EsMesActual { get; set; }
    }
}
