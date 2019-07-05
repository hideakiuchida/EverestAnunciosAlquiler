using System;

namespace Everest.Entities
{
    public class ImagenEntity
    {
        public int IdImagen { get; set; }
        public int IdAnuncio { get; set; }
        public int Descripcion { get; set; }
        public string ImagenUrl { get; set; }
        public string IdPublico { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
