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
    public class AnuncioRepository : BaseConnection, IAnuncioRepository
    {
        public AnuncioRepository(IDbConnection dbConnection) : base(dbConnection)
        {
        }

        public async Task<AnuncioEntity> ConsultarAsync(int id)
        {
            if (_dbConnection.State == ConnectionState.Closed)
                _dbConnection.Open();
            var result = await _dbConnection.QueryAsync<AnuncioEntity>("ConsultarAnuncio", new { Id = id }, commandType: CommandType.StoredProcedure);
            _dbConnection.Close();
            return result.FirstOrDefault();
        }

        public async Task<List<AnuncioEntity>> ConsultarAnunciosAsync(int? idUsuario)
        {
            if (_dbConnection.State == ConnectionState.Closed)
                _dbConnection.Open();
            var result = await _dbConnection.QueryAsync<AnuncioEntity>("ConsultarAnuncios", new { IdUsuario = idUsuario }, commandType: CommandType.StoredProcedure);
            _dbConnection.Close();
            return result.ToList();
        }

        public async Task<int> CrearAnuncioAsync(AnuncioEntity entity)
        {
            if (_dbConnection.State == ConnectionState.Closed)
                _dbConnection.Open();
            var spEntity = new
            {
                entity.IdUsuario,
                entity.AdmiteMascota,
                entity.IdTipoPropiedad,
                entity.MaximaCantidadPersonas,
                entity.Precio,
                entity.TieneSeguridadPrivada,
                entity.Activo,
                entity.FechaCreacion
            };
            var result = await _dbConnection.QueryAsync<int>("CrearAnuncio", spEntity, commandType: CommandType.StoredProcedure);
            _dbConnection.Close();
            return result.FirstOrDefault();
        }

        public async Task<bool> EditarAnuncioAsync(AnuncioEntity entity)
        {
            if (_dbConnection.State == ConnectionState.Closed)
                _dbConnection.Open();
            var spEntity = new
            {
                entity.IdAnuncio,
                entity.IdUsuario,
                entity.AdmiteMascota,
                entity.IdTipoPropiedad,
                entity.MaximaCantidadPersonas,
                entity.Precio,
                entity.TieneSeguridadPrivada,
                entity.Activo,
                entity.FechaCreacion
            };
            var result = await _dbConnection.QueryAsync<bool>("EditarAnuncio", spEntity, commandType: CommandType.StoredProcedure);
            _dbConnection.Close();
            return result.FirstOrDefault();
        }

        public async Task<bool> EliminarAnuncioAsync(int id)
        {
            if (_dbConnection.State == ConnectionState.Closed)
                _dbConnection.Open();
            var result = await _dbConnection.QueryAsync<bool>("EliminarAnuncio", new { Id = id }, commandType: CommandType.StoredProcedure);
            _dbConnection.Close();
            return result.FirstOrDefault();
        }
    }
}
