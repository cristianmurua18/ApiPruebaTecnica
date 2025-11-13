
using ApiPruebaTecnica.ApiDATA.Daos;
using ApiPruebaTecnica.ApiDOMAIN.DTOs;

namespace ApiPruebaTecnica.ApiSERVICES.Servicios
{
    public class SolicitudService(IDAOSolicitudes daoSolicitudes) : ISolicitudService
    {
        private readonly IDAOSolicitudes _daoSolicitudes = daoSolicitudes;
        public async Task<EstudioDTO> ProcesarSolicitudAsync(SolicitudDTO solicitud)
        {
            var respuesta = await _daoSolicitudes.ProcesarSolicitudAsync(solicitud);
            return respuesta;
        }
    }
}
