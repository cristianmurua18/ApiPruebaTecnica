using ApiPruebaTecnica.ApiDOMAIN.DTOs;
using Dapper;
using System.Data;

namespace ApiPruebaTecnica.ApiDATA.Daos
{
    public class DAOSolicitudes(IDbConnection dbConnection) : IDAOSolicitudes
    {
        private readonly IDbConnection _dbConnection = dbConnection;

        // Implementación de los métodos para interactuar con la base de datos

        public async Task<EstudioDTO> ProcesarSolicitudAsync(SolicitudDTO solicitudDTO)
        {

            if (_dbConnection.State != ConnectionState.Open)
            {
                _dbConnection.Open();
            }

            //Abro transaccion
            using (var transaccionn = _dbConnection.BeginTransaction())
            {
                // 1. Obtener o crear Paciente
                var paciente = await ObtenerOCrearPacienteAsync(solicitudDTO.Paciente, transaccionn);

                if (paciente != null)
                {
                    //Esto implica que el paciente fue creado o ya existía

                    // 2. Obtener o crear o actualizar el Médico
                    //            
                    var medico = await ObtenerOCrearMedicoAsync(solicitudDTO.Medico, transaccionn);

                    if (medico != null)
                    {
                        // 3. Validar que el prestador existe
                        var prestador = await ValidarPrestadorAsync(solicitudDTO.PrestadorId, transaccionn);

                        if (prestador != null)
                        {
                            
                            //4. Crear el registro del estudio, vinculando las entidades correspondientes.
                            var query = @"INSERT INTO Estudios (Codigo, Descripcion, FechaSolicitud, PacienteId, MedicoId, PrestadorId) OUTPUT INSERTED.Id 
                                           VALUES (@Codigo, @Descripcion, @FechaSolicitud, @PacienteId, @MedicoId, @PrestadorId);";

                            var estudio = await _dbConnection.QueryFirstOrDefaultAsync<EstudioDTO>(query, new { Codigo = solicitudDTO.Estudio.Codigo, Descripcion = solicitudDTO.Estudio.Descripcion, FechaSolicitud = DateTime.Now, PacienteId = paciente.Id, MedicoId = medico.Id, PrestadorId = prestador.Id }, transaction: transaccionn);

                            if (estudio != null)
                            {
                                // Si todo salió bien, confirmo la transacción
                                transaccionn.Commit();
                                return estudio;

                            }
                            else
                            {
                                // Si hubo un error al insertar el estudio, hago rollback
                                transaccionn.Rollback();
                                return null;
                            }

                        }

                    }
                }
                // Si hubo algún error en el proceso, hago rollback
                transaccionn.Rollback();
                return null;

            }

        }

        private async Task<PacienteDTO> ObtenerOCrearPacienteAsync(PacienteDTO paciente, IDbTransaction transaction)
        {
            // Buscar por DNI (único)
            var query = @"SELECT * FROM Pacientes WHERE Dni = @Dni";

            //Uso conexion a la base de datos

            var pacienteBd = await _dbConnection.QueryFirstOrDefaultAsync<PacienteDTO>(query, new { Dni = paciente.Dni }, transaction: transaction);

            // Verificar si el paciente ya existe
            if (pacienteBd != null)
            {
                // El paciente ya existe, devolverlo con los datos completos
                var pacienteCompleto = new PacienteDTO
                {
                    Id = pacienteBd.Id,
                    Dni = pacienteBd.Dni,
                    Nombre = pacienteBd.Nombre,
                    Apellido = pacienteBd.Apellido,
                    FechaNacimiento = pacienteBd.FechaNacimiento
                };
                return pacienteCompleto;

            }
            else
            {
                //El paciente no existe, debo agregarlo a la base de datos
                var insertQuery = @"INSERT INTO Pacientes (Dni, Nombre, Apellido, FechaNacimiento) OUTPUT INSERTED.Id VALUES (@Dni, @Nombre, @Apellido, @FechaNacimiento);";

                //Debo agregar los datos que vienen de la variable paciente que llega como parámetro del servicio
                var nuevoId = await _dbConnection.QueryFirstOrDefaultAsync<int>(insertQuery, new
                {
                    Dni = paciente.Dni,
                    Nombre = paciente.Nombre,
                    Apellido = paciente.Apellido,
                    FechaNacimiento = paciente.FechaNacimiento
                }, transaction: transaction);

                if (nuevoId > 0)
                {
                    // Devuelvo un nuevo PacienteDTO con el Id generado y los datos originales
                    return new PacienteDTO
                    {
                        Id = nuevoId,
                        Dni = paciente.Dni,
                        Nombre = paciente.Nombre,
                        Apellido = paciente.Apellido,
                        FechaNacimiento = paciente.FechaNacimiento
                    };
                }
                else
                {
                    return null;
                }

            }

        }

        private async Task<MedicoDTO> ObtenerOCrearMedicoAsync(MedicoDTO medico, IDbTransaction transaction)
        {


            var selectQuery = "SELECT * FROM Medicos WHERE Matricula = @Matricula";
            // Buscar por Matrícula (o ambos campos)

            var medicoBd = await _dbConnection.QueryFirstOrDefaultAsync<MedicoDTO>(selectQuery,
                new { Matricula = medico.Matricula }, transaction : transaction);

            //Existe el medico
            if (medicoBd != null)
            {
                var medicoCompleto = new MedicoDTO
                {
                    Id = medicoBd.Id,
                    Nombre = medicoBd.Nombre,
                    Matricula = medicoBd.Matricula

                };
                return medicoCompleto;

            }
            else
            {
                var insertQuery = "INSERT INTO Medicos (Nombre, Matricula) OUTPUT INSERTED.Id VALUES (@Nombre, @Matricula);";

                var newId = await _dbConnection.QueryFirstOrDefaultAsync<int>(insertQuery, new
                {
                    Id = medico.Id,
                    Nombre = medico.Nombre,
                    Matricula = medico.Matricula
                }, transaction: transaction);

                if (newId > 0)
                {
                    // Devuelvo un nuevo MedicoDTO con el Id generado y los datos originales
                    return new MedicoDTO
                    {
                        Id = newId,
                        Nombre = medico.Nombre,
                        Matricula = medico.Matricula
                    };
                }
                else
                {
                    return null;
                }

            }
        }


        private async Task<PrestadorDTO> ValidarPrestadorAsync(int prestadorId, IDbTransaction transaction)
        {
            var selectQuery = "SELECT * FROM Prestadores WHERE Id = @Id";
            // Buscar por Id (o ambos campos)

            var prestadorBd = await _dbConnection.QueryFirstOrDefaultAsync<PrestadorDTO>(selectQuery,
                new { Id = prestadorId }, transaction: transaction);

            //Existe el medico
            if (prestadorBd != null)
            {
                var prestadorCompleto = new PrestadorDTO
                {
                    Id = prestadorBd.Id,
                    Nombre = prestadorBd.Nombre,

                };
                return prestadorCompleto;

            }
            else
            {
                //Cancelo la transacción si no existe el prestador
                return null;
                //Retorno null para indicar que no se encontró el prestador
                throw new Exception("El prestador no existe en nuestro registros");

            }

        }

    }

}

