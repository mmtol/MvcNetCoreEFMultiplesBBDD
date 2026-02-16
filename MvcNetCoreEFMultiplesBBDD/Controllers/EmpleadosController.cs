using Microsoft.AspNetCore.Mvc;
using MvcNetCoreEFMultiplesBBDD.Models;
using MvcNetCoreEFMultiplesBBDD.Repositories;

namespace MvcNetCoreEFMultiplesBBDD.Controllers
{
    public class EmpleadosController : Controller
    {
        private IRepositoryEmpleados repo;

        public EmpleadosController(IRepositoryEmpleados repo)
        {
            this.repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            List<VistaEmpleado> vistas = await repo.GetVistaEmpleadosAsync();
            return View(vistas);
        }

        public async Task<IActionResult> Details(int id)
        {
            VistaEmpleado vista = await repo.FindVistaEmpleadoAsync(id);
            return View(vista);
        }
    }
}
