using System;

namespace Everest.Entities
{
    public class EvaluacionEntity
    {
        public int IdEvaluacion { get; set; }
        public int IdAnuncio { get; set; }
        public string Comentario { get; set; }
        public int Calificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
