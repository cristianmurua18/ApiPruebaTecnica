using System.ComponentModel.DataAnnotations;

namespace ApiPruebaTecnica.ApiDOMAIN.DTOs
{
    public class SolicitudDTO
    {
        [Required(ErrorMessage = "El ID de la solicitud es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID de la solicitud debe ser mayor a 0")]
        public int SolicitudId { get; set; }

        [Required(ErrorMessage = "Los datos del paciente son obligatorios")]
        public PacienteDTO Paciente { get; set; }

        [Required(ErrorMessage = "Los datos del estudio son obligatorios")]
        public EstudioDTO Estudio { get; set; }

        [Required(ErrorMessage = "Los datos del médico son obligatorios")]
        public MedicoDTO Medico { get; set; }

        [Required(ErrorMessage = "El ID del prestador es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del prestador debe ser mayor a 0")]
        public int PrestadorId { get; set; }
    }
}
