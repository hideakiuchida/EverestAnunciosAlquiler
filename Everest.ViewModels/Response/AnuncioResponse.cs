using System;
using System.Collections.Generic;
using System.Text;

namespace Everest.ViewModels.Response
{
    public class AnuncioResponse
    {
        public int IdAnuncio { get; set; }
        public UsuarioResponse Usuario { get; set; }
        public decimal Metros2 { get; set; }
        public int CantidadHabitaciones { get; set; }
        public int CantidadBaños { get; set; }
        public int CantidadParqueos { get; set; }
        public int Plantas { get; set; }
        public string Direccion { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public bool AdmiteMascota { get; set; }
        public TipoPropiedadResponse TipoPropiedad { get; set; }
        public decimal Precio { get; set; }
        public int MaximaCantidadPersonas { get; set; }
        public bool TieneSeguridadPrivada { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public IEnumerable<ImagenResponse> Imagenes { get; set; }
        public IEnumerable<EvaluacionResponse> Evaluaciones { get; set; }
    }
}
