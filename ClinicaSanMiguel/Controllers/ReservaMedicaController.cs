using ClinicaSanMiguel.DTOs;
using ClinicaSanMiguel.Models;
using ClinicaSanMiguel.Repositorys.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ClinicaSanMiguel.Controllers
{
    public class ReservaMedicaController : Controller
    {
        private readonly ICitaMedicaRepository _citaMedicaRepository;

        public ReservaMedicaController(ICitaMedicaRepository citaMedicaRepository)
        {
            _citaMedicaRepository = citaMedicaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> SelectPatient()
        {
            var idPaciente = HttpContext.Session.GetInt32("IdPaciente");
            if (idPaciente == null) return RedirectToAction("SelectLoginRegister", "Home");

            var pacientes = await _citaMedicaRepository.PatientsForMedicalReserveAsync(idPaciente.Value);

            return View(pacientes);
        }
        [HttpPost]
        public IActionResult SelectPatient(int idPacienteSeleccionado)
        {
            HttpContext.Session.SetInt32("PacienteSelecionado", idPacienteSeleccionado);
            return RedirectToAction("SelectHealthInsurance");
        }


        [HttpGet]
        public async Task<IActionResult> SelectHealthInsurance()
        {
            var idPaciente = HttpContext.Session.GetInt32("IdPaciente");
            if (idPaciente == null) return RedirectToAction("SelectLoginRegister", "Home");

            var seguros = await _citaMedicaRepository.HealthInsuranceAsync();

            return View(seguros);
        }
        [HttpPost]
        public IActionResult SelectHealthInsurance(int idSeguroSeleccionado)
        {
            HttpContext.Session.SetInt32("SeguroSeleccionado", idSeguroSeleccionado);
            return RedirectToAction("SelectSpeciality");
        }

        [HttpGet]
        public async Task<IActionResult> SelectSpeciality()
        {
            var idPaciente = HttpContext.Session.GetInt32("IdPaciente");
            if (idPaciente == null) return RedirectToAction("SelectLoginRegister", "Home");

            var especialidades = await _citaMedicaRepository.SpecialtiesAsync();

            return View(especialidades);
        }
        [HttpPost]
        public IActionResult SelectSpeciality(int idEspecialidadSeleccionada)
        {
            HttpContext.Session.SetInt32("EspecialidadSeleccionada", idEspecialidadSeleccionada);
            return RedirectToAction("SelectClinic");
        }


        [HttpGet]
        public async Task<IActionResult> SelectClinic()
        {
            var idPaciente = HttpContext.Session.GetInt32("IdPaciente");
            if (idPaciente == null) return RedirectToAction("SelectLoginRegister", "Home");

            var clinicas = await _citaMedicaRepository.ClinicsAsync();

            return View(clinicas);
        }
        [HttpPost]
        public IActionResult SelectClinic(int idClinicaSeleccionada)
        {
            HttpContext.Session.SetInt32("ClinicaSeleccionada", idClinicaSeleccionada);
            return RedirectToAction("SelectDoctor");
        }

        [HttpGet]
        public async Task<IActionResult> SelectDoctor()
        {
            var idPaciente = HttpContext.Session.GetInt32("IdPaciente");
            if (idPaciente == null) return RedirectToAction("SelectLoginRegister", "Home");

            var clinica = HttpContext.Session.GetInt32("ClinicaSeleccionada");
            var especialidad = HttpContext.Session.GetInt32("EspecialidadSeleccionada");
            var doctores = await _citaMedicaRepository.DoctorsAsync(clinica!.Value, especialidad!.Value);

            return View(doctores);
        }
        [HttpPost]
        public async Task<IActionResult> SelectDoctor(int idDoctorSeleccionado, DateTime FechaHora)
        {
            HttpContext.Session.SetInt32("MedicoSeleccionado", idDoctorSeleccionado);

            var idPaciente = HttpContext.Session.GetInt32("PacienteSeleccionado");
            var idSeguro = HttpContext.Session.GetInt32("SeguroSeleccionado");
            var idEspecialidad = HttpContext.Session.GetInt32("EspecialidadSeleccionada");
            var idClinica = HttpContext.Session.GetInt32("ClinicaSeleccionada");

            var request = new MedicalReserveRequestDto
            {
                IdPaciente = idPaciente.Value,
                IdClinica = idClinica.Value,
                IdMedico = idDoctorSeleccionado,
                IdSeguroSeguro = idSeguro.Value,
                Fecha = FechaHora
            };

            var response = await _citaMedicaRepository.MedicalReserveAsync(request);
            if(response.Resultado > 0)
            {
                HttpContext.Session.SetInt32("idCitaMedica", response.Resultado);
                TempData["Mensaje"] = response.Mensaje;
                return RedirectToAction("DetailMedicalReserve");
            }
            else
            {
                ModelState.AddModelError("", response.Mensaje);
                return RedirectToAction("SelectPatient");
            }
        }

        [HttpGet]
        public async Task<IActionResult> DetailMedicalReserve()
        {
            var idPaciente = HttpContext.Session.GetInt32("IdPaciente");
            if (idPaciente == null) return RedirectToAction("SelectLoginRegister", "Home");

            var detalle = new DetailMedicalResponseDto();
            

            return View();
        }

    }
}
