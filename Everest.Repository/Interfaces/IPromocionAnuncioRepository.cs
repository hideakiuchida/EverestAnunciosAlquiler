using Everest.Entities;
using System.Threading.Tasks;

namespace Everest.Repository.Interfaces
{
    public interface IPromocionAnuncioRepository
    {
        Task<PromocionAnuncioEntity> ConsultarPromocionAsync();
        Task<int> CrearPromocionAnuncioAsync(int idAnuncio);
        Task<bool> AgendarPromocionAnuncioAsync(PromocionAnuncioEntity entity);
    }
}
