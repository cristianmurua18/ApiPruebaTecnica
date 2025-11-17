namespace ApiPruebaTecnica.ApiDOMAIN.Exceptions
{
    /// <summary>
    /// Excepción que se lanza cuando no se encuentra un prestador en la base de datos
    /// </summary>
    public class PrestadorNoEncontradoException : Exception
    {
        /// <summary>
        /// ID del prestador que no fue encontrado
        /// </summary>
        public int PrestadorId { get; }

        /// <summary>
        /// Constructor con el ID del prestador
        /// </summary>
        /// <param name="prestadorId">ID del prestador no encontrado</param>
        public PrestadorNoEncontradoException(int prestadorId) 
            : base($"El prestador con ID {prestadorId} no existe en nuestros registros")
        {
            PrestadorId = prestadorId;
        }

        /// <summary>
        /// Constructor con mensaje personalizado
        /// </summary>
        /// <param name="prestadorId">ID del prestador no encontrado</param>
        /// <param name="mensaje">Mensaje de error personalizado</param>
        public PrestadorNoEncontradoException(int prestadorId, string mensaje) 
            : base(mensaje)
        {
            PrestadorId = prestadorId;
        }
    }
}
