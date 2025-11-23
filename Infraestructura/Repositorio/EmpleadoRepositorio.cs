using RRHH.Core.DTOs;
using RRHH.Core.Interfaces;
using RRHH.Core.Mapedores;
using RRHH.Core.Models;
using RRHH.Infraestructura.Data;
using Microsoft.EntityFrameworkCore;

namespace RRHH.Infraestructura.Repositorio
{
    public class EmpleadoRepositorio : IEmpleadoRepositorio
    {
        private readonly RRHH_DBContext _context;

        public EmpleadoRepositorio(RRHH_DBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene un empleado por su codigo.
        /// </summary>
        public async Task<EmpleadoDTO> GetEmpleado(string codigo)
        {
            var e = await _context.Empleados
                .AsNoTracking()
                .Where(e => e.Codigo == codigo && e.Estado != "Borrado")
                //.Select(e => e.toEmpleadoDTO())
                .FirstOrDefaultAsync();
            if (e == null)
            {
                return null;
            }
            var p = await _context.Personas
                .AsNoTracking()
                .Where(e => e.PersonaId == e.PersonaId && e.Estado == "Activo")
                .FirstOrDefaultAsync();
            if (p == null)
            {
                return null;
            }
            EmpleadoDTO eDTO = e.toEmpleadoDTO();
            eDTO.Persona = p.toPersonaDTO();
            return eDTO;
        }

        /// <summary>
        /// Obtiene todos los empleados activos (no borrados).
        /// </summary>
        public async Task<List<EmpleadoDTO>> GetEmpleado()
        {
            // Obtener empleados y sus personas asociadas en una sola consulta
            var empleados = await (from e in _context.Empleados
                                   join p in _context.Personas on e.PersonaId equals p.PersonaId
                                   where e.Estado != "Borrado"
                                   select new
                                   {
                                       Empleado = e,
                                       Persona = p
                                   }).ToListAsync();

            // Mapear los resultados a DTOs usando el método toDTO()
            var empleadoDTOs = empleados.Select(item =>
            {
                var empleadoDTO = item.Empleado.toEmpleadoDTO();
                empleadoDTO.Persona = item.Persona.toPersonaDTO();
                return empleadoDTO;
            }).ToList();

            return empleadoDTOs;
        }

        /// <summary>
        /// Obtiene todos los empleados marcados como borrados.
        /// </summary>
        public async Task<List<EmpleadoDTO>> GetEmpleadoBorrados()
        {
            // Obtener empleados y sus personas asociadas en una sola consulta
            var empleados = await (from e in _context.Empleados
                                   join p in _context.Personas on e.PersonaId equals p.PersonaId
                                   where e.Estado == "Borrado"
                                   select new
                                   {
                                       Empleado = e,
                                       Persona = p
                                   }).ToListAsync();

            // Mapear los resultados a DTOs usando el método toDTO()
            var empleadoDTOs = empleados.Select(item =>
            {
                var empleadoDTO = item.Empleado.toEmpleadoDTO();
                empleadoDTO.Persona = item.Persona.toPersonaDTO();
                return empleadoDTO;
            }).ToList();

            return empleadoDTOs;
        }

        /// <summary>
        /// Crea un nuevo empleado.
        /// </summary>
        public async Task<EmpleadoDTO> PostEmpleado(string ci, string codigo, DateTime fechaingreso)
        {
            if (fechaingreso == default)
                fechaingreso = DateTime.Now;
            var persona = await _context.Personas
                .FirstOrDefaultAsync(h => h.CI == ci && h.Estado == "Activo");
            if (persona == null) return null;
            var empleado = new Empleado
            {
                Codigo = codigo,
                PersonaId = persona.PersonaId,
                FechaIngreso = fechaingreso,
                Estado = "Activo"
            };
            _context.Empleados.Add(empleado);
            await _context.SaveChangesAsync();
            var empDTO = empleado.toEmpleadoDTO();
            empDTO.Persona = persona.toPersonaDTO();
            return empDTO;
        }

        /// <summary>
        /// Actualiza los datos de un empleado existente.
        /// </summary>
        public async Task<EmpleadoDTO> PutEmpleado(string codigo, string codigoNuevo, string ci)
        {
            var empleado = await _context.Empleados
                .Where(e => e.Estado != "Borrado" && e.Codigo == codigo)
                .FirstOrDefaultAsync();
            var persona = await _context.Personas
                .FirstOrDefaultAsync(h => h.CI == ci && h.Estado == "Activo");
            if (empleado == null || persona == null)
                return null;

            empleado.Codigo = codigoNuevo;

            await _context.SaveChangesAsync();
            var empDTO = empleado.toEmpleadoDTO();
            empDTO.Persona = persona.toPersonaDTO();
            return empDTO;
        }

        /// <summary>
        /// Marca un empleado como borrado (eliminacodigoón lógica).
        /// </summary>
        public async Task<EmpleadoDTO> DeleteEmpleado(string codigo, string ci)
        {
            var empleado = await _context.Empleados
                .Where(e => e.Estado == "Activo" && e.Codigo == codigo)
                .FirstOrDefaultAsync();

            var persona = await _context.Personas
                .FirstOrDefaultAsync(h => h.CI == ci && h.Estado == "Activo");
            if (empleado == null || persona == null)
                return null;

            if (empleado.Estado != "Activo")
                return empleado.toEmpleadoDTO(); // Ya está inhabilitado, no necesita cambios

            empleado.Estado = "Borrado";
            await _context.SaveChangesAsync();
            var empDTO = empleado.toEmpleadoDTO();
            empDTO.Persona = persona.toPersonaDTO();
            return empDTO;
        }

        /// <summary>
        /// Habilita un empleado previamente marcado como "Borrado".
        /// </summary>
        public async Task<EmpleadoDTO?> HabilitarEmpleado(string codigo, string ci)
        {
            var empleado = await _context.Empleados
                .Where(e => e.Estado == "Borrado" && e.Codigo == codigo)
                .FirstOrDefaultAsync();

            var persona = await _context.Personas
                .FirstOrDefaultAsync(h => h.CI == ci && h.Estado == "Activo");
            if (empleado == null || persona == null)
                return null;

            if (empleado.Estado == "Activo")
                return empleado.toEmpleadoDTO(); // Ya está activo, no necesita cambios

            empleado.Estado = "Activo";
            await _context.SaveChangesAsync();
            var empDTO = empleado.toEmpleadoDTO();
            empDTO.Persona = persona.toPersonaDTO();
            return empDTO;
        }
    }
}
