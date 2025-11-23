using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RRHH.Core.Models
{
    [Index(nameof(Cod_Emp), IsUnique = true)]
    public class EmpleadoCurriculum
    {
        [Key]
        public int CurriculumId { get; set; }
        [ForeignKey ("EmpleadoId")]
        public int EmpleadoId { get; set; }
        [ForeignKey("Codigo")]
        public string Cod_Emp { get; set; } = null!;
        public string Perfil { get; set; } = null!;
        public DateTime FechaRecepcion { get; set; } = DateTime.Now;
        public string Estado { get; set; } = "Activo";
        [NotMapped]
        public Persona Persona { get; set; } = null!;
        [NotMapped]
        public Email Email { get; set; } = null!;
    }
}
