using ApiPruebaTecnica.ApiDOMAIN.DTOs;
using System.Data;

namespace ApiPruebaTecnica.ApiDATA.Daos
{
    public class DAOSolicitudes(IDbConnection dbConnection) : IDAOSolicitudes
    {
        private readonly IDbConnection _dbConnection = dbConnection;

        // Implementación de los métodos para interactuar con la base de datos

        public async Task<EstudioDTO> ProcesarSolicitudAsync(SolicitudDTO solicitudDTO)
        {

            // 1. Obtener o crear Paciente
            //var paciente = await ObtenerOCrearPacienteAsync(solicitudDTO.Paciente);

            return new EstudioDTO
            {
                Codigo = "EST123",
                Descripcion = "Estudio procesado exitosamente",
                FechaSolicitud = DateTime.Now
            };


        }

        //private async Task<PacienteDTO> ObtenerOCrearPacienteAsync(PacienteDTO paciente)
        //{

        //    var query = "SELECT * FROM Pacientes WHERE Dni = @Dni";
        //    // Buscar por DNI (único)

        //    using (var transaccionn = _dbConnection.BeginTransaction()) {
        //    var resultado = await transaccionn.Connection.QueryFirstOrDefaultAsync<PacienteDTO>(query, new { Dni = paciente.Dni }, transaction: transaccionn);
        //        .FirstOrDefaultAsync(p => p.Dni == pacienteDTO.Dni);

        //    if (paciente == null)
        //    {
        //        paciente = new Paciente
        //        {
        //            Dni = pacienteDTO.Dni,
        //            Nombre = pacienteDTO.Nombre,
        //            Apellido = pacienteDTO.Apellido,
        //            FechaNacimiento = pacienteDTO.FechaNacimiento
        //        };

        //        _context.Pacientes.Add(paciente);
        //        await _context.SaveChangesAsync();
        //        _logger.LogInformation($"Nuevo paciente creado: {paciente.Id}");
        //    }
        //    else
        //    {
        //        _logger.LogInformation($"Paciente existente reutilizado: {paciente.Id}");
        //    }

        //    return paciente;
        //}

        //    using (var transaction = await _daoSolicitudes.ProcesarSolicitudAsync(solicitud))
        //    {
        //        try
        //        {
        //            // 1. Obtener o crear Paciente
        //            var paciente = await ObtenerOCrearPacienteAsync(solicitudDTO.Paciente);

        //            // 2. Obtener o crear Médico
        //            var medico = await ObtenerOCrearMedicoAsync(solicitudDTO.Medico);

        //            // 3. Validar que Prestador existe
        //            var prestador = await _context.Prestadores
        //                .FirstOrDefaultAsync(p => p.Id == solicitudDTO.PrestadorId);

        //            if (prestador == null)
        //            {
        //                throw new InvalidOperationException($"Prestador con Id {solicitudDTO.PrestadorId} no existe");
        //            }

        //            // 4. Crear Estudio
        //            var estudio = new Estudio
        //            {
        //                Codigo = solicitudDTO.Estudio.Codigo,
        //                Descripcion = solicitudDTO.Estudio.Descripcion,
        //                FechaSolicitud = solicitudDTO.Estudio.FechaSolicitud,
        //                PacienteId = paciente.Id,
        //                MedicoId = medico.Id,
        //                PrestadorId = solicitudDTO.PrestadorId
        //            };

        //            _context.Estudios.Add(estudio);
        //            await _context.SaveChangesAsync();

        //            // 5. Confirmar transacción
        //            await transaction.CommitAsync();

        //            _logger.LogInformation($"Solicitud {solicitudDTO.SolicitudId} procesada exitosamente. Estudio Id: {estudio.Id}");
        //            return estudio.Id;
        //        }
        //        catch (Exception ex)
        //        {
        //            await transaction.RollbackAsync();
        //            _logger.LogError($"Error procesando solicitud: {ex.Message}");
        //            throw;
        //        }
        //    }
        //}

        //private async Task<Paciente> ObtenerOCrearPacienteAsync(PacienteDTO pacienteDTO)
        //{
        //    // Buscar por DNI (único)
        //    var paciente = await _context.Pacientes
        //        .FirstOrDefaultAsync(p => p.Dni == pacienteDTO.Dni);

        //    if (paciente == null)
        //    {
        //        paciente = new Paciente
        //        {
        //            Dni = pacienteDTO.Dni,
        //            Nombre = pacienteDTO.Nombre,
        //            Apellido = pacienteDTO.Apellido,
        //            FechaNacimiento = pacienteDTO.FechaNacimiento
        //        };

        //        _context.Pacientes.Add(paciente);
        //        await _context.SaveChangesAsync();
        //        _logger.LogInformation($"Nuevo paciente creado: {paciente.Id}");
        //    }
        //    else
        //    {
        //        _logger.LogInformation($"Paciente existente reutilizado: {paciente.Id}");
        //    }

        //    return paciente;
        //}

        //private async Task<Medico> ObtenerOCrearMedicoAsync(MedicoDTO medicoDTO)
        //{
        //    // Buscar por Matrícula (o ambos campos)
        //    var medico = await _context.Medicos
        //        .FirstOrDefaultAsync(m => m.Matricula == medicoDTO.Matricula && m.Nombre == medicoDTO.Nombre);

        //    if (medico == null)
        //    {
        //        medico = new Medico
        //        {
        //            Nombre = medicoDTO.Nombre,
        //            Matricula = medicoDTO.Matricula
        //        };

        //        _context.Medicos.Add(medico);
        //        await _context.SaveChangesAsync();
        //        _logger.LogInformation($"Nuevo médico creado: {medico.Id}");
        //    }
        //    else
        //    {
        //        _logger.LogInformation($"Médico existente reutilizado: {medico.Id}");
        //    }

        //    return medico;
        //}

    }



}

