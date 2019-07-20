using System.ComponentModel.DataAnnotations;

namespace Everest.ViewModels.Request
{
    public class CreacionAnuncioRequest
    {
        [Required]
        public int? Metros2 { get; set; }
        [Required]
        public int? CantidadHabitaciones { get; set; }
        [Required]
        public int? CantidadBaños { get; set; }
        [Required]
        public int? CantidadParqueos { get; set; }
        [Required]
        public int? Plantas { get; set; }
        [Required]
        public string Direccion { get; set; }
        [Required]
        public decimal? Latitud { get; set; }
        [Required]
        public decimal? Longitud { get; set; }
        [Required]
        public bool? AdmiteMascota { get; set; }
        [Required]
        public int? IdTipoPropiedad { get; set; }
        [Required]
        public decimal? Precio { get; set; }
        [Required]
        public int? MaximaCantidadPersonas { get; set; }
        [Required]
        public bool? TieneSeguridadPrivada { get; set; }
    }
}
