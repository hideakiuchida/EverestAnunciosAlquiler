using Everest.Entities;
using System.Threading.Tasks;

namespace Everest.Repository.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<UsuarioEntity> ConsultarUsuarioAsync(int id);
    }
}
