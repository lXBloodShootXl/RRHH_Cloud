using Microsoft.EntityFrameworkCore;
using RRHH.Core.DTOs;
using RRHH.Core.Interfaces;
using RRHH.Core.Mapedores;
using RRHH.Core.Models;
using RRHH.Infraestructura.Data;

namespace RRHH.Infraestructura.Repositorio
{
    public class DepartamentoRepositorio : IDepartamentoRepositorio
    {
        private readonly RRHH_DBContext _context;

        public DepartamentoRepositorio(RRHH_DBContext context)
        {
            _context = context;
        }

        public async Task<DepartamentoDTO?> GetDepartamento(string codigo)
        {
            return await _context.Departamentos
                .AsNoTracking()
                .Where(d => d.Codigo == codigo && d.Estado != "Borrado")
                .Select(d => d.toDepartamentoDTO())
                .FirstOrDefaultAsync();
        }

        public async Task<List<DepartamentoDTO>> GetDepartamento()
        {
            return await _context.Departamentos
                .AsNoTracking()
                .Where(d => d.Estado != "Borrado")
                .Select(d => d.toDepartamentoDTO())
                .ToListAsync();
        }

        public async Task<List<DepartamentoDTO>> GetDepartamentoBorrados()
        {
            return await _context.Departamentos
                .AsNoTracking()
                .Where(d => d.Estado == "Borrado")
                .Select(d => d.toDepartamentoDTO())
                .ToListAsync();
        }

        public async Task<DepartamentoDTO> PostDepartamento(string codigo, string nombre)
        {
            var departamento = new Departamento { Codigo = codigo, Nombre = nombre, Estado = "Activo" };
            _context.Departamentos.Add(departamento);
            await _context.SaveChangesAsync();
            return departamento.toDepartamentoDTO();
        }

        public async Task<DepartamentoDTO?> PutDepartamento(string codigo, string nombreNuevo, string codigoNuevo)
        {
            var departamento = await _context.Departamentos
                .FirstOrDefaultAsync(d => d.Codigo == codigo && d.Estado != "Borrado");

            if (departamento == null)
                return null;

            departamento.Codigo = codigoNuevo;
            departamento.Nombre = nombreNuevo;
            await _context.SaveChangesAsync();
            return departamento.toDepartamentoDTO();
        }

        public async Task<DepartamentoDTO?> DeleteDepartamento(string codigo)
        {
            var departamento = await _context.Departamentos
                .FirstOrDefaultAsync(d => d.Codigo == codigo && d.Estado == "Activo");

            if (departamento == null)
                return null;

            departamento.Estado = "Borrado";
            await _context.SaveChangesAsync();
            return departamento.toDepartamentoDTO();
        }

        public async Task<DepartamentoDTO?> HabilitarDepartamento(string codigo)
        {
            var departamento = await _context.Departamentos
                .FirstOrDefaultAsync(d => d.Codigo == codigo && d.Estado == "Borrado");

            if (departamento == null)
                return null;

            departamento.Estado = "Activo";
            await _context.SaveChangesAsync();
            return departamento.toDepartamentoDTO();
        }
    }
}
