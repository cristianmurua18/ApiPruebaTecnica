using ApiPruebaTecnica.ApiDOMAIN.DTOs;
using ApiPruebaTecnica.ApiDOMAIN.Exceptions;
using Dapper;
using System.Data;

namespace ApiPruebaTecnica.ApiDATA.Daos
{
    public class DAOSolicitudes(IDbConnection dbConnection) : IDAOSolicitudes
    {
        private readonly IDbConnection _dbConnection = dbConnection;

        public async Task<EstudioDTO> ProcesarSolicitudAsync(SolicitudDTO solicitudDTO)
        {
            if (_dbConnection.State != ConnectionState.Open)
            {
                _dbConnection.Open();
            }

            using (var transaccion = _dbConnection.BeginTransaction())
            {
                try
                {
                    // 1. Obtener o crear Paciente
                    var paciente = await ObtenerOCrearPacienteAsync(solicitudDTO.Paciente, transaccion);
                    if (paciente == null)
                    {
                        transaccion.Rollback();
                        return null;
                    }

                    // 2. Obtener o crear Médico
                    var medico = await ObtenerOCrearMedicoAsync(solicitudDTO.Medico, transaccion);
                    if (medico == null)
                    {
                        transaccion.Rollback();
                        return null;
                    }

                    // 3. Validar que el prestador existe (lanza excepción si no existe)
                    var prestador = await ValidarPrestadorAsync(solicitudDTO.PrestadorId, transaccion);

                    // 4. Crear el registro del estudio
                    var estudio = await CrearEstudioAsync(solicitudDTO.Estudio, paciente.Id, medico.Id, prestador.Id, transaccion);

                    if (estudio != null)
                    {
                        transaccion.Commit();
                        return estudio;
                    }
                    else
                    {
                        transaccion.Rollback();
                        return null;
                    }
                }
                catch (PrestadorNoEncontradoException)
                {
                    transaccion.Rollback();
                    throw;
                }
                catch (Exception)
                {
                    transaccion.Rollback();
                    throw;
                }
            }
        }

        private async Task<EstudioDTO> CrearEstudioAsync(EstudioDTO estudio, int pacienteId, int medicoId, int prestadorId, IDbTransaction transaccion)
        {
            var query = @"INSERT INTO Estudios (Codigo, Descripcion, FechaSolicitud, PacienteId, MedicoId, PrestadorId) 
                          OUTPUT INSERTED.Id, INSERTED.Codigo, INSERTED.Descripcion, INSERTED.FechaSolicitud, 
                                 INSERTED.PacienteId, INSERTED.MedicoId, INSERTED.PrestadorId
                          VALUES (@Codigo, @Descripcion, @FechaSolicitud, @PacienteId, @MedicoId, @PrestadorId);";

            return await _dbConnection.QueryFirstOrDefaultAsync<EstudioDTO>(query, 
                new 
                { 
                    Codigo = estudio.Codigo, 
                    Descripcion = estudio.Descripcion, 
                    FechaSolicitud = DateTime.Now, 
                    PacienteId = pacienteId, 
                    MedicoId = medicoId, 
                    PrestadorId = prestadorId 
                }, 
                transaction: transaccion);
        }

        private async Task<PacienteDTO> ObtenerOCrearPacienteAsync(PacienteDTO paciente, IDbTransaction transaccion)
        {
            var querySelect = @"SELECT * FROM Pacientes WHERE Dni = @Dni";

            var pacienteExistente = await _dbConnection.QueryFirstOrDefaultAsync<PacienteDTO>(
                querySelect, 
                new { Dni = paciente.Dni }, 
                transaction: transaccion);

            if (pacienteExistente != null)
            {
                return new PacienteDTO
                {
                    Id = pacienteExistente.Id,
                    Dni = pacienteExistente.Dni,
                    Nombre = pacienteExistente.Nombre,
                    Apellido = pacienteExistente.Apellido,
                    FechaNacimiento = pacienteExistente.FechaNacimiento
                };
            }

            // El paciente no existe, crearlo
            var queryInsert = @"INSERT INTO Pacientes (Dni, Nombre, Apellido, FechaNacimiento) 
                               OUTPUT INSERTED.Id 
                               VALUES (@Dni, @Nombre, @Apellido, @FechaNacimiento);";

            var nuevoId = await _dbConnection.QueryFirstOrDefaultAsync<int>(
                queryInsert, 
                new
                {
                    Dni = paciente.Dni,
                    Nombre = paciente.Nombre,
                    Apellido = paciente.Apellido,
                    FechaNacimiento = paciente.FechaNacimiento
                }, 
                transaction: transaccion);

            if (nuevoId > 0)
            {
                return new PacienteDTO
                {
                    Id = nuevoId,
                    Dni = paciente.Dni,
                    Nombre = paciente.Nombre,
                    Apellido = paciente.Apellido,
                    FechaNacimiento = paciente.FechaNacimiento
                };
            }

            return null;
        }

        private async Task<MedicoDTO> ObtenerOCrearMedicoAsync(MedicoDTO medico, IDbTransaction transaccion)
        {
            var querySelect = "SELECT * FROM Medicos WHERE Matricula = @Matricula";

            var medicoExistente = await _dbConnection.QueryFirstOrDefaultAsync<MedicoDTO>(
                querySelect,
                new { Matricula = medico.Matricula }, 
                transaction: transaccion);

            if (medicoExistente != null)
            {
                return new MedicoDTO
                {
                    Id = medicoExistente.Id,
                    Nombre = medicoExistente.Nombre,
                    Matricula = medicoExistente.Matricula
                };
            }

            // El médico no existe, crearlo
            var queryInsert = "INSERT INTO Medicos (Nombre, Matricula) OUTPUT INSERTED.Id VALUES (@Nombre, @Matricula);";

            var nuevoId = await _dbConnection.QueryFirstOrDefaultAsync<int>(
                queryInsert, 
                new
                {
                    Nombre = medico.Nombre,
                    Matricula = medico.Matricula
                }, 
                transaction: transaccion);

            if (nuevoId > 0)
            {
                return new MedicoDTO
                {
                    Id = nuevoId,
                    Nombre = medico.Nombre,
                    Matricula = medico.Matricula
                };
            }

            return null;
        }

        private async Task<PrestadorDTO> ValidarPrestadorAsync(int prestadorId, IDbTransaction transaccion)
        {
            var querySelect = "SELECT * FROM Prestadores WHERE Id = @Id";

            var prestadorExistente = await _dbConnection.QueryFirstOrDefaultAsync<PrestadorDTO>(
                querySelect,
                new { Id = prestadorId }, 
                transaction: transaccion);

            if (prestadorExistente != null)
            {
                return new PrestadorDTO
                {
                    Id = prestadorExistente.Id,
                    Nombre = prestadorExistente.Nombre,
                };
            }

            throw new PrestadorNoEncontradoException(prestadorId);
        }
    }
}

