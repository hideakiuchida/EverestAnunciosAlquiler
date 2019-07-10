using Dapper;
using Everest.Entities;
using Everest.Repository.Interfaces;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Everest.Repository.Implementations
{
    public class UbicacionRepository : BaseConnection, IUbicacionRepository
    {
        public UbicacionRepository(IDbConnection dbConnection) : base(dbConnection)
        {
        }

        public async Task<UbicacionEntity> ConsultarPorAnuncioAsync(int id)
        {
            if (_dbConnection.State == ConnectionState.Closed)
                _dbConnection.Open();
            var result = await _dbConnection.QueryAsync<UbicacionEntity>("ConsultarUbicacionPorAnuncio", new { Id = id }, commandType: CommandType.StoredProcedure);
            _dbConnection.Close();
            return result.FirstOrDefault();
        }

        public async Task<int> CrearUbicacionAsync(UbicacionEntity entity)
        {
            if (_dbConnection.State == ConnectionState.Closed)
                _dbConnection.Open();
            var spEntity = new {
                entity.IdAnuncio,
                entity.Direccion,
                Latitud = Math.Round(entity.Latitud, 6),
                Longitud = Math.Round(entity.Longitud, 6) };
            var result = await _dbConnection.QueryAsync<int>("CrearUbicacion", spEntity, commandType: CommandType.StoredProcedure);
            _dbConnection.Close();
            return result.FirstOrDefault();
        }

        public async Task<bool> EditarUbicacionAsync(UbicacionEntity entity)
        {
            if (_dbConnection.State == ConnectionState.Closed)
                _dbConnection.Open();
            var spEntity = new
            {
                entity.IdUbicacion,
                entity.IdAnuncio,
                entity.Direccion,
                entity.Latitud,
                entity.Longitud
            };
            var result = await _dbConnection.QueryAsync<bool>("EditarUbicacion", spEntity, commandType: CommandType.StoredProcedure);
            _dbConnection.Close();
            return result.FirstOrDefault();
        }

        public async Task<bool> EliminarUbicacionAsync(int id)
        {
            if (_dbConnection.State == ConnectionState.Closed)
                _dbConnection.Open();
            var result = await _dbConnection.QueryAsync<bool>("EliminarUbicacion", new { Id = id }, commandType: CommandType.StoredProcedure);
            _dbConnection.Close();
            return result.FirstOrDefault();
        }
    }
}
