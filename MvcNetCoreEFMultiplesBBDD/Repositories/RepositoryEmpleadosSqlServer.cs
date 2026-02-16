using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MvcNetCoreEFMultiplesBBDD.Data;
using MvcNetCoreEFMultiplesBBDD.Models;
using System.Diagnostics.Metrics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MvcNetCoreEFMultiplesBBDD.Repositories
{
    #region VIEWS
    //    Sql Server
    //    alter view V_EMPLEADOS
    //as
    //	select EMP.EMP_NO as ID,
    //    EMP.APELLIDO, EMP.OFICIO, EMP.SALARIO, DEPT.DEPT_NO AS DEPARTAMENTO, DEPT.DNOMBRE AS NOMBRE_DEPT, DEPT.LOC as LOCALIDAD
    //    from EMP inner join DEPT on EMP.DEPT_NO = DEPT.DEPT_NO
    //go
    #endregion

    #region STORED PROCEDURES
    //Sql Server
    //    create procedure SP_ALL_VEMPLEADOS
    //as
    //	select* from V_EMPLEADOS
    //go
    #endregion

    public class RepositoryEmpleadosSqlServer:IRepositoryEmpleados
    {
        private HospitalContext context;

        public RepositoryEmpleadosSqlServer(HospitalContext context)
        {
            this.context = context;
        }

        public async Task<List<VistaEmpleado>> GetVistaEmpleadosAsync()
        {
            string sql = "SP_ALL_VEMPLEADOS";
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
