using Dapper;
using Everest.Entities;
using Everest.Repository.Interfaces;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Everest.Repository.Implementations
{
    public class TipoPropiedadRepository : BaseConnection, ITipoPropiedadRepository
    {
        public TipoPropiedadRepository(IDbConnection dbConnection) : base(dbConnection)
        {
        }

        public async Task<TipoPropiedadEntity> ConsultarTipoPropiedadAsync(int id)
        {
            if (_dbConnection.State == ConnectionState.Closed)
                _dbConnection.Open();
            var result = await _dbConnection.QueryAsync<TipoPropiedadEntity>("ConsultarTipoPropiedad", new { Id = id }, commandType: CommandType.StoredProcedure);
            _dbConnection.Close();
            return result.FirstOrDefault();
        }
    }
}
