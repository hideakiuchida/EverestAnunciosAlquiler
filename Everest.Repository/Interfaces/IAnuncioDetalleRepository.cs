using Everest.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Everest.Repository.Interfaces
{
    public interface IAnuncioDetalleRepository
    {
        Task<AnuncioDetalleEntity> ConsultarPorAnuncioDetalleAsync(int id);
        Task<int> CrearAnuncioDetalleAsync(AnuncioDetalleEntity entity);
        Task<bool> EditarAnuncioDetalleAsync(AnuncioDetalleEntity entity);
    }
}
