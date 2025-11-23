using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace RRHH.Core.Models
{
    [Index(nameof(CI), IsUnique = true)]
    public class Persona
    {
        [Key]
        public int PersonaId { get; set; }
        public string CI { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string ApellidoPaterno { get; set; } = null!;
        public string ApellidoMaterno { get; set; } = null!;
        public DateTime FechaNacimiento { get; set; }
        public string Sexo { get; set; } = null!;
        public string Estado { get; set; } = "Activo";

        //public ICollection<PersonaTelefono>? PersonaTelefonos { get; set; }
        //public ICollection<PersonaEmail>? PersonaEmails { get; set; }
        [NotMapped]
        public Empleado? Empleado { get; set; }
    }
}
