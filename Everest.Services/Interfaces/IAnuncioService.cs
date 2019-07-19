using Everest.ViewModels;
using Everest.ViewModels.Request;
using Everest.ViewModels.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Everest.Services.Interfaces
{
    public interface IAnuncioService
    {
        Task<BaseServiceResponse<int>> CrearAsync(int idUsuario, CreacionAnuncioRequest request);
        Task<BaseServiceResponse<bool>> EditarAsync(int idUsuario, EdicionAnuncioRequest request);
        Task<BaseServiceResponse<bool>> EliminarAsync(int idUsuario, int id);
        Task<BaseServiceResponse<IEnumerable<AnuncioResponse>>> ConsultarPorUsuarioAsync(int idUsuario);
        Task<BaseServiceResponse<bool>> ActivarAnuncioAsync(int id, bool esActivo);
    }
}
