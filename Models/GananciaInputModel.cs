using System.ComponentModel.DataAnnotations;

namespace ObligatorioIntegrador2026.Models
{
    public class GananciaInputModel
    {
        [Required]
        public int AnalisisId { get; set; }

        [Required(ErrorMessage = "El título es obligatorio.")]
        [StringLength(100)]
        public string Titulo { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "El monto de ganancia es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto de ganancia debe ser un valor positivo.")]
        public double Monto { get; set; }
    }
}
