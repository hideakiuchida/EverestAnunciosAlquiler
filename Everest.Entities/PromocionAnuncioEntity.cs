using System;
namespace Everest.Entities
{
    public class PromocionAnuncioEntity
    {
        public int IdPromocionAnuncio { get; set; }
        public int IdAnuncio { get; set; }
        public int IdUsuario { get; set; }
        public bool Agendado { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
