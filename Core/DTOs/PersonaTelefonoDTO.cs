namespace RRHH.Core.DTOs
{
    public class PersonaTelefonoDTO
    {
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public bool EsPrincipal { get; set; }

        public PersonaDTO Persona { get; set; } = null!;
        public TelefonoDTO Telefono { get; set; } = null!;
    }
}
