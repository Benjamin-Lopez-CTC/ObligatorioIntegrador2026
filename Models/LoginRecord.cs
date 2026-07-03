using System;
using System.ComponentModel.DataAnnotations;

namespace ObligatorioIntegrador2026.Models
{
    public class LoginRecord
    {
        [Key]
        public int Id { get; set; }

        public DateTime AttemptDate { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public DateTime AttemptDateMontevideo
        {
            get
            {
                DateTime utcTime = AttemptDate.ToUniversalTime();
                try
                {
                    string tzId = OperatingSystem.IsWindows() ? "Montevideo Standard Time" : "America/Montevideo";
                    var tz = TimeZoneInfo.FindSystemTimeZoneById(tzId);
                    return TimeZoneInfo.ConvertTimeFromUtc(utcTime, tz);
                }
                catch
                {
                    return utcTime.AddHours(-3);
                }
            }
        }

        [MaxLength(50)]
        public string Username { get; set; } = string.Empty;

        [MaxLength(50)]
        public string IpAddress { get; set; } = string.Empty;

        [MaxLength(200)]
        public string DeviceBrowser { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Location { get; set; } = string.Empty;

        public bool IsSuccess { get; set; }
    }
}
