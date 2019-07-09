using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Everest.ViewModels.Request
{
    public class CreacionImagenRequest
    {
        public IFormFile Imagen { get; set; }
        public string Descripcion { get; set; } 
    }
}
