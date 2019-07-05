using Everest.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Everest.Repository.Interfaces
{
    public interface IImagenRepository
    {
        Task<List<ImagenEntity>> ConsultarPorAnuncioAsync(int id);
        Task<int> CrearImagenAsync(ImagenEntity entity);
        Task<bool> EliminarImagenAsync(int id);
    }
}
