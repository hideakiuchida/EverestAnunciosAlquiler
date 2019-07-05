using System;
using System.Collections.Generic;
using System.Text;

namespace Everest.ViewModels.Request
{
    public class CreacionEvaluacionRequest
    {
        public int IdAnuncio { get; set; }
        public string Comentario { get; set; }
        public int Calificacion { get; set; }
    }
}
