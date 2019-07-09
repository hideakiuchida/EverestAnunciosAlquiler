using Everest.ViewModels;
using Everest.ViewModels.Request;
using System.Threading.Tasks;

namespace Everest.Services.Interfaces
{
    public interface IEvaluacionService
    {
        Task<BaseServiceResponse<int>> CrearEvaluacionAsync(int idAnuncio, CreacionEvaluacionRequest request);
    }
}
