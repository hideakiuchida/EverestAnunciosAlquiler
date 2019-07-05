using System;

namespace Everest.Entities
{
    public class AnuncioEntity
    {
        public int IdAnuncio { get; set; }
        public int IdUsuario { get; set; }
        public bool AdmiteMascota { get; set; }
        public int IdTipoPropiedad { get; set; }
        public decimal Precio { get; set; }
        public int MaximaCantidadPersonas { get; set; }
        public bool TieneSeguridadPrivada { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
