using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MvcNetCoreEFMultiplesBBDD.Data;
using MvcNetCoreEFMultiplesBBDD.Models;
using Mysqlx.Crud;
using Mysqlx.Cursor;
using Org.BouncyCastle.Utilities.Zlib;
using System.Collections.Generic;
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

    //    create procedure SP_INSERTAR_EMP
    //(@apellido nvarchar(50), @oficio nvarchar(50), @dir int, @salario int, @comision int, @dept nvarchar(50), @emp_no int out)
    //as
    //	declare @dept_no int;
    //	declare @fecha DateTime;
    //	select @emp_no = Max(EMP_NO) + 1 from EMP;
    //    select @dept_no = DEPT_NO from DEPT where DNOMBRE = @dept;
    //    set @fecha = SYSDATETIME();
    //    insert into EMP values(@emp_no, @apellido, @oficio, @dir, @fecha, @salario, @comision, @dept_no);
    //    go
    #endregion

    public class RepositoryEmpleadosSqlServer:IRepositoryEmpleados
    {
        private HospitalContext context;

        public RepositoryEmpleadosSqlServer(HospitalContext context)
        {
            this.context = context;
        }

        public async Task<int> InsertEmp(string apellido, string oficio, int dir, int salario, int comision, string dept)
        {
            string sql = "SP_INSERTAR_EMP @apellido, @oficio, @dir, @salario, @comision, @dept, @emp_no OUTPUT";
            SqlParameter pamApellido = new SqlParameter("@apellido", apellido);
            SqlParameter pamOficio = new SqlParameter("@oficio", oficio);
            SqlParameter pamDir = new SqlParameter("@dir", dir);
            SqlParameter pamSalario = new SqlParameter("@salario", salario);
            SqlParameter pamComision = new SqlParameter("@comision", comision);
            SqlParameter pamDept = new SqlParameter("@dept", dept);

            SqlParameter pamEmpNo = new SqlParameter("@emp_no", System.Data.SqlDbType.Int);
            pamEmpNo.Direction = System.Data.ParameterDirection.Output;

            await context.Database.ExecuteSqlRawAsync(sql, pamApellido, pamOficio, pamDir, pamSalario, pamComision, pamDept, pamEmpNo);

            return (int)pamEmpNo.Value;
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
