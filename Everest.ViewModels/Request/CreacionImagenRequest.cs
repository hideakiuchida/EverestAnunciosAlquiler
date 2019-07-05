using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Everest.ViewModels.Request
{
    public class CreacionImagenRequest
    {
        public IFormFile Imagen { get; set; }
        public int Descripcion { get; set; }
        public string ImagenUrl { get; set; }
        public string IdPublico { get; set; }     
    }
}
