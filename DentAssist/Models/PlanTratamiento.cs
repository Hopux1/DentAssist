using System.ComponentModel.DataAnnotations;

namespace DentAssist.Models
{
    public class PlanTratamiento
    {
        public int Id { get; set; }

        [StringLength(500, ErrorMessage = "Las observaciones no deben exceder los 500 caracteres.")]
        public string? Observaciones { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un paciente.")]
        [Display(Name = "Paciente")]
        public int PacienteId { get; set; }
        public Paciente? Paciente { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un odontólogo.")]
        [Display(Name = "Odontólogo")]
        public int OdontologoId { get; set; }
        public Odontologo? Odontologo { get; set; }

        [Required(ErrorMessage = "Debe indicar el estado del plan.")]
        [StringLength(50)]
        public string Estado { get; set; } = "Activo";

        public ICollection<PasoTratamiento> Pasos { get; set; } = new List<PasoTratamiento>();
    }
}
