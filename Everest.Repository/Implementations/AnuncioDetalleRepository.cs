using Dapper;
using Everest.Entities;
using Everest.Repository.Interfaces;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Everest.Repository.Implementations
{
    public class AnuncioDetalleRepository : BaseConnection, IAnuncioDetalleRepository
    {
        public AnuncioDetalleRepository(IDbConnection dbConnection) : base(dbConnection)
        {
        }

        public async Task<AnuncioDetalleEntity> ConsultarAnuncioDetallePorAnuncioAsync(int id)
        {
            if(_dbConnection.State == ConnectionState.Closed)
                _dbConnection.Open();
            var result = await _dbConnection.QueryAsync<AnuncioDetalleEntity>("ConsultarAnuncioDetallePorAnuncio", new { Id = id }, commandType: CommandType.StoredProcedure);
            _dbConnection.Close();
            return result.FirstOrDefault();
        }

        public async Task<int> CrearAnuncioDetalleAsync(AnuncioDetalleEntity entity)
        {
            if (_dbConnection.State == ConnectionState.Closed)
                _dbConnection.Open();
            var spEntity = new
            {
                entity.IdAnuncio,
                entity.Metros2,
                entity.CantidadBaños,
                entity.CantidadHabitaciones,
                entity.CantidadParqueos,
                entity.Plantas
            };
            var result = await _dbConnection.QueryAsync<int>("CrearAnuncioDetalle", spEntity, commandType: CommandType.StoredProcedure);
            _dbConnection.Close();
            return result.FirstOrDefault();
        }

        public async Task<bool> EditarAnuncioDetalleAsync(AnuncioDetalleEntity entity)
        {
            if (_dbConnection.State == ConnectionState.Closed)
                _dbConnection.Open();
            var spEntity = new
            {
                entity.IdAnuncioDetalle,
                entity.IdAnuncio,
                entity.Metros2,
                entity.CantidadBaños,
                entity.CantidadHabitaciones,
                entity.CantidadParqueos,
                entity.Plantas
            };
            var result = await _dbConnection.QueryAsync<bool>("EditarAnuncioDetalle", spEntity, commandType: CommandType.StoredProcedure);
            _dbConnection.Close();
            return result.FirstOrDefault();
        }
    }
}
