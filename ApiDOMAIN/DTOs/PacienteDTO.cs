using System.ComponentModel.DataAnnotations;

namespace ApiPruebaTecnica.ApiDOMAIN.DTOs
{
    public class PacienteDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El DNI del paciente es obligatorio")]
        [StringLength(20, MinimumLength = 7, ErrorMessage = "El DNI debe tener entre 7 y 20 caracteres")]
        public string Dni { get; set; }

        [Required(ErrorMessage = "El nombre del paciente es obligatorio")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 100 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido del paciente es obligatorio")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "El apellido debe tener entre 2 y 100 caracteres")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }
    }
}
