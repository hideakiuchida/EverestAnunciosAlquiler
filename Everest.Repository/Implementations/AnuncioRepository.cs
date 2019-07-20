using Dapper;
using Everest.Entities;
using Everest.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
            try
            {
                if (_dbConnection.State == ConnectionState.Closed)
                    _dbConnection.Open();
                var result = await _dbConnection.QueryAsync<AnuncioEntity>("ConsultarAnuncio", new { Id = id }, commandType: CommandType.StoredProcedure);
                _dbConnection.Close();
                return result.FirstOrDefault();
            }
            catch (Exception)
            {
                return default;
            }
            
        }

        public async Task<List<AnuncioEntity>> ConsultarAnunciosAsync(string idUsuario)
        {
            try
            {
                if (_dbConnection.State == ConnectionState.Closed)
                    _dbConnection.Open();
                var result = await _dbConnection.QueryAsync<AnuncioEntity>("ConsultarAnuncios", new { IdUsuario = idUsuario }, commandType: CommandType.StoredProcedure);
                _dbConnection.Close();
                return result.ToList();
            }
            catch (Exception)
            {
                return default;
            }
            
        }

        public async Task<int> CrearAnuncioAsync(AnuncioEntity entity)
        {
            try
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
                    FechaCreacion = DateTime.UtcNow
                };
                var result = await _dbConnection.QueryAsync<int>("CrearAnuncio", spEntity, commandType: CommandType.StoredProcedure);
                _dbConnection.Close();
                return result.FirstOrDefault();
            }
            catch (Exception)
            {
                return default;
            }
            
        }

        public async Task<bool> EditarAnuncioAsync(AnuncioEntity entity)
        {
            try
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
                    entity.TieneSeguridadPrivada
                };
                var result = await _dbConnection.QueryAsync<bool>("EditarAnuncio", spEntity, commandType: CommandType.StoredProcedure);
                _dbConnection.Close();
                return result.FirstOrDefault();
            }
            catch (Exception)
            {
                return default;
            }
            
        }

        public async Task<bool> EliminarAnuncioAsync(int id)
        {
            try
            {
                if (_dbConnection.State == ConnectionState.Closed)
                    _dbConnection.Open();
                var result = await _dbConnection.QueryAsync<bool>("EliminarAnuncio", new { Id = id }, commandType: CommandType.StoredProcedure);
                _dbConnection.Close();
                return result.FirstOrDefault();
            }
            catch (Exception)
            {
                return default;
            }
            
        }

        public async Task<AnuncioEntity> ConsultarAnuncioMasAntiguoAsync()
        {
            try
            {
                if (_dbConnection.State == ConnectionState.Closed)
                    _dbConnection.Open();
                var result = await _dbConnection.QueryAsync<AnuncioEntity>("ConsultarAnuncioMasAntiguo", commandType: CommandType.StoredProcedure);
                _dbConnection.Close();
                return result.FirstOrDefault();
            }
            catch (Exception)
            {
                return default;
            }
        }

        public async Task<bool> ActivarAnuncioAsync(int id, bool esActivo)
        {
            try
            {
                if (_dbConnection.State == ConnectionState.Closed)
                    _dbConnection.Open();
                var result = await _dbConnection.QueryAsync<bool>("ActivarAnuncio", new { IdAnuncio = id, Activo = esActivo }, commandType: CommandType.StoredProcedure);
                _dbConnection.Close();
                return result.FirstOrDefault();
            }
            catch (Exception)
            {
                return default;
            }
        }
    }
}
