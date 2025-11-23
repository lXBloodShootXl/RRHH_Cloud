using System.ComponentModel.DataAnnotations;

namespace RRHH.Core.Models
{
    public class Telefono
    {
        [Key]
        public int TelefonoId { get; set; }
        public string Numero { get; set; } = null!;
        public string Estado { get; set; } = "Activo";

        //public ICollection<PersonaTelefono>? PersonaTelefonos { get; set; }
    }
}
