using System.ComponentModel.DataAnnotations;

namespace ApiPruebaTecnica.ApiDOMAIN.DTOs
{
    public class MedicoDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del médico es obligatorio")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 200 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La matrícula del médico es obligatoria")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "La matrícula debe tener entre 2 y 50 caracteres")]
        public string Matricula { get; set; }
    }
}
