using RRHH.Core.DTOs;

namespace RRHH.Core.Interfaces
{
    public interface IReporteEmpleadoRepositorio
    {
        Task<List<ReporteEmpleadoDTO>> GetReporteEmpleado(string cod_emp, string cod_dep, DateTime fecha);
        Task<List<ReporteEmpleadoDTO>> GetReporteEmpleado();
        Task<List<ReporteEmpleadoDTO>> GetReporteEmpleadoBorrados();
        Task<ReporteEmpleadoDTO> PostReporteEmpleado(string cod_emp, string cod_dep, DateTime fecha, string tipo, string desc);
        Task<ReporteEmpleadoDTO> PutReporteEmpleado(string cod_emp, string cod_dep, DateTime fecha, string tipo, string desc);
        Task<ReporteEmpleadoDTO> DeleteReporteEmpleado(string cod_emp, string cod_dep, DateTime fecha);
        Task<ReporteEmpleadoDTO?> HabilitarReporteEmpleado(string cod_emp, string cod_dep, DateTime fecha);
    }
}
