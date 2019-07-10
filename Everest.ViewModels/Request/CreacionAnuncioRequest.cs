namespace Everest.ViewModels.Request
{
    public class CreacionAnuncioRequest
    {
        public int IdUsuario { get; set; }
        public int Metros2 { get; set; }
        public int CantidadHabitaciones { get; set; }
        public int CantidadBaños { get; set; }
        public int CantidadParqueos { get; set; }
        public int Plantas { get; set; }
        public string Direccion { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public bool AdmiteMascota { get; set; }
        public int IdTipoPropiedad { get; set; }
        public decimal Precio { get; set; }
        public int MaximaCantidadPersonas { get; set; }
        public bool TieneSeguridadPrivada { get; set; }
    }
}
