using RRHH.Core.DTOs;

namespace RRHH.Core.Interfaces
{
    public interface IPersonaEmailRepositorio
    {
        Task<PersonaEmailDTO> GetPersonaEmail(string ci);
        Task<List<PersonaEmailDTO>> GetPersonaEmail();
        Task<List<PersonaEmailDTO>> GetPersonaEmailBorrados();
        Task<PersonaEmailDTO> PostPersonaEmail(string ci, string nombre);
        Task<PersonaEmailDTO> PutPersonaEmail(string ci, string nombreNuevo, string CiNuevo);
        Task<PersonaEmailDTO> DeletePersonaEmail(string ci);
        Task<PersonaEmailDTO?> HabilitarPersonaEmail(string ci);
    }
}
