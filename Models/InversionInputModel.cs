using System.ComponentModel.DataAnnotations;

namespace ObligatorioIntegrador2026.Models
{
    public class InversionInputModel
    {
        [Required]
        public int AnalisisId { get; set; }

        [Required(ErrorMessage = "El título es obligatorio.")]
        [StringLength(100)]
        public string Titulo { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Nota { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser un valor positivo.")]
        public double Precio { get; set; }
    }
}
