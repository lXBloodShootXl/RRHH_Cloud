using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using RRHH.Core.Models;

namespace RRHH.Core.Models
{
    [Index(nameof(PersonaId), nameof(TelefonoId), nameof(FechaInicio), IsUnique = true)]
    public class PersonaTelefono
    {
        public int PersonaId { get; set; }
        public int TelefonoId { get; set; }
        public DateTime FechaInicio { get; set; } = DateTime.Now;
        public DateTime? FechaFin { get; set; }
        public bool EsPrincipal { get; set; }
        public string Estado { get; set; } = "Activo";

        public Persona Persona { get; set; } = null!;
        public Telefono Telefono { get; set; } = null!;
    }
}
