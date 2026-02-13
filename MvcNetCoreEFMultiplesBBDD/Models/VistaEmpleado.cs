using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcNetCoreEFMultiplesBBDD.Models
{
    [Table("V_EMPLEADOS")]
    public class VistaEmpleado
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("APELLIDO")]
        public string Apellido { get; set; }

        [Column("OFICIO")]
        public string Oficio { get; set; }

        [Column("SALARIO")]
        public int Salario { get; set; }

        [Column("DEPARTAMENTO")]
        public int Departamento { get; set; }

        [Column("NOMBRE_DEPT")]
        public string NombreDept { get; set; }

        [Column("LOCALIDAD")]
        public string Localidad { get; set; }
    }
}
