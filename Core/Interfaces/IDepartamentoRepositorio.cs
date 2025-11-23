using RRHH.Core.DTOs;

namespace RRHH.Core.Interfaces
{
    public interface IDepartamentoRepositorio
    {
        Task<DepartamentoDTO> GetDepartamento(string codigo);
        Task<List<DepartamentoDTO>> GetDepartamento();
        Task<List<DepartamentoDTO>> GetDepartamentoBorrados();
        Task<DepartamentoDTO> PostDepartamento(string codigo, string nombre);
        Task<DepartamentoDTO> PutDepartamento(string codigo, string nombreNuevo, string codigoNuevo);
        Task<DepartamentoDTO> DeleteDepartamento(string codigo);
        Task<DepartamentoDTO?> HabilitarDepartamento(string codigo);
    }
}
