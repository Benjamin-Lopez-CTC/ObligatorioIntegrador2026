using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ObligatorioIntegrador2026.Models
{
    public class Inversion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int AnalisisId { get; set; }

        [ForeignKey("AnalisisId")]
        public Analisis? Analisis { get; set; }

        [Required(ErrorMessage = "El título es obligatorio.")]
        [StringLength(100, ErrorMessage = "El título no puede superar los 100 caracteres.")]
        public string Titulo { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "La nota no puede superar los 500 caracteres.")]
        public string Nota { get; set; } = string.Empty;

        [Required(ErrorMessage = "El precio es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser un valor positivo.")]
        public double Precio { get; set; }
    }
}
