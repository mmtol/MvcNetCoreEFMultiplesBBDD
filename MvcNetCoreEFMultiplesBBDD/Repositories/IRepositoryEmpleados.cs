using MvcNetCoreEFMultiplesBBDD.Models;

namespace MvcNetCoreEFMultiplesBBDD.Repositories
{
    public interface IRepositoryEmpleados
    {
        Task<List<VistaEmpleado>> GetVistaEmpleadosAsync();
        Task<VistaEmpleado> FindVistaEmpleadoAsync(int id);
        Task<int> InsertEmp(string apellido, string oficio, int dir, int salario, int comision, string dept);
    }
}
