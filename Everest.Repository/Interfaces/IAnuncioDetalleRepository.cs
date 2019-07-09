using Everest.Entities;
using System.Threading.Tasks;

namespace Everest.Repository.Interfaces
{
    public interface IAnuncioDetalleRepository
    {
        Task<AnuncioDetalleEntity> ConsultarAnuncioDetallePorAnuncioAsync(int id);
        Task<int> CrearAnuncioDetalleAsync(AnuncioDetalleEntity entity);
        Task<bool> EditarAnuncioDetalleAsync(AnuncioDetalleEntity entity);
    }
}
