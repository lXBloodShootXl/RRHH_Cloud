using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RRHH.Core.Models
{
    [Index(nameof(EmpleadoReportadoId), nameof(DepartamentoEmisorId), nameof(Fecha), IsUnique = true)]
    public class ReporteEmpleado
    {
        [Key]
        public int ReporteId { get; set; }
        public int EmpleadoReportadoId { get; set; }
        public int DepartamentoEmisorId { get; set; }
        public string Cod_Emp { get; set; } = null!;
        public string Cod_Dep { get; set; } = null!;
        public DateTime Fecha { get; set; } = DateTime.Now;
        public string Tipo { get; set; } = "Desconocido";
        public string? Descripcion { get; set; }
        public string Estado { get; set; } = "Activo";
        [NotMapped]
        public Empleado? EmpleadoReportado { get; set; } = null!;
        [NotMapped]
        public Departamento? DepartamentoEmisor { get; set; } = null!;
    }
}
