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
                TempData["Mensaje"] = "Bienvenido!";
                return RedirectToAction("Index", "Home");
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
