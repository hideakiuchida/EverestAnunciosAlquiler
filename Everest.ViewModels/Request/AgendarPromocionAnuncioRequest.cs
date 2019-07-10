using System;
using System.Collections.Generic;
using System.Text;

namespace Everest.ViewModels.Request
{
    public class AgendarPromocionAnuncioRequest
    {
        public int IdAnuncio { get; set; }
        public int IdUsuario { get; set; }
        public bool Agendado { get; set; }
    }
}
