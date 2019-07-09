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
    public class EvaluacionRepository : BaseConnection, IEvaluacionRepository
    {
        public EvaluacionRepository(IDbConnection dbConnection) : base(dbConnection)
        {
        }

        public async Task<List<EvaluacionEntity>> ConsultarPorAnuncioAsync(int id)
        {
            if (_dbConnection.State == ConnectionState.Closed)
                _dbConnection.Open();
            var result = await _dbConnection.QueryAsync<EvaluacionEntity>("ConsultarEvaluacionesPorAnuncio", new { Id = id }, commandType: CommandType.StoredProcedure);
            _dbConnection.Close();
            return result.ToList();
        }

        public async Task<int> CrearEvaluacionAsync(EvaluacionEntity entity)
        {
            if (_dbConnection.State == ConnectionState.Closed)
                _dbConnection.Open();
            var spEntity = new
            {
                entity.IdAnuncio,
                entity.Comentario,
                entity.Calificacion,
                FechaCreacion = DateTime.UtcNow
            };
            var result = await _dbConnection.QueryAsync<int>("CrearEvaluacion", spEntity, commandType: CommandType.StoredProcedure);
            _dbConnection.Close();
            return result.FirstOrDefault();
        }
    }
}
