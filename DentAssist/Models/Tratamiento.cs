using System.ComponentModel.DataAnnotations;

namespace DentAssist.Models
{
    public class Tratamiento
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no debe exceder los 100 caracteres.")]
        public string Nombre { get; set; }

        [StringLength(500, ErrorMessage = "La descripción no debe exceder los 500 caracteres.")]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio.")]
        [Range(0.0, 999999.99, ErrorMessage = "El precio debe ser un número positivo.")]
        public decimal Precio { get; set; }

        public ICollection<PasoTratamiento>? Pasos { get; set; }
    }
}
