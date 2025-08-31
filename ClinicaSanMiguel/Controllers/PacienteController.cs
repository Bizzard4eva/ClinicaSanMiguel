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
                HttpContext.Session.SetInt32("IdPaciente", resultado.Resultado);
                TempData["Mensaje"] = "Bienvenido!";
                return RedirectToAction("Index", "Home"); // TODO
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
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            var resultado = await _pacienteRepository.RegisterAsync(request);
            if (resultado.Resultado > 0)
            {
                TempData["Mensaje"] = "Registro Exitoso!";
                return RedirectToAction("SelectLoginRegister", "Home"); // TODO
            }
            else
            {
                ViewBag.Error = resultado.Mensaje;
                return View(request);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var idPaciente = HttpContext.Session.GetInt32("IdPaciente");
            if (idPaciente  == null) return RedirectToAction("Login", "Paciente");
            var profile = await _pacienteRepository.LoadingProfileAsync(idPaciente.Value);
            return View(profile);
        }
 

        [HttpGet]
        public IActionResult AddFamiliar(int idPacienteTitular)
        {
            // Inicializas el modelo con el idPaciente titular
            var model = new AddFamiliarRequestDto
            {
                idPacienteTitular = idPacienteTitular
            };
            return View(model); // <- Vista Create (AddFamiliar.cshtml)
        }

        [HttpPost]
        public async Task<IActionResult> AddFamiliar(AddFamiliarRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var resultado = await _pacienteRepository.AddFamiliarAsync(request);
            if (resultado.Resultado > 0)
            {
                ViewBag.Mensaje = "Familiar agregado correctamente";
                // Devuelves la misma vista limpia para poder ingresar otro familiar
                return View(new AddFamiliarRequestDto { idPacienteTitular = request.idPacienteTitular });
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

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Index()
        {
            // TODO
            return View();
        }

    }
}
