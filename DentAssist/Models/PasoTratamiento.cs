using System.ComponentModel.DataAnnotations;

namespace DentAssist.Models
{
    public class PasoTratamiento
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un tratamiento.")]
        [Display(Name = "Tratamiento")]
        public int TratamientoId { get; set; }
        public Tratamiento? Tratamiento { get; set; }

        [Required(ErrorMessage = "Debe indicar la fecha estimada.")]
        [Display(Name = "Fecha Estimada")]
        public DateTime FechaEstimada { get; set; }

        [Required(ErrorMessage = "Debe indicar el estado.")]
        [StringLength(100, ErrorMessage = "El estado no debe exceder los 100 caracteres.")]
        public string Estado { get; set; } = "Pendiente";

        [StringLength(500, ErrorMessage = "Las observaciones no deben exceder los 500 caracteres.")]
        [Required(ErrorMessage = "Debe hacer una observación.")]
        public string? Observaciones { get; set; }

        [Required(ErrorMessage = "Debe asociar un plan de tratamiento.")]
        [Display(Name = "Plan de Tratamiento")]
        public int PlanTratamientoId { get; set; }
        public PlanTratamiento? PlanTratamiento { get; set; }
    }
}
