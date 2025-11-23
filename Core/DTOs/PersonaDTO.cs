namespace RRHH.Core.DTOs
{
    public class PersonaDTO
    {
        public string CI { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string ApellidoPaterno { get; set; } = null!;
        public string ApellidoMaterno { get; set; } = null!;
        public DateTime FechaNacimiento { get; set; }
        public string Sexo { get; set; } = null!;

        //public ICollection<PersonaTelefonoDTO>? PersonaTelefonos { get; set; }
        //public ICollection<PersonaEmailDTO>? PersonaEmails { get; set; }
        //public EmpleadoDTO? Empleado { get; set; }
    }
}
