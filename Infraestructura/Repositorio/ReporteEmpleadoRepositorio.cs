using Microsoft.EntityFrameworkCore;
using RRHH.Core.DTOs;
using RRHH.Core.Interfaces;
using RRHH.Core.Mapedores;
using RRHH.Core.Models;
using RRHH.Infraestructura.Data;

namespace RRHH.Infraestructura.Repositorio
{
    public class ReporteEmpleadoRepositorio : IReporteEmpleadoRepositorio
    {
        private readonly RRHH_DBContext _context;

        public ReporteEmpleadoRepositorio(RRHH_DBContext context)
        {
            _context = context;
        }

        // Obtiene un reporte por empleadoId + departamentoId + FechaInicio (clave compuesta aproximada)
        public async Task<List<ReporteEmpleadoDTO>> GetReporteEmpleado(string codigo_emp, string cod_dep, DateTime fecha)
        {
            var empleado = await _context.Empleados
                .FirstOrDefaultAsync(h => h.Codigo == codigo_emp && h.Estado == "Activo");
            var dep = await _context.Departamentos
                .FirstOrDefaultAsync(h => h.Codigo == cod_dep && h.Estado == "Activo");
            if (empleado == null || dep == null)
                return null;
            return await _context.ReportesEmpleados
                .AsNoTracking()
                .Where(h => h.Cod_Emp == codigo_emp && h.Cod_Dep == cod_dep && h.Estado != "Borrado" && h.Fecha == fecha)
                .Select(h => h.toReporteEmpleadoDTO())
                .ToListAsync();
        }

        public async Task<List<ReporteEmpleadoDTO>> GetReporteEmpleado()
        {
            return await _context.ReportesEmpleados
                .AsNoTracking()
                .Where(h => h.Estado != "Borrado")
                .Select(h => h.toReporteEmpleadoDTO())
                .ToListAsync();
        }

        public async Task<List<ReporteEmpleadoDTO>> GetReporteEmpleadoBorrados()
        {
            return await _context.ReportesEmpleados
                .AsNoTracking()
                .Where(h => h.Estado == "Borrado")
                .Select(h => h.toReporteEmpleadoDTO())
                .ToListAsync();
        }


        public async Task<ReporteEmpleadoDTO> PostReporteEmpleado(string cod_emp, string cod_dep, DateTime fecha, string tipo, string desc)
        {
            if (fecha == default)
                fecha = DateTime.Now;
            var empleado = await _context.Empleados
                .FirstOrDefaultAsync(h => h.Codigo == cod_emp && h.Estado == "Activo");
            var departamento = await _context.Departamentos
                .FirstOrDefaultAsync(h => h.Codigo == cod_dep && h.Estado == "Activo");
            if (empleado == null || departamento == null)
                return null;
            var reporte = new ReporteEmpleado
            {
                Cod_Emp = cod_emp,
                Cod_Dep = cod_dep,
                EmpleadoReportadoId = empleado.EmpleadoId,
                DepartamentoEmisorId = departamento.DepartamentoId,
                Fecha = fecha,
                Tipo = tipo,
                Descripcion = desc,
                Estado = "Activo"
            };

            _context.ReportesEmpleados.Add(reporte);
            await _context.SaveChangesAsync();
            return reporte.toReporteEmpleadoDTO();
        }

        // Actualiza principalmente FechaFin (no se suele cambiar las FK históricas)
        public async Task<ReporteEmpleadoDTO?> PutReporteEmpleado(string cod_emp, string cod_dep, DateTime fecha, string tipo, string desc)
        {
            var empleado = await (from u in _context.Empleados
                                  where u.Codigo == cod_emp && u.Estado == "Activo"
                                  select u.toEmpleadoDTO()).FirstOrDefaultAsync();
            var departamento = await (from u in _context.Empleados
                                      where u.Codigo == cod_dep && u.Estado == "Activo"
                                      select u.toEmpleadoDTO()).FirstOrDefaultAsync();
            if (empleado == null || departamento == null)
                return null;

            var reporte = await _context.ReportesEmpleados
                .FirstOrDefaultAsync(h => h.Cod_Emp == empleado.Codigo && h.Cod_Dep == departamento.Codigo && h.Estado != "Borrado" && h.Fecha == fecha);

            if (reporte is null)
                return null;
            reporte.Tipo = tipo;
            reporte.Descripcion = desc;
            await _context.SaveChangesAsync();
            return reporte.toReporteEmpleadoDTO();
        }

        public async Task<ReporteEmpleadoDTO?> DeleteReporteEmpleado(string cod_emp, string cod_dep, DateTime fecha)
        {
            var empleado = await (from u in _context.Empleados
                                  where u.Codigo == cod_emp && u.Estado == "Activo"
                                  select u).FirstOrDefaultAsync();
            var departamento = await (from u in _context.Departamentos
                                      where u.Codigo == cod_dep && u.Estado == "Activo"
                                      select u.toDepartamentoDTO()).FirstOrDefaultAsync();
            if (empleado == null || departamento == null)
                return null;

            var reporte = await _context.ReportesEmpleados
                .FirstOrDefaultAsync(h => h.Cod_Emp == empleado.Codigo && h.Cod_Dep == departamento.Codigo && h.Estado == "Activo" && h.Fecha == fecha);

            if (reporte is null)
                return null;

            reporte.Estado = "Borrado";
            await _context.SaveChangesAsync();
            return reporte.toReporteEmpleadoDTO();
        }

        public async Task<ReporteEmpleadoDTO?> HabilitarReporteEmpleado(string cod_emp, string cod_dep, DateTime fecha)
        {
            var empleado = await (from u in _context.Empleados
                                  where u.Codigo == cod_emp && u.Estado == "Activo"
                                  select u).FirstOrDefaultAsync();
            var departamento = await (from u in _context.Departamentos
                                      where u.Codigo == cod_dep && u.Estado == "Activo"
                                      select u.toDepartamentoDTO()).FirstOrDefaultAsync();
            if (empleado == null || departamento == null)
                return null;
            var reporte = await _context.ReportesEmpleados
                .FirstOrDefaultAsync(h => h.Cod_Emp == empleado.Codigo && h.Cod_Dep == departamento.Codigo && h.Estado == "Borrado" && h.Fecha == fecha);

            if (reporte is null)
                return null;

            reporte.Estado = "Activo";
            await _context.SaveChangesAsync();
            return reporte.toReporteEmpleadoDTO();
        }
    }
}
