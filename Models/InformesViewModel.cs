using System;
using System.Collections.Generic;

namespace ObligatorioIntegrador2026.Models
{
    public class InformesViewModel
    {
        public DateTime? FechaUltimaDeclaracion { get; set; }
        public DateTime? FechaProximaDeclaracion { get; set; }
        public int DiasRestantes { get; set; }

        public int TotalColmenasPropiedad { get; set; }
        public int TotalNucleos { get; set; }

        public List<ApiarioInformeItem> Apiarios { get; set; } = new List<ApiarioInformeItem>();
    }

    public class ApiarioInformeItem
    {
        public int Id { get; set; }
        public string Nro { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Departamento { get; set; } = string.Empty;
        public string SeccionPolicial { get; set; } = string.Empty;
        public string Paraje { get; set; } = string.Empty;
        public int CantidadColmenas { get; set; }
        public int CantidadNucleos { get; set; }
        public string Coordenadas { get; set; } = string.Empty;
    }
}
