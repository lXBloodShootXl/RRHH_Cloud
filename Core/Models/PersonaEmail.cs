using Microsoft.EntityFrameworkCore;
using RRHH.Core.Models;

namespace RRHH.Core.Models
{
    [Index(nameof(PersonaId), nameof(EmailId), nameof(FechaInicio), IsUnique = true)]
    public class PersonaEmail
    {
        public int PersonaId { get; set; }
        public int EmailId { get; set; }
        public DateTime FechaInicio { get; set; } = DateTime.Now;
        public DateTime? FechaFin { get; set; }
        public bool EsPrincipal { get; set; }
        public string Estado { get; set; } = "Activo";

        public Persona Persona { get; set; } = null!;
        public Email Email { get; set; } = null!;
    }
}
