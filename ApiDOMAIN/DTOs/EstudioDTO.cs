namespace ApiPruebaTecnica.ApiDOMAIN.DTOs
{
    public class EstudioDTO
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public int PacienteId { get; set; }
        public int MedicoId { get; set; }
        public int PrestadorId { get; set; }



    }
}
