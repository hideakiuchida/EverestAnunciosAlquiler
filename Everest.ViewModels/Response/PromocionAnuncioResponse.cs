using System;
using System.Collections.Generic;
using System.Text;

namespace Everest.ViewModels.Response
{
    public class PromocionAnuncioResponse
    {
        public int IdPromocionAnuncio { get; set; }
        public int IdAnuncio { get; set; }
        public int IdUsuario { get; set; }
        public bool Agendado { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
