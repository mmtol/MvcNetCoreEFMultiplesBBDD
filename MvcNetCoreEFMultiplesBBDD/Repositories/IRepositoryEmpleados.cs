using MvcNetCoreEFMultiplesBBDD.Models;

namespace MvcNetCoreEFMultiplesBBDD.Repositories
{
    public interface IRepositoryEmpleados
    {
        Task<List<VistaEmpleado>> GetVistaEmpleadosAsync();
        Task<VistaEmpleado> FindVistaEmpleadoAsync(int id);
    }
}
