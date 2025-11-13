using ApiPruebaTecnica.ApiDOMAIN.DTOs;

namespace ApiPruebaTecnica.ApiSERVICES.Servicios
{
    public interface ISolicitudService
    {
        public Task<EstudioDTO> ProcesarSolicitudAsync(SolicitudDTO solicitud);

    }
}
