using System.ComponentModel.DataAnnotations;

namespace ObligatorioIntegrador2026.Models
{
    public class Equipment
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "La descripción/nombre es obligatoria.")]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "El tipo es obligatorio.")]
        [StringLength(50)]
        public string Type { get; set; } = string.Empty;

        [Required(ErrorMessage = "El stock es obligatorio.")]
        [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo.")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "La categoría es obligatoria.")]
        [StringLength(20)]
        public string Category { get; set; } = string.Empty; // Material, Herramienta, Medicamento, Otro

        [Required(ErrorMessage = "El umbral bajo es obligatorio.")]
        [Range(0, int.MaxValue, ErrorMessage = "El umbral bajo no puede ser negativo.")]
        public int LowThreshold { get; set; }

        [Required(ErrorMessage = "El umbral medio es obligatorio.")]
        [Range(0, int.MaxValue, ErrorMessage = "El umbral medio no puede ser negativo.")]
        public int MediumThreshold { get; set; }

        public int DisplayOrder { get; set; }

        public string Status
        {
            get
            {
                if (Stock <= LowThreshold)
                    return "Bajo";
                if (Stock <= MediumThreshold)
                    return "Medio";
                return "Alto";
            }
        }
    }
}
