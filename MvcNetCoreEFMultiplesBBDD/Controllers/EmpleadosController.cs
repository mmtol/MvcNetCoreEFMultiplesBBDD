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

        public async Task<IActionResult> Insert()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Insert(string apellido, string oficio, int dir, int salario, int comision, string dept)
        {
            int empNo = await repo.InsertEmp(apellido, oficio, dir, salario, comision, dept);
            return RedirectToAction("Details", new { id = empNo });
        }
    }
}
