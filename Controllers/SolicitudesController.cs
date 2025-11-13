using ApiPruebaTecnica.ApiDOMAIN.DTOs;
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
        //El sistema recibe un JSON con la siguiente estructura:
        //{
        //    "solicitudId": 1258, "paciente": {
        //      No viene el Id del paciente
        //    "dni": "30123123", UNIQUE¿?
        //    "nombre": "Juan",
        //    "apellido": "Pérez", "fechaNacimiento": "1985-06-20"
        //    },
        //    "estudio": {
        //    "codigo": "RX-TORAX",
        //    "descripcion": "Radiografía de tórax", "fechaSolicitud": "2025-10-30"
        //    },
        //    "medico": {
        //        "nombre": "Dra. Gómez", "matricula": "MP12345" UNIQUE ¿?
        //    },
        //    "prestadorId": 18
        //}



        [HttpPost]
        public async Task<IActionResult> ProcesarSolicitud([FromBody] SolicitudDTO solicitud)
        {
            if (solicitud != null)
            {
                //La respuesta es un estudio
                var respuesta = await _solicitudService.ProcesarSolicitudAsync(solicitud);

                if (respuesta != null)
                {
                    return Ok(respuesta);
                }
                else
                {
                    return BadRequest("Datos de solicitud vacios.");
                }
                
            }
            // Aquí se podría agregar la lógica para procesar la solicitud, como guardarla en una base de datos.
            //return Ok(new { Mensaje = "Solicitud creada exitosamente.", Solicitud = solicitud });
            return BadRequest("Datos de solicitud vacios.");
        }
    }
}
