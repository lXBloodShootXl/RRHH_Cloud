using Microsoft.EntityFrameworkCore;
using RRHH.Core.DTOs;
using RRHH.Core.Interfaces;
using RRHH.Core.Mapedores;
using RRHH.Core.Models;
using RRHH.Infraestructura.Data;

namespace RRHH.Infraestructura.Repositorio
{
    public class PuestoRepositorio : IPuestoRepositorio
    {
        private readonly RRHH_DBContext _context;

        public PuestoRepositorio(RRHH_DBContext context)
        {
            _context = context;
        }

        public async Task<PuestoDTO?> GetPuesto(string codigo)
        {
            return await _context.Puestos
                .AsNoTracking()
                .Where(p => p.Codigo == codigo && p.Estado != "Borrado")
                .Select(p => p.toPuestoDTO())
                .FirstOrDefaultAsync();
        }

        public async Task<List<PuestoDTO>> GetPuesto()
        {
            return await _context.Puestos
                .AsNoTracking()
                .Where(p => p.Estado != "Borrado")
                .Select(p => p.toPuestoDTO())
                .ToListAsync();
        }

        public async Task<List<PuestoDTO>> GetPuestoBorrados()
        {
            return await _context.Puestos
                .AsNoTracking()
                .Where(p => p.Estado == "Borrado")
                .Select(p => p.toPuestoDTO())
                .ToListAsync();
        }

        public async Task<PuestoDTO> PostPuesto(string codigo, string nombre)
        {
            var puesto = new Puesto { Codigo = codigo, Nombre = nombre, Estado = "Activo" };
            _context.Puestos.Add(puesto);
            await _context.SaveChangesAsync();
            return puesto.toPuestoDTO();
        }

        public async Task<PuestoDTO?> PutPuesto(string codigo, string nuevoNombre)
        {
            var puesto = await _context.Puestos.FirstOrDefaultAsync(p => p.Codigo == codigo && p.Estado != "Borrado");
            if (puesto == null) return null;
            puesto.Nombre = nuevoNombre;
            await _context.SaveChangesAsync();
            return puesto.toPuestoDTO();
        }

        public async Task<PuestoDTO?> DeletePuesto(string codigo)
        {
            var puesto = await _context.Puestos.FirstOrDefaultAsync(p => p.Codigo == codigo && p.Estado == "Activo");
            if (puesto == null) return null;
            puesto.Estado = "Borrado";
            await _context.SaveChangesAsync();
            return puesto.toPuestoDTO();
        }

        public async Task<PuestoDTO?> HabilitarPuesto(string codigo)
        {
            var puesto = await _context.Puestos.FirstOrDefaultAsync(p => p.Codigo == codigo && p.Estado == "Borrado");
            if (puesto == null) return null;
            puesto.Estado = "Activo";
            await _context.SaveChangesAsync();
            return puesto.toPuestoDTO();
        }
    }
}
