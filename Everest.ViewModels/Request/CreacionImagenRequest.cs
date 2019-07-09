using Microsoft.AspNetCore.Http;

namespace Everest.ViewModels.Request
{
    public class CreacionImagenRequest
    {
        public IFormFile Imagen { get; set; }
        public string Descripcion { get; set; } 
    }
}
