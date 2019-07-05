namespace Everest.Entities
{
    public class AnuncioDetalleEntity
    {
        public int IdAnuncioDetalle { get; set; }
        public int IdAnuncio { get; set; }
        public decimal Metros2 { get; set; }
        public int CantidadHabitaciones { get; set; }
        public int CantidadBaños { get; set; }
        public int CantidadParqueos { get; set; }
        public int Plantas { get; set; }
    }
}
