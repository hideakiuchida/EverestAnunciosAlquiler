using Everest.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Everest.Repository.Interfaces
{
    public interface IAnuncioRepository
    {
        Task<List<AnuncioEntity>> ConsultarAnunciosAsync(int? idUsuario);
        Task<AnuncioEntity> ConsultarAsync(int id);
        Task<int> CrearAnuncioAsync(AnuncioEntity entity);
        Task<bool> EditarAnuncioAsync(AnuncioEntity entity);
        Task<bool> EliminarAnuncioAsync(int id);
    }
}
