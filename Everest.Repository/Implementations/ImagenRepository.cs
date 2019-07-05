using Dapper;
using Everest.Entities;
using Everest.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Everest.Repository.Implementations
{
    public class ImagenRepository : BaseConnection, IImagenRepository
    {
        public ImagenRepository(IDbConnection dbConnection) : base(dbConnection)
        {
        }

        public async Task<List<ImagenEntity>> ConsultarPorAnuncioAsync(int id)
        {
            if (_dbConnection.State == ConnectionState.Closed)
                _dbConnection.Open();
            var result = await _dbConnection.QueryAsync<ImagenEntity>("ConsultarImagenesPorAnuncio", new { Id = id }, commandType: CommandType.StoredProcedure);
            _dbConnection.Close();
            return result.ToList();
        }

        public async Task<int> CrearImagenAsync(ImagenEntity entity)
        {
            if (_dbConnection.State == ConnectionState.Closed)
                _dbConnection.Open();
            var spEntity = new
            {
                entity.IdAnuncio,
                entity.Descripcion,
                entity.ImagenUrl,
                entity.IdPublico,
                entity.FechaCreacion
            };
            var result = await _dbConnection.QueryAsync<int>("CrearImagen", spEntity, commandType: CommandType.StoredProcedure);
            _dbConnection.Close();
            return result.FirstOrDefault();
        }

        public async Task<bool> EliminarImagenAsync(int id)
        {
            if (_dbConnection.State == ConnectionState.Closed)
                _dbConnection.Open();
            var result = await _dbConnection.QueryAsync<bool>("EliminarImagen", new { Id = id }, commandType: CommandType.StoredProcedure);
            _dbConnection.Close();
            return result.FirstOrDefault();
        }
    }
}
