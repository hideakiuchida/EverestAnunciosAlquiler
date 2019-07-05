using System;
using System.Collections.Generic;
using System.Text;

namespace Everest.ViewModels.Response
{
    public class EvaluacionResponse
    {
        public int IdEvaluacion { get; set; }
        public int IdAnuncio { get; set; }
        public string Comentario { get; set; }
        public int Calificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
