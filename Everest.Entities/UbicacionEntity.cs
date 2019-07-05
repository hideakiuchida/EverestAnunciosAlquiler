namespace Everest.Entities
{
    public class UbicacionEntity
    {
        public int IdUbicacion { get; set; }
        public int IdAnuncio { get; set; }
        public string Direccion { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
    }
}
