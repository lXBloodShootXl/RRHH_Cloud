namespace RRHH.Core.DTOs
{
    public class PersonaEmailDTO
    {
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public bool EsPrincipal { get; set; }

        public PersonaDTO Persona { get; set; } = null!;
        public EmailDTO Email { get; set; } = null!;
    }
}
