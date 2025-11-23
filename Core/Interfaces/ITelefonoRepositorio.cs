using RRHH.Core.DTOs;

namespace RRHH.Core.Interfaces
{
    public interface ITelefonoRepositorio
    {
        Task<TelefonoDTO> GetTelefono(string ci);
        Task<List<TelefonoDTO>> GetTelefono();
        Task<List<TelefonoDTO>> GetTelefonoBorrados();
        Task<TelefonoDTO> PostTelefono(string ci, string nombre);
        Task<TelefonoDTO> PutTelefono(string ci, string nombreNuevo, string CiNuevo);
        Task<TelefonoDTO> DeleteTelefono(string ci);
        Task<TelefonoDTO?> HabilitarTelefono(string ci);
    }
}
