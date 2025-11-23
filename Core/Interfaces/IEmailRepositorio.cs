using RRHH.Core.DTOs;

namespace RRHH.Core.Interfaces
{
    public interface IEmailRepositorio
    {
        Task<EmailDTO> GetEmail(string correo);
        Task<List<EmailDTO>> GetEmail();
        Task<List<EmailDTO>> GetEmailBorrados();
        Task<EmailDTO> PostEmail(string email);
        Task<EmailDTO> PutEmail(string email, string emailNuevo);
        Task<EmailDTO> DeleteEmail(string correo);
        Task<EmailDTO?> HabilitarEmail(string correo);
    }
}
