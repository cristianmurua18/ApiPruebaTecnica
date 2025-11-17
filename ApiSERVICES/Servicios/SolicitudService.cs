using ApiPruebaTecnica.ApiDATA.Daos;
using ApiPruebaTecnica.ApiDOMAIN.Constants;
using ApiPruebaTecnica.ApiDOMAIN.DTOs;
using System.Text.RegularExpressions;

namespace ApiPruebaTecnica.ApiSERVICES.Servicios
{
    public class SolicitudService(IDAOSolicitudes daoSolicitudes) : ISolicitudService
    {
        private readonly IDAOSolicitudes _daoSolicitudes = daoSolicitudes;

        public async Task<EstudioDTO> ProcesarSolicitudAsync(SolicitudDTO solicitud)
        {
            if (solicitud.SolicitudId > 0)
            {
                // Normalizar la matrícula del médico a 12 caracteres antes de procesar
                if (solicitud.Medico != null && !string.IsNullOrEmpty(solicitud.Medico.Matricula))
                {
                    solicitud.Medico.Matricula = NormalizarMatricula(solicitud.Medico.Matricula);
                }

                // Validar y ajustar el código del estudio según la edad del paciente
                var edadPaciente = CalcularEdad(solicitud.Paciente.FechaNacimiento);
                var codigoEstudio = solicitud.Estudio.Codigo;

                if (edadPaciente > EstudioConstants.EdadMinimaPrefijoMono)
                {
                    // Si el paciente es mayor de 48 años, agregar el prefijo "MONO-" si no lo tiene
                    if (!codigoEstudio.StartsWith(EstudioConstants.PrefijoMono, StringComparison.OrdinalIgnoreCase))
                    {
                        codigoEstudio = $"{EstudioConstants.PrefijoMono}{codigoEstudio}";
                        solicitud.Estudio.Codigo = codigoEstudio;
                    }
                }

                var respuesta = await _daoSolicitudes.ProcesarSolicitudAsync(solicitud);
                
                if (respuesta != null)
                    return respuesta;
                else
                    return null;
            }
            
            // La solicitud tiene un formato incorrecto
            return null;
        }

        /// <summary>
        /// Normaliza la matrícula del médico a 12 caracteres.
        /// Extrae el prefijo (letras) y el número, luego rellena con ceros a la izquierda del número.
        /// Ejemplo: "MP12345" -> "MP0000012345"
        /// </summary>
        /// <param name="matricula">Matrícula original del médico</param>
        /// <returns>Matrícula normalizada de 12 caracteres</returns>
        private string NormalizarMatricula(string matricula)
        {
            // Si ya tiene 12 caracteres, devolverla sin cambios
            if (matricula.Length == EstudioConstants.LongitudMatricula)
                return matricula;

            // Extraer prefijo (letras) y número usando regex
            var match = Regex.Match(matricula, @"^([A-Za-z]+)(\d+)$");

            if (match.Success)
            {
                var prefijo = match.Groups[1].Value;
                var numero = match.Groups[2].Value;

                // Calcular cuántos caracteres debe tener la parte numérica
                var longitudNumero = EstudioConstants.LongitudMatricula - prefijo.Length;

                // Rellenar con ceros a la izquierda
                var numeroNormalizado = numero.PadLeft(longitudNumero, '0');

                return $"{prefijo}{numeroNormalizado}";
            }

            // Si no coincide con el patrón esperado, rellenar con ceros a la derecha
            return matricula.PadRight(EstudioConstants.LongitudMatricula, '0');
        }

        /// <summary>
        /// Calcula la edad de una persona basándose en su fecha de nacimiento
        /// </summary>
        /// <param name="fechaNacimiento">Fecha de nacimiento del paciente</param>
        /// <returns>Edad en años</returns>
        private int CalcularEdad(DateTime fechaNacimiento)
        {
            var hoy = DateTime.Today;
            var edad = hoy.Year - fechaNacimiento.Year;

            // Ajustar si aún no ha cumplido años este año
            if (fechaNacimiento.Date > hoy.AddYears(-edad))
            {
                edad--;
            }

            return edad;
        }
    }
}
