using Dapper;
using Everest.Entities;
using Everest.Repository.Interfaces;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Everest.Repository.Implementations
{
    public class UsuarioRepository : BaseConnection, IUsuarioRepository
    {
        public UsuarioRepository(IDbConnection dbConnection) : base(dbConnection)
        {
        }

        public async Task<UsuarioEntity> ConsultarUsuarioAsync(int id)
        {
            if (_dbConnection.State == ConnectionState.Closed)
                _dbConnection.Open();
            var result = await _dbConnection.QueryAsync<UsuarioEntity>("ConsultarUsuario", new { Id = id }, commandType: CommandType.StoredProcedure);
            _dbConnection.Close();
            return result.FirstOrDefault();
        }

        public async Task<UsuarioEntity> ConsultarUsuarioPorAnuncioAsync(int id)
        {
            if (_dbConnection.State == ConnectionState.Closed)
                _dbConnection.Open();
            var result = await _dbConnection.QueryAsync<UsuarioEntity>("ConsultarUsuarioPorAnuncio", new { Id = id }, commandType: CommandType.StoredProcedure);
            _dbConnection.Close();
            return result.FirstOrDefault();
        }
    }
}
