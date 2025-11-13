using ApiPruebaTecnica.ApiDOMAIN.DTOs;
using ApiPruebaTecnica.ApiDOMAIN.Entities;

namespace ApiDOMAIN.Entities
{
    public class Solicitud
    {
        public int SolicitudId { get; set; }
        public Paciente Paciente { get; set; }
        public Estudio Estudio { get; set; }
        public Medico Medico { get; set; }
        public int PrestadorId { get; set; }
    }
}
