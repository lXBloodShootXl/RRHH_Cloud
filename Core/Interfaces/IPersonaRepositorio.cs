using RRHH.Core.DTOs;

namespace RRHH.Core.Interfaces
{
    public interface IPersonaRepositorio
    {
        Task<PersonaDTO> GetPersona(string ci);
        Task<List<PersonaDTO>> GetPersona();
        Task<List<PersonaDTO>> GetPersonaBorrados();
        Task<PersonaDTO> PostPersona(string ci, string nombre, string? apellidoPaterno, string? apellidoMaterno, string sexo, DateTime fechanacimiento);
        Task<PersonaDTO> PutPersona(string ci, string ciNuevo, string nombre, string? apellidoPaterno, string? apellidoMaterno);
        Task<PersonaDTO> DeletePersona(string ci);
        Task<PersonaDTO?> HabilitarPersona(string ci);
    }
}
