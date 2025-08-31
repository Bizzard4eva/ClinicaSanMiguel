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
        public IActionResult Confirmar()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index()
        {
            return View();
        }
    }
}
