using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace RRHH.Core.Models
{
    [Index(nameof(EmpleadoId), nameof(PuestoId), nameof(DepartamentoId), nameof(FechaInicio), IsUnique = true)]
    public class Historial
    {
        [ForeignKey("EmpleadoId")]
        public int EmpleadoId { get; set; }
        [ForeignKey("PuestoId")]
        public int PuestoId { get; set; }
        [ForeignKey("DepartamentoId")]
        public int DepartamentoId { get; set; }
        [ForeignKey("Codigo")]
        public string Codigo_Emp { get; set; } = null!;
        [ForeignKey("Codigo")]
        public string Codigo_Puesto { get; set; } = null!;
        [ForeignKey("Codigo")]
        public string Codigo_Dep { get; set; } = null!;
        public DateTime FechaInicio { get; set; } = DateTime.Now;
        public DateTime? FechaFin { get; set; }
        public string Estado { get; set; } = "Activo";

        //public Empleado Empleado { get; set; } = null!;
        //public Puesto Puesto { get; set; } = null!;
        //public Departamento Departamento { get; set; } = null!;
    }
}
