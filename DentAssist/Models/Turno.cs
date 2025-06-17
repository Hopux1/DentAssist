using System.ComponentModel.DataAnnotations;

namespace DentAssist.Models
{
    public class Turno
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La fecha y hora son obligatorias.")]
        public DateTime FechaHora { get; set; }

        [Required(ErrorMessage = "La duración es obligatoria.")]
        [Range(1, 480, ErrorMessage = "La duración debe estar entre 1 y 480 minutos.")]
        public int DuracionMinutos { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio.")]
        public string Estado { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un paciente.")]
        [Display(Name = "Paciente")]
        public int PacienteId { get; set; }
        public Paciente? Paciente { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un odontólogo.")]
        [Display(Name = "Odontólogo")]
        public int OdontologoId { get; set; }
        public Odontologo? Odontologo { get; set; }
    }
}
