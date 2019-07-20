using System.ComponentModel.DataAnnotations;

namespace Everest.ViewModels.Request
{
    public class ActivarAnuncioRequest
    {
        [Required]
        public int? IdAnuncio { get; set; }
        [Required]
        public bool? EsActivo { get; set; }
    }
}
