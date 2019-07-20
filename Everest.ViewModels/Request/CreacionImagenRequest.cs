using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Everest.ViewModels.Request
{
    public class CreacionImagenRequest
    {
        [Required]
        public IFormFile Imagen { get; set; }
        public string Descripcion { get; set; } 
    }
}
