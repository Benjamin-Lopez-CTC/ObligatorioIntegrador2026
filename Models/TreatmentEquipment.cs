using System.ComponentModel.DataAnnotations;

namespace ObligatorioIntegrador2026.Models
{
    public class TreatmentEquipment
    {
        public int Id { get; set; }

        [Required]
        public int TreatmentId { get; set; }
        
        public Treatment? Treatment { get; set; }

        [Required]
        [StringLength(100)]
        public string EquipmentName { get; set; } = string.Empty;

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a cero.")]
        public int Cantidad { get; set; }
    }
}
