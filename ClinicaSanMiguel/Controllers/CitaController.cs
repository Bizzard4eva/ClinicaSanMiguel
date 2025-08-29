using ClinicaSanMiguel.Repositorys.Interfaces;
using ClinicaSanMiguel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ClinicaSanMiguel.Controllers
{
    public class CitaController : Controller
    {
        private readonly ICitaMedicaRepository _citaMedicaRepository;
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public CitaController(ICitaMedicaRepository citaMedicaRepository, IPacienteRepository pacienteRepository, IConfiguration configuration)
        {
            _citaMedicaRepository = citaMedicaRepository;
            _pacienteRepository = pacienteRepository;
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("stringConexion") ?? "";
        }

        [HttpGet]
        public async Task<IActionResult> Paso1()
        {
            // Obtener el ID del paciente logueado desde la sesión
            var pacienteId = HttpContext.Session.GetInt32("PacienteId");
            
            // Si no hay paciente logueado, redirigir al login
            if (!pacienteId.HasValue)
            {
                TempData["Error"] = "Debe iniciar sesión para reservar una cita.";
                return RedirectToAction("Login", "Paciente");
            }
            
            ViewBag.PacienteTitular = await GetPacienteByIdAsync(pacienteId.Value);
            ViewBag.Familiares = await GetFamiliaresByTitularAsync(pacienteId.Value);
            
            return View();
        }

        [HttpPost]
        public IActionResult Paso1(int pacienteId)
        {
            HttpContext.Session.SetInt32("PacienteSeleccionado", pacienteId);
            return RedirectToAction("Paso2");
        }

        [HttpGet]
        public async Task<IActionResult> Paso2()
        {
            ViewBag.Especialidades = await GetEspecialidadesAsync();
            ViewBag.SegurosHalud = await GetSegurosHaludAsync();
            
            // Obtener datos del paciente seleccionado
            var pacienteSeleccionadoId = HttpContext.Session.GetInt32("PacienteSeleccionado");
            if (pacienteSeleccionadoId.HasValue)
            {
                ViewBag.PacienteSeleccionado = await GetPacienteByIdAsync(pacienteSeleccionadoId.Value);
            }
            
            return View();
        }

        [HttpPost]
        public IActionResult Paso2(int especialidadId, int? seguroId, bool tieneSeguro)
        {
            HttpContext.Session.SetInt32("EspecialidadSeleccionada", especialidadId);
            if (seguroId.HasValue)
                HttpContext.Session.SetInt32("SeguroSeleccionado", seguroId.Value);
            HttpContext.Session.SetString("TieneSeguro", tieneSeguro.ToString());
            
            return RedirectToAction("Paso3");
        }

        [HttpGet]
        public async Task<IActionResult> Paso3()
        {
            var especialidadId = HttpContext.Session.GetInt32("EspecialidadSeleccionada");
            if (especialidadId.HasValue)
            {
                ViewBag.Medicos = await GetMedicosByEspecialidadAsync(especialidadId.Value);
                ViewBag.EspecialidadSeleccionada = await GetEspecialidadByIdAsync(especialidadId.Value);
            }
            
            // Obtener datos del paciente seleccionado
            var pacienteSeleccionadoId = HttpContext.Session.GetInt32("PacienteSeleccionado");
            if (pacienteSeleccionadoId.HasValue)
            {
                ViewBag.PacienteSeleccionado = await GetPacienteByIdAsync(pacienteSeleccionadoId.Value);
            }
            
            return View();
        }

        [HttpPost]
        public IActionResult Paso3(int medicoId, DateTime fechaCita, TimeSpan horaCita)
        {
            HttpContext.Session.SetInt32("MedicoSeleccionado", medicoId);
            HttpContext.Session.SetString("FechaCita", fechaCita.ToString("yyyy-MM-dd"));
            HttpContext.Session.SetString("HoraCita", horaCita.ToString());
            
            return RedirectToAction("Paso4");
        }

        [HttpGet]
        public async Task<IActionResult> Paso4()
        {
            var pacienteId = HttpContext.Session.GetInt32("PacienteSeleccionado");
            var especialidadId = HttpContext.Session.GetInt32("EspecialidadSeleccionada");
            var medicoId = HttpContext.Session.GetInt32("MedicoSeleccionado");
            var seguroId = HttpContext.Session.GetInt32("SeguroSeleccionado");
            
            // Pasar los IDs para compatibilidad
            ViewBag.PacienteId = pacienteId;
            ViewBag.EspecialidadId = especialidadId;
            ViewBag.MedicoId = medicoId;
            ViewBag.SeguroId = seguroId;
            ViewBag.FechaCita = HttpContext.Session.GetString("FechaCita");
            ViewBag.HoraCita = HttpContext.Session.GetString("HoraCita");
            ViewBag.TieneSeguro = HttpContext.Session.GetString("TieneSeguro");
            
            // Obtener objetos completos para mostrar información detallada
            if (pacienteId.HasValue)
                ViewBag.PacienteSeleccionado = await GetPacienteByIdAsync(pacienteId.Value);
            if (especialidadId.HasValue)
                ViewBag.EspecialidadSeleccionada = await GetEspecialidadByIdAsync(especialidadId.Value);
            if (medicoId.HasValue)
                ViewBag.MedicoSeleccionado = await GetMedicoByIdAsync(medicoId.Value);
            if (seguroId.HasValue)
                ViewBag.SeguroSeleccionado = await GetSeguroByIdAsync(seguroId.Value);
            
            return View();
        }

        [HttpPost]
        public IActionResult ConfirmarCita(bool pagarAhora = false)
        {
            try
            {
                var citaData = new
                {
                    PacienteId = HttpContext.Session.GetInt32("PacienteSeleccionado"),
                    EspecialidadId = HttpContext.Session.GetInt32("EspecialidadSeleccionada"),
                    MedicoId = HttpContext.Session.GetInt32("MedicoSeleccionado"),
                    FechaCita = HttpContext.Session.GetString("FechaCita"),
                    HoraCita = HttpContext.Session.GetString("HoraCita"),
                    PagarAhora = pagarAhora
                };

                HttpContext.Session.Clear();

                if (pagarAhora)
                {
                    return RedirectToAction("ProcesarPago");
                }
                else
                {
                    TempData["Mensaje"] = "Cita reservada exitosamente. Puede pagar el día de la cita.";
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al confirmar la cita: " + ex.Message;
                return RedirectToAction("Paso4");
            }
        }

        [HttpGet]
        public IActionResult ProcesarPago()
        {
            return View();
        }

        // Private helper methods for data access
        private async Task<List<Especialidad>> GetEspecialidadesAsync()
        {
            var especialidades = new List<Especialidad>();
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT idEspecialidad, especialidad, precio FROM Especialidades ORDER BY especialidad", connection);
                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        especialidades.Add(new Especialidad
                        {
                            idEspecialidad = reader.GetInt32("idEspecialidad"),
                            especialidad = reader.GetString("especialidad"),
                            precio = reader.GetInt32("precio")
                        });
                    }
                }
            }
            return especialidades;
        }

        private async Task<List<SeguroSalud>> GetSegurosHaludAsync()
        {
            var seguros = new List<SeguroSalud>();
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT idSeguroSalud, nombreSeguro, cobertura FROM SeguroSalud ORDER BY nombreSeguro", connection);
                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        seguros.Add(new SeguroSalud
                        {
                            idSeguroSalud = reader.GetInt32("idSeguroSalud"),
                            nombreSeguro = reader.GetString("nombreSeguro"),
                            cobertura = reader.GetInt32("cobertura")
                        });
                    }
                }
            }
            return seguros;
        }

        private async Task<List<Medico>> GetMedicosByEspecialidadAsync(int especialidadId)
        {
            var medicos = new List<Medico>();
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(@"
                    SELECT m.idMedico, m.nombres, m.apellidos, m.celular, m.correo, 
                           m.idEspecialidad, e.especialidad 
                    FROM Medicos m 
                    INNER JOIN Especialidades e ON m.idEspecialidad = e.idEspecialidad 
                    WHERE m.idEspecialidad = @especialidadId
                    ORDER BY m.apellidos, m.nombres", connection);
                command.Parameters.AddWithValue("@especialidadId", especialidadId);
                
                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        medicos.Add(new Medico
                        {
                            idMedico = reader.GetInt32("idMedico"),
                            nombres = reader.GetString("nombres"),
                            apellidos = reader.GetString("apellidos"),
                            celular = reader.IsDBNull("celular") ? null : reader.GetString("celular"),
                            correo = reader.IsDBNull("correo") ? null : reader.GetString("correo"),
                            idEspecialidad = reader.GetInt32("idEspecialidad"),
                            Especialidad = new Especialidad
                            {
                                idEspecialidad = reader.GetInt32("idEspecialidad"),
                                especialidad = reader.GetString("especialidad")
                            }
                        });
                    }
                }
            }
            return medicos;
        }

        private async Task<List<Paciente>> GetPacientesFamiliaresByTitularAsync(int titularId)
        {
            var pacientes = new List<Paciente>();
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(@"
                    SELECT p.idPaciente, p.nombres, p.apellidoPaterno, p.apellidoMaterno, 
                           p.documento, p.titular, td.documento as tipoDoc,
                           tp.parentesco
                    FROM Pacientes p
                    INNER JOIN TipoDocumento td ON p.idTipoDocumento = td.idTipoDocumento
                    LEFT JOIN PacientesParentesco pp ON p.idPaciente = pp.idFamiliar
                    LEFT JOIN TipoParentesco tp ON pp.idTipoParentesco = tp.idTipoParentesco
                    WHERE p.idPaciente = @titularId OR pp.idPaciente = @titularId
                    ORDER BY p.titular DESC, p.apellidoPaterno, p.nombres", connection);
                command.Parameters.AddWithValue("@titularId", titularId);
                
                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        pacientes.Add(new Paciente
                        {
                            idPaciente = reader.GetInt32("idPaciente"),
                            nombres = reader.GetString("nombres"),
                            apellidoPaterno = reader.GetString("apellidoPaterno"),
                            apellidoMaterno = reader.GetString("apellidoMaterno"),
                            documento = reader.GetString("documento"),
                            titular = reader.GetBoolean("titular")
                        });
                    }
                }
            }
            return pacientes;
        }

        private async Task<Paciente?> GetPacienteByIdAsync(int pacienteId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(@"
                    SELECT p.idPaciente, p.nombres, p.apellidoPaterno, p.apellidoMaterno, 
                           p.documento, p.titular, td.documento as tipoDocumento
                    FROM Pacientes p
                    INNER JOIN TipoDocumento td ON p.idTipoDocumento = td.idTipoDocumento
                    WHERE p.idPaciente = @pacienteId", connection);
                command.Parameters.AddWithValue("@pacienteId", pacienteId);
                
                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new Paciente
                        {
                            idPaciente = reader.GetInt32("idPaciente"),
                            nombres = reader.GetString("nombres"),
                            apellidoPaterno = reader.GetString("apellidoPaterno"),
                            apellidoMaterno = reader.GetString("apellidoMaterno"),
                            documento = reader.GetString("documento"),
                            titular = reader.GetBoolean("titular")
                        };
                    }
                }
            }
            return null;
        }

        private async Task<List<Paciente>> GetFamiliaresByTitularAsync(int titularId)
        {
            var familiares = new List<Paciente>();
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(@"
                    SELECT f.idPaciente, f.nombres, f.apellidoPaterno, f.apellidoMaterno, 
                           f.documento, f.titular, tp.parentesco
                    FROM Pacientes f
                    INNER JOIN PacientesParentesco pp ON f.idPaciente = pp.idFamiliar
                    INNER JOIN TipoParentesco tp ON pp.idTipoParentesco = tp.idTipoParentesco
                    WHERE pp.idPaciente = @titularId", connection);
                command.Parameters.AddWithValue("@titularId", titularId);
                
                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        familiares.Add(new Paciente
                        {
                            idPaciente = reader.GetInt32("idPaciente"),
                            nombres = reader.GetString("nombres"),
                            apellidoPaterno = reader.GetString("apellidoPaterno"),
                            apellidoMaterno = reader.GetString("apellidoMaterno"),
                            documento = reader.GetString("documento"),
                            titular = reader.GetBoolean("titular")
                        });
                    }
                }
            }
            return familiares;
        }

        private async Task<Especialidad?> GetEspecialidadByIdAsync(int especialidadId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT idEspecialidad, especialidad, precio FROM Especialidades WHERE idEspecialidad = @especialidadId", connection);
                command.Parameters.AddWithValue("@especialidadId", especialidadId);
                
                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new Especialidad
                        {
                            idEspecialidad = reader.GetInt32("idEspecialidad"),
                            especialidad = reader.GetString("especialidad"),
                            precio = reader.GetInt32("precio")
                        };
                    }
                }
            }
            return null;
        }

        private async Task<Medico?> GetMedicoByIdAsync(int medicoId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(@"
                    SELECT m.idMedico, m.nombres, m.apellidos, m.celular, m.correo, 
                           m.idEspecialidad, e.especialidad 
                    FROM Medicos m 
                    INNER JOIN Especialidades e ON m.idEspecialidad = e.idEspecialidad 
                    WHERE m.idMedico = @medicoId", connection);
                command.Parameters.AddWithValue("@medicoId", medicoId);
                
                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new Medico
                        {
                            idMedico = reader.GetInt32("idMedico"),
                            nombres = reader.GetString("nombres"),
                            apellidos = reader.GetString("apellidos"),
                            celular = reader.IsDBNull("celular") ? null : reader.GetString("celular"),
                            correo = reader.IsDBNull("correo") ? null : reader.GetString("correo"),
                            idEspecialidad = reader.GetInt32("idEspecialidad"),
                            Especialidad = new Especialidad
                            {
                                idEspecialidad = reader.GetInt32("idEspecialidad"),
                                especialidad = reader.GetString("especialidad")
                            }
                        };
                    }
                }
            }
            return null;
        }

        private async Task<SeguroSalud?> GetSeguroByIdAsync(int seguroId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT idSeguroSalud, nombreSeguro, cobertura FROM SeguroSalud WHERE idSeguroSalud = @seguroId", connection);
                command.Parameters.AddWithValue("@seguroId", seguroId);
                
                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new SeguroSalud
                        {
                            idSeguroSalud = reader.GetInt32("idSeguroSalud"),
                            nombreSeguro = reader.GetString("nombreSeguro"),
                            cobertura = reader.GetInt32("cobertura")
                        };
                    }
                }
            }
            return null;
        }
    }
}