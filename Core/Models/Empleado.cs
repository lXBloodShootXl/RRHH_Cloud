using Microsoft.EntityFrameworkCore;
using RRHH.Core.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RRHH.Core.Models
{
    [Index(nameof(Codigo), IsUnique = true)]
    public class Empleado
    {
        [Key]
        public int EmpleadoId { get; set; }
        public string Codigo { get; set; } = null!;
        [ForeignKey("PersonaId")]
        public int PersonaId { get; set; }
        public DateTime FechaIngreso { get; set; } = DateTime.Now;
        public string Estado { get; set; } = "Activo";

        [NotMapped]
        public Persona? Persona { get; set; } = null!;
        //public ICollection<HistorialPuesto>? HistorialPuestos { get; set; }
        //public ICollection<Historial>? HistorialDepartamentos { get; set; }
        //public ICollection<Nomina>? Nominas { get; set; }
        //public ICollection<ReporteEmpleado>? ReportesRecibidos { get; set; }
    }
}
