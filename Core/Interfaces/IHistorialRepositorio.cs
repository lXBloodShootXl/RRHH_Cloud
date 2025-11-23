using RRHH.Core.DTOs;

namespace RRHH.Core.Interfaces
{
    public interface IHistorialRepositorio
    {
        Task<List<HistorialDTO>> GetHistorialDepartamento(string codigo_emp, string codigo_puesto, string cod_dep);
        Task<List<HistorialDTO>> GetHistorialDepartamento(string cod_dep);
        Task<List<HistorialDTO>> GetHistorialDepartamentoBorrados(string cod_emp, string cod_dep);
        Task<List<HistorialDTO>> GetHistorialDepartamentoBorrados(string cod_dep);
        Task<HistorialDTO> PostHistorialDepartamento(string cod_emp, string codigo_puesto, string cod_dep, DateTime fechaInicio, DateTime? fechaFin = null);
        Task<HistorialDTO> PutHistorialDepartamento(string cod_emp, string codigo_puesto, string cod_dep, DateTime? nuevaFechaFin);
        Task<HistorialDTO> DeleteHistorialDepartamento(string cod_emp, string codigo_puesto, string cod_dep);
        Task<HistorialDTO?> HabilitarHistorialDepartamento(string cod_emp, string codigo_puesto, string cod_dep);
    }
}
