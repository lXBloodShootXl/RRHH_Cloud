using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace RRHH.Core.Models
{
    [Index(nameof(Codigo), IsUnique = true)]
    public class Puesto
    {
        [Key]
        public int PuestoId { get; set; }
        public string Codigo { get; set; } = null!;
        public string? Nombre { get; set; }
        public string Estado { get; set; } = "Activo";

        //public ICollection<HistorialPuesto>? HistorialPuestos { get; set; }
    }
}
