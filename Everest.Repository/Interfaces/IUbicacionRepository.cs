using Everest.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Everest.Repository.Interfaces
{
    public interface IUbicacionRepository
    {
        Task<UbicacionEntity> ConsultarPorAnuncioAsync(int id);
        Task<int> CrearUbicacionAsync(UbicacionEntity entity);
        Task<bool> EditarUbicacionAsync(UbicacionEntity entity);
        Task<bool> EliminarUbicacionAsync(int id);
    }
}
