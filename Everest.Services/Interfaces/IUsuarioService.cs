using Everest.ViewModels;
using Everest.ViewModels.Response;
using System.Threading.Tasks;

namespace Everest.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<BaseServiceResponse<UsuarioResponse>> ConsultarUsuarioAsync(int idUsuario);
        Task<BaseServiceResponse<UsuarioResponse>> ConsultarUsuarioPorAnuncioAsync(int idAnuncio);
    }
}
