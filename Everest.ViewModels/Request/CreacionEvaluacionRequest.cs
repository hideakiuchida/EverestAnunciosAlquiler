using System.ComponentModel.DataAnnotations;

namespace Everest.ViewModels.Request
{
    public class CreacionEvaluacionRequest
    {
        [Required]
        [StringLength(300, ErrorMessage = "La longitud del comentario no puede ser mayor de 300.")]
        public string Comentario { get; set; }
        [Required]
        [Range(1, 5, ErrorMessage = "Los valores de calificación solo puede estar dentro del rango de 1 a 5.")]
        public int? Calificacion { get; set; }
    }
}
