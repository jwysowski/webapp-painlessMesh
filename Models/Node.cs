using System.ComponentModel.DataAnnotations;

namespace Listener.Models
{
    public class Node
    {
        public int Id { get; set; }

        [Display(Name = "Node Id")]
        public string NodeId { get; set; } = string.Empty;

        [Display(Name = "Temperature [°C]")]
        public double Temperature { get; set; }

        [Display(Name = "Humidity [% RH]")]
        public double Humidity { get; set; }

        [Display(Name = "Last Update")]
        [DataType(DataType.Date)]
        public DateTime LastUpdate { get; set; }

        [Display(Name = "Manual mode")]
        public bool InManualMode { get; set; } = false;

        [Display(Name = "Target temperature")]
        public double? TargetTemperature { get; set; }

        [Display(Name = "Target humidity")]
        public double? TargetHumidity { get; set; }
        public bool Arch { get; set; }
    }
}