using System.ComponentModel.DataAnnotations;

namespace DentAssist.Models
{
    public class Odontologo
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La matrícula es obligatoria.")]
        public string Matricula { get; set; }

        [Required(ErrorMessage = "La especialidad es obligatoria.")]
        public string Especialidad { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El correo electrónico no tiene un formato válido.")]
        public string Email { get; set; }

        public ICollection<Turno> Turnos { get; set; } = new List<Turno>();
        public ICollection<PlanTratamiento> PlanesTratamiento { get; set; } = new List<PlanTratamiento>();
    }
}
