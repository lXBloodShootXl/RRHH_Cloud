using RRHH.Core.DTOs;

namespace RRHH.Core.Interfaces
{
    public interface IEmpleadoRepositorio
    {
        Task<EmpleadoDTO> GetEmpleado(string codigo);
        Task<List<EmpleadoDTO>> GetEmpleado();
        Task<List<EmpleadoDTO>> GetEmpleadoBorrados();
        Task<EmpleadoDTO> PostEmpleado(string ci, string codigo, DateTime fechaingreso);
        Task<EmpleadoDTO> PutEmpleado(string codigo, string codigoNuevo, string ci);
        Task<EmpleadoDTO> DeleteEmpleado(string codigo, string ci);
        Task<EmpleadoDTO?> HabilitarEmpleado(string codigo, string ci);
    }
}
