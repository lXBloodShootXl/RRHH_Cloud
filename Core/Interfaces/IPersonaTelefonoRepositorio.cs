using RRHH.Core.DTOs;

namespace RRHH.Core.Interfaces
{
    public interface IPersonaTelefonoRepositorio
    {
        Task<PersonaTelefonoDTO> GetPersonaTelefono(string ci);
        Task<List<PersonaTelefonoDTO>> GetPersonaTelefono();
        Task<List<PersonaTelefonoDTO>> GetPersonaTelefonoBorrados();
        Task<PersonaTelefonoDTO> PostPersonaTelefono(string ci, string nombre);
        Task<PersonaTelefonoDTO> PutPersonaTelefono(string ci, string nombreNuevo, string CiNuevo);
        Task<PersonaTelefonoDTO> DeletePersonaTelefono(string ci);
        Task<PersonaTelefonoDTO?> HabilitarPersonaTelefono(string ci);
    }
}
