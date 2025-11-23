using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RRHH.Core.Models
{
    [Index(nameof(EmpleadoId), nameof(PeriodoInicio), nameof(PeriodoFin), IsUnique = true)]
    public class Nomina
    {
        [Key]
        public int NominaId { get; set; }
        public string Cod_Nom { get; set; } = null!;
        public int EmpleadoId { get; set; }
        public string Cod_Emp { get; set; } = null!;
        public DateTime PeriodoInicio { get; set; } = DateTime.Now;
        public DateTime PeriodoFin { get; set; }
        public decimal SalarioBase { get; set; } = 2500;
        public decimal Bonos { get; set; } = 0;
        public decimal Descuentos { get; set; } = 0;
        public decimal TotalNeto { get; set; }
        public string Estado { get; set; } = "Activo";
    }
}
