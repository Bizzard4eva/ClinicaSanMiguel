using ClinicaSanMiguel.Repositorys.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult SelectPatient()
        {
            var idPaciente = HttpContext.Session.GetInt32("IdPaciente");
            if (idPaciente == null) return RedirectToAction("SelectLoginRegister", "Home");
            //TODO
            return View();
        }

    }
}
