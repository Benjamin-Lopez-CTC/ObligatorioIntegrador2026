using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ObligatorioIntegrador2026.Models
{
    public class Treatment
    {
        public int Id { get; set; }

        [Required]
        public int ColmenaId { get; set; }
        
        public Colmena? Colmena { get; set; }

        [Required(ErrorMessage = "El título de tratamiento es obligatorio.")]
        [StringLength(100)]
        public string Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "El tipo de tratamiento es obligatorio.")]
        [StringLength(50)]
        public string Tipo { get; set; } = string.Empty; // "Mantenimiento", "Medicinal", "Otro"

        public string? Nota { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        public List<TreatmentEquipment> EquipamientosUsados { get; set; } = new List<TreatmentEquipment>();
    }
}
