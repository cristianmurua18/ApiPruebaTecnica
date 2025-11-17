namespace ApiPruebaTecnica.ApiDOMAIN.Constants
{
    /// <summary>
    /// Constantes relacionadas con los estudios médicos
    /// </summary>
    public static class EstudioConstants
    {
        /// <summary>
        /// Edad mínima del paciente para requerir el prefijo MONO en el código del estudio
        /// </summary>
        public const int EdadMinimaPrefijoMono = 48;

        /// <summary>
        /// Prefijo que se agrega al código del estudio para pacientes mayores de 48 años
        /// </summary>
        public const string PrefijoMono = "MONO-";

        /// <summary>
        /// Longitud total requerida para la matrícula del médico
        /// </summary>
        public const int LongitudMatricula = 12;
    }
}
