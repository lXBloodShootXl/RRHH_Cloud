using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace RRHH.Core.Models
{
    [Index(nameof(Correo), IsUnique = true)]
    public class Email
    {
        [Key]
        public int EmailId { get; set; }
        public string Correo { get; set; } = null!;
        public string Estado { get; set; } = "Activo";

        //public ICollection<PersonaEmail>? PersonaEmails { get; set; }
    }
}
