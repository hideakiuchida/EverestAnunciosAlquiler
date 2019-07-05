using Everest.ViewModels;
using Everest.ViewModels.Request;
using Everest.ViewModels.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Everest.Services.Interfaces
{
    public interface IAnuncioService
    {
        Task<BaseServiceResponse<int>> CrearAsync(CreacionAnuncioRequest request);
        Task<BaseServiceResponse<bool>> EditarAsync(EdicionAnuncioRequest request);
        Task<BaseServiceResponse<bool>> EliminarAsync(int id);
        Task<BaseServiceResponse<IEnumerable<AnuncioResponse>>> ConsultarPorUsuarioAsync(int idUsuario);
    }
}
