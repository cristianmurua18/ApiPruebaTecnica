using ApiPruebaTecnica.ApiDOMAIN.DTOs;

namespace ApiPruebaTecnica.ApiDATA.Daos
{
    public interface IDAOSolicitudes
    {
        public Task<EstudioDTO> ProcesarSolicitudAsync(SolicitudDTO solicitudDTO);
    }
}
