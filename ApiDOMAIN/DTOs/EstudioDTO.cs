using System.ComponentModel.DataAnnotations;

namespace ApiPruebaTecnica.ApiDOMAIN.DTOs
{
    public class EstudioDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El código del estudio es obligatorio")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "El código debe tener entre 2 y 50 caracteres")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "La descripción del estudio es obligatoria")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "La descripción debe tener entre 5 y 200 caracteres")]
        public string Descripcion { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime FechaSolicitud { get; set; }

        public int PacienteId { get; set; }
        public int MedicoId { get; set; }
        public int PrestadorId { get; set; }
    }
}
