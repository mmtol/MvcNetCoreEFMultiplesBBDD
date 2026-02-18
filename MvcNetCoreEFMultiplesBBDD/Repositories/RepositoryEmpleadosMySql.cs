using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MvcNetCoreEFMultiplesBBDD.Data;
using MvcNetCoreEFMultiplesBBDD.Models;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using Mysqlx.Cursor;
using Oracle.ManagedDataAccess.Client;
using Org.BouncyCastle.Utilities.Zlib;
using System.Collections.Generic;
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

    //    DELIMITER $$
    //CREATE PROCEDURE SP_INSERTAR_EMP(
    //    IN apellido VARCHAR(50),
    //    IN oficio VARCHAR(50),
    //    IN dir INT,
    //    IN salario INT,
    //    IN comision INT,
    //    IN dept VARCHAR(50),
    //    OUT emp_no INT
    //)
    //BEGIN
    //    DECLARE dept_no INT;
    //    DECLARE fecha DATETIME;
    
    //    SELECT(MAX(EMP_NO)) + 1 INTO emp_no FROM EMP;
    //    SELECT DEPT_NO INTO dept_no FROM DEPT WHERE DNOMBRE = dept;
    //    SET fecha = NOW();

    //    INSERT INTO EMP(EMP_NO, APELLIDO, OFICIO, DIR, FECHA_ALT, SALARIO, COMISION, DEPT_NO)
    //    VALUES(emp_no, apellido, oficio, dir, fecha, salario, comision, dept_no);
    //    END$$
    //DELIMITER ;
    #endregion

    public class RepositoryEmpleadosMySql:IRepositoryEmpleados
    {
        private HospitalContext context;

        public RepositoryEmpleadosMySql(HospitalContext context)
        {
            this.context = context;
        }

        public async Task<int> InsertEmp(string apellido, string oficio, int dir, int salario, int comision, string dept)
        {
            string sql = "CALL SP_INSERTAR_EMP(@apellido, @oficio, @dir, @salario, @comision, @dept, @emp_no)";
            MySqlParameter pamApellido = new MySqlParameter("@apellido", apellido);
            MySqlParameter pamOficio = new MySqlParameter("@oficio", oficio);
            MySqlParameter pamDir = new MySqlParameter("@dir", dir);
            MySqlParameter pamSalario = new MySqlParameter("@salario", salario);
            MySqlParameter pamComision = new MySqlParameter("@comision", comision);
            MySqlParameter pamDept = new MySqlParameter("@dept", dept);

            MySqlParameter pamEmpNo = new MySqlParameter("@emp_no", MySqlDbType.Int32);
            pamEmpNo.Direction = System.Data.ParameterDirection.Output;

            await context.Database.ExecuteSqlRawAsync(sql, pamApellido, pamOficio, pamDir, pamSalario, pamComision, pamDept, pamEmpNo);

            return (int)pamEmpNo.Value;
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
