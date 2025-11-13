namespace ApiPruebaTecnica.ApiDOMAIN.DTOs
{
    public class EstudioDTO
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaSolicitud { get; set; } = DateTime.Now;
    }
}
