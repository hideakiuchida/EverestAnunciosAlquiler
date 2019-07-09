using Everest.Entities;
using System.Threading.Tasks;

namespace Everest.Repository.Interfaces
{
    public interface ITipoPropiedadRepository
    {
        Task<TipoPropiedadEntity> ConsultarTipoPropiedadAsync(int id);
    }
}
