using Microsoft.EntityFrameworkCore;
using RRHH.Core.DTOs;
using RRHH.Core.Interfaces;
using RRHH.Core.Mapedores;
using RRHH.Core.Models;
using RRHH.Infraestructura.Data;

namespace RRHH.Infraestructura.Repositorio
{
    public class HistorialRepositorio : IHistorialRepositorio
    {
        private readonly RRHH_DBContext _context;

        public HistorialRepositorio(RRHH_DBContext context)
        {
            _context = context;
        }

        // Obtiene un historial por empleadoId + departamentoId + FechaInicio (clave compuesta aproximada)
        public async Task<List<HistorialDTO>> GetHistorialDepartamento(string codigo_emp, string codigo_puesto, string cod_dep)
        {
            var empleado = await _context.Empleados
                .FirstOrDefaultAsync(h => h.Codigo == codigo_emp && h.Estado == "Activo");
            var dep = await _context.Departamentos
                .FirstOrDefaultAsync(h => h.Codigo == cod_dep && h.Estado == "Activo");
            var puesto = await _context.Puestos
                .FirstOrDefaultAsync(h => h.Codigo == codigo_puesto && h.Estado == "Activo");
            if (empleado == null || dep == null || puesto == null)
                return null;
            return await _context.HistorialDepartamentos
                .AsNoTracking()
                .Where(h => h.Codigo_Emp == codigo_emp && h.Codigo_Dep == cod_dep && h.Codigo_Puesto==codigo_puesto && h.Estado != "Borrado")
                .Select(h => h.toHistorialDepartamentoDTO())
                .ToListAsync();
        }

        public async Task<List<HistorialDTO>> GetHistorialDepartamento(string cod_dep)
        {
            return await _context.HistorialDepartamentos
                .AsNoTracking()
                .Where(h => h.Estado != "Borrado" & h.Codigo_Dep == cod_dep)
                .Select(h => h.toHistorialDepartamentoDTO())
                .ToListAsync();
        }
        public async Task<List<HistorialDTO>> GetHistorialDepartamentoBorrados(string codigo_emp, string cod_dep)
        {
            var empleado = await _context.Empleados
                .FirstOrDefaultAsync(h => h.Codigo == codigo_emp && h.Estado == "Activo");
            var departamento = await _context.Departamentos
                .FirstOrDefaultAsync(h => h.Codigo == cod_dep && h.Estado == "Activo");
            if (empleado == null || departamento == null)
                return null;
            return await _context.HistorialDepartamentos
                .AsNoTracking()
                .Where(h => h.Codigo_Emp == codigo_emp && h.Codigo_Dep == cod_dep && h.Estado == "Borrado")
                .Select(h => h.toHistorialDepartamentoDTO())
                .ToListAsync();
        }

        public async Task<List<HistorialDTO>> GetHistorialDepartamentoBorrados(string cod_dep)
        {
            return await _context.HistorialDepartamentos
                .AsNoTracking()
                .Where(h => h.Estado == "Borrado" & h.Codigo_Dep == cod_dep)
                .Select(h => h.toHistorialDepartamentoDTO())
                .ToListAsync();
        }


        public async Task<HistorialDTO> PostHistorialDepartamento(string cod_emp, string codigo_puesto, string cod_dep, DateTime fechaInicio, DateTime? fechaFin = null)
        {
            if (fechaInicio == default)
                fechaInicio = DateTime.Now;
            var empleado = await _context.Empleados
                .FirstOrDefaultAsync(h => h.Codigo == cod_emp && h.Estado == "Activo");
            var puesto = await _context.Puestos
                .FirstOrDefaultAsync(h => h.Codigo == codigo_puesto && h.Estado == "Activo");
            var departamento = await _context.Departamentos
                .FirstOrDefaultAsync(h => h.Codigo == cod_dep && h.Estado == "Activo");
            if (empleado == null || departamento == null || puesto == null)
                return null;
            var historial = new Historial
            {
                Codigo_Emp = cod_emp,
                Codigo_Puesto = codigo_puesto,
                Codigo_Dep = cod_dep,
                EmpleadoId = empleado.EmpleadoId,
                DepartamentoId = departamento.DepartamentoId,
                FechaInicio = fechaInicio,
                FechaFin = fechaFin,
                Estado = "Activo"
            };

            _context.HistorialDepartamentos.Add(historial);
            await _context.SaveChangesAsync();
            return historial.toHistorialDepartamentoDTO();
        }

        // Actualiza principalmente FechaFin (no se suele cambiar las FK históricas)
        public async Task<HistorialDTO?> PutHistorialDepartamento(string cod_emp, string codigo_puesto, string cod_dep, DateTime? nuevaFechaFin)
        {
            var empleado = await (from u in _context.Empleados
                                  where u.Codigo == cod_emp && u.Estado == "Activo"
                                  select u.toEmpleadoDTO()).FirstOrDefaultAsync();
            var puesto = await (from u in _context.Puestos
                                      where u.Codigo == codigo_puesto && u.Estado == "Activo"
                                      select u.toPuestoDTO()).FirstOrDefaultAsync();
            var departamento = await (from u in _context.Empleados
                                      where u.Codigo == cod_dep && u.Estado == "Activo"
                                      select u.toEmpleadoDTO()).FirstOrDefaultAsync();
            if (empleado == null || departamento == null || puesto == null)
                return null;

            var historial = await _context.HistorialDepartamentos
                .FirstOrDefaultAsync(h => h.Codigo_Emp == empleado.Codigo && h.Codigo_Dep == departamento.Codigo && h.Codigo_Puesto==codigo_puesto && h.Estado != "Borrado");

            if (historial is null)
                return null;

            if (nuevaFechaFin != null)
                historial.FechaFin = nuevaFechaFin;
            await _context.SaveChangesAsync();
            return historial.toHistorialDepartamentoDTO();
        }

        public async Task<HistorialDTO?> DeleteHistorialDepartamento(string cod_emp, string codigo_puesto, string cod_dep)
        {
            var empleado = await (from u in _context.Empleados
                                  where u.Codigo == cod_emp && u.Estado == "Activo"
                                  select u.toEmpleadoDTO()).FirstOrDefaultAsync();
            var puesto = await (from u in _context.Puestos
                                      where u.Codigo == codigo_puesto && u.Estado == "Activo"
                                      select u.toPuestoDTO()).FirstOrDefaultAsync();
            var departamento = await (from u in _context.Departamentos
                                      where u.Codigo == cod_dep && u.Estado == "Activo"
                                      select u.toDepartamentoDTO()).FirstOrDefaultAsync();
            if (empleado == null || departamento == null || puesto == null)
                return null;

            var historial = await _context.HistorialDepartamentos
                .FirstOrDefaultAsync(h => h.Codigo_Emp == empleado.Codigo && h.Codigo_Dep == departamento.Codigo && h.Codigo_Puesto == codigo_puesto && h.Estado == "Activo");

            if (historial is null)
                return null;

            historial.Estado = "Borrado";
            await _context.SaveChangesAsync();
            return historial.toHistorialDepartamentoDTO();
        }

        public async Task<HistorialDTO?> HabilitarHistorialDepartamento(string cod_emp, string codigo_puesto, string cod_dep)
        {
            var empleado = await (from u in _context.Empleados
                                  where u.Codigo == cod_emp && u.Estado == "Activo"
                                  select u.toEmpleadoDTO()).FirstOrDefaultAsync();
            var puesto = await (from u in _context.Puestos
                                      where u.Codigo == codigo_puesto && u.Estado == "Activo"
                                      select u.toPuestoDTO()).FirstOrDefaultAsync();
            var departamento = await (from u in _context.Empleados
                                      where u.Codigo == cod_dep && u.Estado == "Activo"
                                      select u.toEmpleadoDTO()).FirstOrDefaultAsync();
            if (empleado == null || departamento == null || codigo_puesto == null)
                return null;
            var historial = await _context.HistorialDepartamentos
                .FirstOrDefaultAsync(h => h.Codigo_Emp == empleado.Codigo && h.Codigo_Dep == departamento.Codigo && h.Codigo_Puesto == codigo_puesto && h.Estado == "Borrado");

            if (historial is null)
                return null;

            historial.Estado = "Activo";
            await _context.SaveChangesAsync();
            return historial.toHistorialDepartamentoDTO();
        }
    }
}
