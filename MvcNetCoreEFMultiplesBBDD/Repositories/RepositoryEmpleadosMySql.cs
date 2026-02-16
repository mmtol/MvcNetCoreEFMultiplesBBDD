using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MvcNetCoreEFMultiplesBBDD.Data;
using MvcNetCoreEFMultiplesBBDD.Models;
using Oracle.ManagedDataAccess.Client;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MvcNetCoreEFMultiplesBBDD.Repositories
{
    #region VIEWS
    //    create view V_EMPLEADOS as
    //select EMP.EMP_NO as ID,
    //EMP.APELLIDO, EMP.OFICIO, EMP.SALARIO, DEPT.DEPT_NO AS DEPARTAMENTO, DEPT.DNOMBRE AS NOMBRE_DEPT, DEPT.LOC as LOCALIDAD
    //from EMP inner join DEPT on EMP.DEPT_NO = DEPT.DEPT_NO
    #endregion
    #region STORED PROCEDURES
    //    delimiter //
    //create procedure SP_ALL_VEMPLEADOS()
    //begin
    //    select* from V_EMPLEADOS;
    //end //
    //delimiter;
    #endregion
    public class RepositoryEmpleadosMySql:IRepositoryEmpleados
    {
        private HospitalContext context;

        public RepositoryEmpleadosMySql(HospitalContext context)
        {
            this.context = context;
        }

        public async Task<List<VistaEmpleado>> GetVistaEmpleadosAsync()
        {
            string sql = "CALL SP_ALL_VEMPLEADOS";
            var consulta = context.VistaEmpleados.FromSqlRaw(sql);
            List<VistaEmpleado> data = await consulta.ToListAsync();
            return data;
        }

        public async Task<VistaEmpleado> FindVistaEmpleadoAsync(int id)
        {
            var consulta = from datos in context.VistaEmpleados
                           where datos.Id == id
                           select datos;
            return await consulta.FirstOrDefaultAsync();
        }
    }
}
