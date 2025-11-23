using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace RRHH.Core.Models
{
    [Index(nameof(Codigo), IsUnique = true)]
    public class Departamento
    {
        [Key]
        public int DepartamentoId { get; set; }
        public string Codigo { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Estado { get; set; } = "Activo";

        //public ICollection<Historial>? HistorialDepartamentos { get; set; }
        //public ICollection<ReporteEmpleado>? ReportesEmitidos { get; set; }
    }
}
