using Everest.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Everest.Repository.Interfaces
{
    public interface IEvaluacionRepository
    {
        Task<List<EvaluacionEntity>> ConsultarPorAnuncioAsync(int id);
        Task<int> CrearEvaluacionAsync(EvaluacionEntity entity);
    }
}
