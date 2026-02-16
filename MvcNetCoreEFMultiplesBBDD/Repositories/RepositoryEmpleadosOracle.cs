using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MvcNetCoreEFMultiplesBBDD.Data;
using MvcNetCoreEFMultiplesBBDD.Models;
using Mysqlx.Crud;
using Mysqlx.Cursor;
using Oracle.ManagedDataAccess.Client;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MvcNetCoreEFMultiplesBBDD.Repositories
{
    #region VIEWS
    //        create or replace view V_EMPLEADOS
    //as
    //    select EMP.EMP_NO as ID,
    //    EMP.APELLIDO, EMP.OFICIO, EMP.SALARIO, DEPT.DEPT_NO AS DEPARTAMENTO, DEPT.DNOMBRE AS NOMBRE_DEPT, DEPT.LOC as LOCALIDAD
    //    from EMP inner join DEPT on EMP.DEPT_NO = DEPT.DEPT_NO
    #endregion

    #region STORED PROCEDURES
    //    create procedure SP_ALL_VEMPLEADOS
    //(p_cursor_empleados out SYS_REFCURSOR)
    //as
    //begin
    //    open p_cursor_empleados for
    //    select* from V_EMPLEADOS;
    //end;

    //    create or replace procedure SP_INSERTAR_EMP
    //(apellido IN VARCHAR2, oficio IN VARCHAR2, dir IN NUMBER, salario IN NUMBER, comision IN NUMBER, dept IN VARCHAR2, emp_no OUT NUMBER)
    //as
    //    dept_no NUMBER;
    //    fecha DATE;
    //    begin
    //        SELECT(MAX(EMP_NO)) + 1 INTO emp_no FROM EMP;
    //    SELECT DEPT_NO INTO dept_no FROM DEPT WHERE DNOMBRE = dept;
    //    fecha := SYSDATE;
    //    INSERT INTO EMP(EMP_NO, APELLIDO, OFICIO, DIR, FECHA_ALT, SALARIO, COMISION, DEPT_NO) VALUES(emp_no, apellido, oficio, dir, fecha, salario, comision, dept_no);
    //    end;
    #endregion
    public class RepositoryEmpleadosOracle:IRepositoryEmpleados
    {
        private HospitalContext context;

        public RepositoryEmpleadosOracle(HospitalContext context)
        {
            this.context = context;
        }

        public async Task<int> InsertEmp(string apellido, string oficio, int dir, int salario, int comision, string dept)
        {
            string sql = "begin ";
            sql += "SP_INSERTAR_EMP (:apellido, :oficio, :dir, :salario, :comision, :dept, :emp_no);";
            sql += "end;";

            var pamApellido = new OracleParameter("apellido", OracleDbType.Varchar2)
            {
                Value = apellido
            };

            var pamOficio = new OracleParameter("oficio", OracleDbType.Varchar2)
            {
                Value = oficio
            };

            var pamDir = new OracleParameter("dir", OracleDbType.Int32)
            {
                Value = dir
            };

            var pamSalario = new OracleParameter("salario", OracleDbType.Int32)
            {
                Value = salario
            };

            var pamComision = new OracleParameter("comision", OracleDbType.Int32)
            {
                Value = comision
            };

            var pamDept = new OracleParameter("dept", OracleDbType.Varchar2)
            {
                Value = dept
            };

            var pamEmpNo = new OracleParameter("emp_no", OracleDbType.Int32)
            {
                Direction = System.Data.ParameterDirection.Output
            };

            await context.Database.ExecuteSqlRawAsync(sql, pamApellido, pamOficio, pamDir, pamSalario, pamComision, pamDept, pamEmpNo);

            return ((Oracle.ManagedDataAccess.Types.OracleDecimal)pamEmpNo.Value).ToInt32();
        }

        public async Task<List<VistaEmpleado>> GetVistaEmpleadosAsync()
        {
            string sql = "begin ";
            sql += " SP_ALL_VEMPLEADOS (:p_cursor_empleados); ";
            sql += "end;";
            OracleParameter pamCursor = new OracleParameter();
            pamCursor.ParameterName = "p_cursor_empleados";
            pamCursor.Value = null;
            pamCursor.Direction = System.Data.ParameterDirection.Output;
            //indicamos el tipo de oracle
            pamCursor.OracleDbType = OracleDbType.RefCursor;

            var consulta = context.VistaEmpleados.FromSqlRaw(sql, pamCursor);
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
