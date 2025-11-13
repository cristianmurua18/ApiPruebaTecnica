using System.Data;

namespace ApiPruebaTecnica.ApiDATA.Daos
{
    public class DAOSolicitudes(IDbConnection dbConnection) : IDAOSolicitudes
    {
        private readonly IDbConnection _dbConnection = dbConnection;


    }
}
