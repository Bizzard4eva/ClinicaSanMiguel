using ClinicaSanMiguel.DTOs;
using ClinicaSanMiguel.Repositorys.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaSanMiguel.Controllers
{
    public class PacienteController : Controller
    {
        private readonly IPacienteRepository _pacienteRepository;

        public PacienteController(IPacienteRepository pacienteRepository)
        {
            _pacienteRepository = pacienteRepository;
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            var resultado = await _pacienteRepository.LoginAsync(request);
            if(resultado.Resultado > 0)
            {
                // Guardar el ID del paciente en la sesión
                HttpContext.Session.SetInt32("PacienteId", resultado.Resultado);
                TempData["Mensaje"] = "Bienvenido!";
                return RedirectToAction("Paso1", "Cita");
            }
            else
            {
                ViewBag.Error = resultado.Mensaje;
                return View(request);
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequestDto request) 
        {
            if(!ModelState.IsValid)
            {
                return View(request);
            }
            var resultado = await _pacienteRepository.RegisterAsync(request);
            if(resultado.Resultado > 0)
            {
                TempData["Mensaje"] = "Registro Exitoso!";
                return RedirectToAction("Index", "Home"); // TODO
            }
            else
            {
                ViewBag.Error = resultado.Mensaje;
                return View(request);
            }
        }

        [HttpGet]
        public IActionResult UpdateProfile()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProfile(UpdateProfileRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            var resultado = await _pacienteRepository.UpdateProfileAsync(request);
            if (resultado.Resultado > 0)
            {
                TempData["Mensaje"] = "Registro Exitoso!";
                return RedirectToAction("Index", "Home"); // TODO
            }
            else
            {
                ViewBag.Error = resultado.Mensaje;
                return View(request);
            }
        }





        public IActionResult Index()
        {
            return View();
        }
    }
}
