using System.ComponentModel.DataAnnotations;

namespace Everest.ViewModels.Request
{
    public class AgendarPromocionAnuncioRequest
    {
        [Required]
        public int? IdAnuncio { get; set; }
        [Required]
        public bool? Agendado { get; set; }
    }
}
