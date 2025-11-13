namespace ApiPruebaTecnica.ApiDOMAIN.DTOs
{
    public class SolicitudDTO
    {

        public int SolicitudId { get; set; }
        public PacienteDTO Paciente { get; set; }
        public EstudioDTO Estudio { get; set; }
        public MedicoDTO Medico { get; set; }
        public int PrestadorId { get; set; }

    }
}
