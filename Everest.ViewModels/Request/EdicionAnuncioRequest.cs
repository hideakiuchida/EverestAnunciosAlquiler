using System.ComponentModel.DataAnnotations;

namespace Everest.ViewModels.Request
{
    public class EdicionAnuncioRequest : CreacionAnuncioRequest
    {
        [Required]
        public int? IdAnuncio { get; set; }
    }
}
