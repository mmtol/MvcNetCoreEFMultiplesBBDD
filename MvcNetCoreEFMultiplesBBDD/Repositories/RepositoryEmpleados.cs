using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MvcNetCoreEFMultiplesBBDD.Data;
using MvcNetCoreEFMultiplesBBDD.Models;
using System.Diagnostics.Metrics;

namespace MvcNetCoreEFMultiplesBBDD.Repositories
{
    #region VIEWS
    //Oracle
    //        create or replace view V_EMPLEADOS
    //as
    //    select EMP.EMP_NO as ID,
    //    EMP.APELLIDO, EMP.OFICIO, EMP.SALARIO, DEPT.DEPT_NO AS DEPARTAMENTO, DEPT.DNOMBRE AS NOMBRE_DEPT, DEPT.LOC as LOCALIDAD
    //    from EMP inner join DEPT on EMP.DEPT_NO = DEPT.DEPT_NO
    //    Sql Server
    //    alter view V_EMPLEADOS
    //as
    //	select EMP.EMP_NO as ID,
    //    EMP.APELLIDO, EMP.OFICIO, EMP.SALARIO, DEPT.DEPT_NO AS DEPARTAMENTO, DEPT.DNOMBRE AS NOMBRE_DEPT, DEPT.LOC as LOCALIDAD
    //    from EMP inner join DEPT on EMP.DEPT_NO = DEPT.DEPT_NO
    //go
    #endregion

    public class RepositoryEmpleados
    {
        private HospitalContext context;

        public RepositoryEmpleados(HospitalContext context)
        {
            this.context = context;
        }

        public async Task<List<VistaEmpleado>> GetVistaEmpleadosAsync()
        {
            var consulta = from datos in context.VistaEmpleados
                           select datos;
            return await consulta.ToListAsync();
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
