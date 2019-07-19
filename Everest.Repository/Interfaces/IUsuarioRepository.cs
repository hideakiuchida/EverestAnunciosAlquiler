using Everest.Entities;
using System.Threading.Tasks;

namespace Everest.Repository.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<UsuarioEntity> ConsultarUsuarioAsync(string id);
        Task<UsuarioEntity> ConsultarUsuarioPorAnuncioAsync(int id);
    }
}
