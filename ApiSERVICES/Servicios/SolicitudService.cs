
using ApiPruebaTecnica.ApiDATA.Daos;
using ApiPruebaTecnica.ApiDOMAIN.DTOs;

namespace ApiPruebaTecnica.ApiSERVICES.Servicios
{
    public class SolicitudService(IDAOSolicitudes daoSolicitudes) : ISolicitudService
    {
        private readonly IDAOSolicitudes _daoSolicitudes = daoSolicitudes;
        public async Task<EstudioDTO> ProcesarSolicitudAsync(SolicitudDTO solicitud)
        {
            if (solicitud.SolicitudId > 0)
            {
                var respuesta = await _daoSolicitudes.ProcesarSolicitudAsync(solicitud);
                
                if (respuesta != null)
                    return respuesta;
                else
                    return null;
            }
            //La solicitud tiene un formato incorrecto
            else
                return null;
        }
    }
}
