using Microsoft.EntityFrameworkCore;
using RRHH.Core.DTOs;
using RRHH.Core.Interfaces;
using RRHH.Core.Mapedores;
using RRHH.Core.Models;
using RRHH.Infraestructura.Data;
using System.Reflection.Metadata;

namespace RRHH.Infraestructura.Repositorio
{
    public class NominaRepositorio : INominaRepositorio
    {
        private readonly RRHH_DBContext _context;

        public NominaRepositorio(RRHH_DBContext context)
        {
            _context = context;
        }

        public async Task<NominaDTO?> GetNomina(string cod_nom)
        {
            var nomina = await _context.Nominas
                .AsNoTracking()
                .Where(p => p.Cod_Nom == cod_nom && p.Estado != "Borrado")
                .Select(p => p.toNominaDTO())
                .FirstOrDefaultAsync();
            if (nomina == null)
                return null;
            var emp = await _context.Empleados
                .AsNoTracking()
                .Where(p => p.Codigo == nomina.Cod_Emp && p.Estado != "Borrado")
                //.Select(p => p.toEmpleadoDTO())
                .FirstOrDefaultAsync();
            if (emp == null)
                return null;
            return nomina;
        }

        public async Task<List<NominaDTO>> GetNomina()
        {
            return await _context.Nominas
                .AsNoTracking()
                .Where(p => p.Estado != "Borrado")
                .Select(p => p.toNominaDTO())
                .ToListAsync();
        }

        public async Task<List<NominaDTO>> GetNominaBorrados()
        {
            return await _context.Nominas
                .AsNoTracking()
                .Where(p => p.Estado == "Borrado")
                .Select(p => p.toNominaDTO())
                .ToListAsync();
        }

        public async Task<NominaDTO> PostNomina(string cod_nom, string cod_emp, DateTime periodoInicio, DateTime periodoFin, decimal salario, decimal bonos, decimal descuentos, decimal total)
        {
            if (string.IsNullOrWhiteSpace(cod_nom) || string.IsNullOrWhiteSpace(cod_emp) || decimal.IsNegative(salario) || decimal.IsNegative(bonos) || decimal.IsNegative(descuentos) || decimal.IsNegative(total))
                return null;
            var nomina = new Nomina
            {
                Cod_Nom = cod_nom,
                Cod_Emp = cod_emp,
                PeriodoInicio = periodoInicio,
                PeriodoFin = periodoFin,
                SalarioBase = salario,
                Bonos = bonos,
                Descuentos = descuentos,
                TotalNeto = total,
                Estado = "Activo"
            };
            _context.Nominas.Add(nomina);
            await _context.SaveChangesAsync();
            return nomina.toNominaDTO();
        }

        public async Task<NominaDTO?> PutNomina(string cod_nom, string cod_emp, decimal salario, decimal bonos, decimal descuentos, decimal total)
        {
            if (string.IsNullOrWhiteSpace(cod_nom) || string.IsNullOrWhiteSpace(cod_emp) || decimal.IsNegative(salario) || decimal.IsNegative(bonos) || decimal.IsNegative(descuentos) || decimal.IsNegative(total))
                return null;
            var nomina = await _context.Nominas.FirstOrDefaultAsync(p => p.Cod_Nom == cod_nom && p.Estado != "Borrado");
            if (nomina == null)
                return null;
            nomina.SalarioBase = salario;
            nomina.Bonos = bonos;
            nomina.Descuentos = descuentos;
            nomina.TotalNeto = total;
            await _context.SaveChangesAsync();
            return nomina.toNominaDTO();
        }

        public async Task<NominaDTO?> DeleteNomina(string cod_nom)
        {
            var nomina = await _context.Nominas.FirstOrDefaultAsync(p => p.Cod_Nom == cod_nom && p.Estado == "Activo");
            if (nomina == null) return null;
            nomina.Estado = "Borrado";
            await _context.SaveChangesAsync();
            return nomina.toNominaDTO();
        }

        public async Task<NominaDTO?> HabilitarNomina(string cod_nom)
        {
            var nomina = await _context.Nominas.FirstOrDefaultAsync(p => p.Cod_Nom == cod_nom && p.Estado == "Borrado");
            if (nomina == null) return null;
            nomina.Estado = "Activo";
            await _context.SaveChangesAsync();
            return nomina.toNominaDTO();
        }
    }
}
