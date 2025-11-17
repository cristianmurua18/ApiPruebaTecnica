using ApiPruebaTecnica.ApiDOMAIN.DTOs;
using ApiPruebaTecnica.ApiDOMAIN.Exceptions;
using ApiPruebaTecnica.ApiSERVICES.Servicios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml;

namespace ApiPruebaTecnica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudesController(ISolicitudService solicitudService) : ControllerBase
    {
        private readonly ISolicitudService _solicitudService = solicitudService;

        /// <summary>
        /// Procesa una solicitud de estudio médico
        /// </summary>
        /// <param name="solicitud">Datos de la solicitud con información del paciente, médico, estudio y prestador</param>
        /// <returns>Información del estudio creado</returns>
        /// <response code="201">Estudio creado exitosamente</response>
        /// <response code="400">Datos de solicitud inválidos</response>
        /// <response code="404">Prestador no encontrado</response>
        /// <response code="500">Error interno del servidor</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ProcesarSolicitud([FromBody] SolicitudDTO solicitud)
        {
            try
            {
                // Validar que el modelo sea válido según las DataAnnotations
                if (!ModelState.IsValid)
                {
                    return BadRequest(new 
                    { 
                        Mensaje = "Datos de solicitud inválidos", 
                        Errores = ModelState.Values
                            .SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage)
                            .ToList()
                    });
                }

                if (solicitud == null)
                {
                    return BadRequest(new { Mensaje = "Los datos de la solicitud no pueden estar vacíos" });
                }

                var respuesta = await _solicitudService.ProcesarSolicitudAsync(solicitud);

                if (respuesta != null)
                {
                    return CreatedAtAction(
                        nameof(ProcesarSolicitud), 
                        new { id = respuesta.Id }, 
                        new 
                        { 
                            IdEstudio = respuesta.Id,
                            //Codigo = respuesta.Codigo,
                            //Descripcion = respuesta.Descripcion,
                            //FechaSolicitud = respuesta.FechaSolicitud,
                            Mensaje = "Estudio creado con éxito" 
                        });
                }
                else
                {
                    return BadRequest(new { Mensaje = "Error al procesar la solicitud. Verifique los datos ingresados" });
                }
            }
            catch (PrestadorNoEncontradoException ex)
            {
                return NotFound(new 
                { 
                    Mensaje = ex.Message, 
                    PrestadorId = ex.PrestadorId 
                });
            }
            catch (Exception ex)
            {
                // Log del error aquí (implementar logging posteriormente)
                return StatusCode(
                    StatusCodes.Status500InternalServerError, 
                    new 
                    { 
                        Mensaje = "Error interno del servidor al procesar la solicitud",
                        Detalle = ex.Message 
                    });
            }
        }
    }
}
