using System;
using System.Collections.Generic;
using System.Text;

namespace Everest.ViewModels.Request
{
    public class ActivarAnuncioRequest
    {
        public int IdAnuncio { get; set; }
        public bool EsActivo { get; set; }
    }
}
