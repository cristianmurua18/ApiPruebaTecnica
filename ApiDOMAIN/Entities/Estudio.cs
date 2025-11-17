using System.ComponentModel.DataAnnotations;

namespace ApiPruebaTecnica.ApiDOMAIN.Entities
{
    public class Estudio
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Codigo { get; set; }

        [Required]
        [MaxLength(200)]
        public required string Descripcion { get; set; }

        [Required]
        public DateTime FechaSolicitud { get; set; }

        // Foreign Keys
        public int PacienteId { get; set; }
        public int MedicoId { get; set; }
        public int PrestadorId { get; set; }

        // Relaciones
        //public Paciente Paciente { get; set; }
        //public Medico Medico { get; set; }
        //public Prestador Prestador { get; set; }
    }
}
