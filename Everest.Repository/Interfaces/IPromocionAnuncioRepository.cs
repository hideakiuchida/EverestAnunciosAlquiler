using Everest.Entities;
using System.Threading.Tasks;

namespace Everest.Repository.Interfaces
{
    public interface IPromocionAnuncioRepository
    {
        Task<int> CrearPromocionAnuncioAsync(int idAnuncio);
        Task<bool> AgendarPromocioAnuncioAsync(PromocionAnuncioEntity entity);
    }
}
