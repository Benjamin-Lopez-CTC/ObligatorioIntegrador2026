using System.ComponentModel.DataAnnotations;

namespace ObligatorioIntegrador2026.Models
{
    public class EquipmentRegistrationDto : Equipment
    {
        public int? AnalisisId { get; set; }
        public bool CreateNewAnalisis { get; set; }
    }
}
