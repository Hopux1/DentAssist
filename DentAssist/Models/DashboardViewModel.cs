using System.Collections.Generic;

namespace DentAssist.Models
{
    public class DashboardViewModel
    {
        public int TotalPacientes { get; set; }
        public int TotalOdontologos { get; set; }
        public int TotalTurnos { get; set; }
        public int TotalTratamientos { get; set; }

        public Dictionary<string, int> TratamientosMasUsados { get; set; } = new();
        public List<PlanTratamiento> UltimosPlanes { get; set; } = new();
    }
}
