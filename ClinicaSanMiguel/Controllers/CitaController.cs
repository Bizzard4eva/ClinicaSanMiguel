using ClinicaSanMiguel.Repositorys.Interfaces;
using ClinicaSanMiguel.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaSanMiguel.Controllers
{
    public class CitaController : Controller
    {
        private readonly ICitaMedicaRepository _citaMedicaRepository;
        private readonly IPacienteRepository _pacienteRepository;

        public CitaController(ICitaMedicaRepository citaMedicaRepository, IPacienteRepository pacienteRepository)
        {
            _citaMedicaRepository = citaMedicaRepository;
            _pacienteRepository = pacienteRepository;
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
            
            ViewBag.PacienteTitular = await _pacienteRepository.GetPacienteByIdAsync(pacienteId.Value);
            ViewBag.Familiares = await _pacienteRepository.GetFamiliaresByTitularAsync(pacienteId.Value);
            
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
            ViewBag.Especialidades = await _citaMedicaRepository.GetEspecialidadesAsync();
            ViewBag.SegurosHalud = await _citaMedicaRepository.GetSegurosAsync();
            
            // Obtener datos del paciente seleccionado
            var pacienteSeleccionadoId = HttpContext.Session.GetInt32("PacienteSeleccionado");
            if (pacienteSeleccionadoId.HasValue)
            {
                ViewBag.PacienteSeleccionado = await _pacienteRepository.GetPacienteByIdAsync(pacienteSeleccionadoId.Value);
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
                ViewBag.Medicos = await _citaMedicaRepository.GetMedicosByEspecialidadAsync(especialidadId.Value);
                ViewBag.EspecialidadSeleccionada = await _citaMedicaRepository.GetEspecialidadByIdAsync(especialidadId.Value);
            }
            
            // Obtener datos del paciente seleccionado
            var pacienteSeleccionadoId = HttpContext.Session.GetInt32("PacienteSeleccionado");
            if (pacienteSeleccionadoId.HasValue)
            {
                ViewBag.PacienteSeleccionado = await _pacienteRepository.GetPacienteByIdAsync(pacienteSeleccionadoId.Value);
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
                ViewBag.PacienteSeleccionado = await _pacienteRepository.GetPacienteByIdAsync(pacienteId.Value);
            if (especialidadId.HasValue)
                ViewBag.EspecialidadSeleccionada = await _citaMedicaRepository.GetEspecialidadByIdAsync(especialidadId.Value);
            if (medicoId.HasValue)
                ViewBag.MedicoSeleccionado = await _citaMedicaRepository.GetMedicoByIdAsync(medicoId.Value);
            if (seguroId.HasValue)
                ViewBag.SeguroSeleccionado = await _citaMedicaRepository.GetSeguroByIdAsync(seguroId.Value);
            
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

    }
}