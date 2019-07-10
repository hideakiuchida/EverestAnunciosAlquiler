using Dapper;
using Everest.Entities;
using Everest.Repository.Interfaces;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Everest.Repository.Implementations
{
    public class PromocionAnuncioRepository : BaseConnection, IPromocionAnuncioRepository
    {
        public PromocionAnuncioRepository(IDbConnection dbConnection) : base(dbConnection)
        {
        }

        public async Task<int> CrearPromocionAnuncioAsync(int idAnuncio)
        {
            try
            {
                if (_dbConnection.State == ConnectionState.Closed)
                    _dbConnection.Open();
                var spEntity = new
                {
                    IdAnuncio = idAnuncio,
                    FechaCreacion = DateTime.UtcNow
                };
                var result = await _dbConnection.QueryAsync<int>("CrearPromocionAnuncio", spEntity, commandType: CommandType.StoredProcedure);
                _dbConnection.Close();
                return result.FirstOrDefault();
            }
            catch (Exception)
            {
                return default;
            }

        }

        public async Task<bool> AgendarPromocionAnuncioAsync(PromocionAnuncioEntity entity)
        {
            try
            {
                if (_dbConnection.State == ConnectionState.Closed)
                    _dbConnection.Open();
                var spEntity = new
                {
                    entity.IdAnuncio,
                    entity.IdUsuario,
                    entity.Agendado
                };
                var result = await _dbConnection.QueryAsync<bool>("AgendarPromocioAnuncio", spEntity, commandType: CommandType.StoredProcedure);
                _dbConnection.Close();
                return result.FirstOrDefault();
            }
            catch (Exception)
            {
                return default;
            }

        }

        public async Task<PromocionAnuncioEntity> ConsultarPromocionAsync()
        {
            try
            {
                if (_dbConnection.State == ConnectionState.Closed)
                    _dbConnection.Open();
                var result = await _dbConnection.QueryAsync<PromocionAnuncioEntity>("ConsultarPromocion", commandType: CommandType.StoredProcedure);
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
