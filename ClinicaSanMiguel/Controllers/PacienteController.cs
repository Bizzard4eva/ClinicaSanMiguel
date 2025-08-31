using ClinicaSanMiguel.DTOs;
using ClinicaSanMiguel.Repositorys.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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

        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {
            // Verificar que el usuario esté logueado
            var pacienteId = HttpContext.Session.GetInt32("IdPaciente");
            if (!pacienteId.HasValue)
            {
                TempData["Error"] = "Debe iniciar sesión para acceder al dashboard.";
                return RedirectToAction("Login");
            }

            // Obtener el perfil completo del paciente (con peso, altura, tipo sangre)
            var profileData = await _pacienteRepository.LoadingProfileAsync(pacienteId.Value);
            if (profileData == null)
            {
                TempData["Error"] = "No se pudieron cargar los datos del paciente.";
                return RedirectToAction("Login");
            }

            return View(profileData);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            var resultado = await _pacienteRepository.LoginAsync(request);
            if (resultado.Resultado > 0)
            {
                HttpContext.Session.SetInt32("IdPaciente", resultado.Resultado);
                TempData["Mensaje"] = "Bienvenido!";
                return RedirectToAction("Dashboard");
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
            if (idPaciente == null) return RedirectToAction("SelectLoginRegister", "Home");

            var profile = await _pacienteRepository.LoadingProfileAsync(idPaciente.Value);
            return View(profile);
        }


        [HttpGet]
        public async Task<IActionResult> AddFamiliar()
        {
            var idPaciente = HttpContext.Session.GetInt32("IdPaciente");
            if (idPaciente == null) return RedirectToAction("SelectLoginRegister", "Home");

            var model = new AddFamiliarRequestDto
            {
                idPacienteTitular = idPaciente.Value
            };

            // Llenar combos
            ViewBag.TipoParentescos = await _pacienteRepository.ListRelationshipTypeAsync();
            ViewBag.TipoDocumentos = await _pacienteRepository.ListDocumentTypeAsync();
            ViewBag.Generos = await _pacienteRepository.ListGenresAsync();

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddFamiliar(AddFamiliarRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                // volver a llenar combos si falla validación
                ViewBag.TipoParentescos = await _pacienteRepository.ListRelationshipTypeAsync();
                ViewBag.TipoDocumentos = await _pacienteRepository.ListDocumentTypeAsync();
                ViewBag.Generos = await _pacienteRepository.ListGenresAsync();
                return View(request);
            }

            var resultado = await _pacienteRepository.AddFamiliarAsync(request);

            if (resultado.Resultado > 0)
            {
                ViewBag.Mensaje = "Familiar agregado correctamente";
                return RedirectToAction("SelectPatient", "ReservaMedica");
            }
            else
            {
                ViewBag.Error = resultado.Mensaje;
                // recarga combos en caso de error
                ViewBag.TipoParentescos = await _pacienteRepository.ListRelationshipTypeAsync();
                ViewBag.TipoDocumentos = await _pacienteRepository.ListDocumentTypeAsync();
                ViewBag.Generos = await _pacienteRepository.ListGenresAsync();
                return View(request);
            }
        }


        [HttpGet]
        public async Task<IActionResult> UpdateProfile()
        {
            var idPaciente = HttpContext.Session.GetInt32("IdPaciente");
            if (idPaciente == null) return RedirectToAction("Login", "Paciente");

            var profile = await _pacienteRepository.LoadingProfileAsync(idPaciente.Value);

            var model = new UpdateProfileRequestDto
            {
                IdPaciente = profile.IdPaciente,
                IdGenero = (profile.Genero == "Masculino" ? 1 : 2),
                Peso = profile.Peso ?? 0,
                Altura = profile.Altura ?? 0,
                IdTipoSangre = 0
            };

            ViewBag.TiposSangre = await _pacienteRepository.ListBloodTypeAsync();

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProfile(UpdateProfileRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.TiposSangre = await _pacienteRepository.ListBloodTypeAsync();
                return View(request);
            }
            var resultado = await _pacienteRepository.UpdateProfileAsync(request);
            if (resultado.Resultado > 0)
            {
                TempData["Mensaje"] = "Perfil actualizado correctamente!";
                return RedirectToAction("Profile", "Paciente"); // TODO
            }
            else
            {
                ViewBag.TiposSangre = await _pacienteRepository.ListBloodTypeAsync();
                ViewBag.Error = resultado.Mensaje;
                return View(request);
            }
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
