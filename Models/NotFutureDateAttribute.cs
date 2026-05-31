using System;
using System.ComponentModel.DataAnnotations;

namespace ObligatorioIntegrador2026.Models
{
    public class NotFutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                if (value is DateTime dateTime)
                {
                    // Comparamos solo la fecha (día) para evitar discrepancias menores por segundos/milisegundos
                    if (dateTime.Date > DateTime.Today)
                    {
                        return new ValidationResult(ErrorMessage ?? "La fecha no puede ser posterior a la fecha actual.");
                    }
                }
            }
            return ValidationResult.Success;
        }
    }
}
