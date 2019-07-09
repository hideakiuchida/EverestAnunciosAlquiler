using Everest.ViewModels;
using Everest.ViewModels.Request;
using System.Threading.Tasks;

namespace Everest.Services.Interfaces
{
    public interface IImagenService
    {
        Task<BaseServiceResponse<int>> CrearImagenAsync(int idAnuncio, CreacionImagenRequest request);
        Task<BaseServiceResponse<bool>> EliminarAsync(int idAnuncio, int id);
    }
}
