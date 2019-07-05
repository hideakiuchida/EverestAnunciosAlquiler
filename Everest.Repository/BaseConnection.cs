using System.Data;
namespace Everest.Repository
{
    public class BaseConnection
    {
        protected readonly IDbConnection _dbConnection;
        public BaseConnection(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
    }
}
