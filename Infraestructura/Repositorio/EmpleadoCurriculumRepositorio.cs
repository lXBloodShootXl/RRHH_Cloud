using Microsoft.EntityFrameworkCore;
using RRHH.Core.DTOs;
using RRHH.Core.Interfaces;
using RRHH.Core.Mapedores;
using RRHH.Core.Models;
using RRHH.Infraestructura.Data;

namespace RRHH.Infraestructura.Repositorio
{
    public class EmpleadoCurriculumRepositorio : IEmpleadoCurriculumRepositorio
    {
        private readonly RRHH_DBContext _context;

        public EmpleadoCurriculumRepositorio(RRHH_DBContext context)
        {
            _context = context;
        }

        public async Task<EmpleadoCurriculumDTO?> GetEmpleadoCurriculum(string cod_emp)
        {
            var e = await _context.Empleados
                .AsNoTracking()
                .Where(e => e.Codigo == cod_emp && e.Estado != "Borrado")
                //.Select(e => e.toEmpleadoDTO())
                .FirstOrDefaultAsync();
            if (e == null)
            {
                return null;
            }
            return await _context.EmpleadoCurriculums
                .AsNoTracking()
                .Where(p => p.Cod_Emp == cod_emp && p.Estado != "Borrado")
                .Select(p => p.toEmpleadoCurriculumDTO())
                .FirstOrDefaultAsync();
        }

        public async Task<List<EmpleadoCurriculumDTO>> GetEmpleadoCurriculum()
        {
            return await _context.EmpleadoCurriculums
                .AsNoTracking()
                .Where(p => p.Estado != "Borrado")
                .Select(p => p.toEmpleadoCurriculumDTO())
                .ToListAsync();
        }

        public async Task<List<EmpleadoCurriculumDTO>> GetEmpleadoCurriculumBorrados()
        {
            return await _context.EmpleadoCurriculums
                .AsNoTracking()
                .Where(p => p.Estado == "Borrado")
                .Select(p => p.toEmpleadoCurriculumDTO())
                .ToListAsync();
        }

        public async Task<EmpleadoCurriculumDTO> PostEmpleadoCurriculum(string cod_emp, string perfil, DateTime fecharecepcion)
        {
            if (string.IsNullOrWhiteSpace(cod_emp) || string.IsNullOrWhiteSpace(perfil))
                return null;
            var e = await _context.Empleados
                .AsNoTracking()
                .Where(e => e.Codigo == cod_emp && e.Estado != "Borrado")
                .FirstOrDefaultAsync();
            if (e == null)
            {
                return null;
            }
            var empleadocurriculum = new EmpleadoCurriculum
            {
                Cod_Emp = cod_emp,
                Perfil = perfil,
                FechaRecepcion = fecharecepcion,
                Estado = "Activo"
            };
            _context.EmpleadoCurriculums.Add(empleadocurriculum);
            await _context.SaveChangesAsync();
            return empleadocurriculum.toEmpleadoCurriculumDTO();
        }

        public async Task<EmpleadoCurriculumDTO?> PutEmpleadoCurriculum(string cod_emp, string perfil, DateTime fecharecepcion)
        {
            if (string.IsNullOrWhiteSpace(cod_emp) || string.IsNullOrWhiteSpace(perfil))
                return null;
            var empleadocurriculum = await _context.EmpleadoCurriculums.FirstOrDefaultAsync(p => p.Cod_Emp == cod_emp && p.Estado != "Borrado");
            if (empleadocurriculum == null)
                return null;
            empleadocurriculum.Cod_Emp = cod_emp;
            empleadocurriculum.Perfil = perfil;
            empleadocurriculum.FechaRecepcion = fecharecepcion;
            await _context.SaveChangesAsync();
            return empleadocurriculum.toEmpleadoCurriculumDTO();
        }

        public async Task<EmpleadoCurriculumDTO?> DeleteEmpleadoCurriculum(string cod_emp)
        {
            var empleadocurriculum = await _context.EmpleadoCurriculums.FirstOrDefaultAsync(p => p.Cod_Emp == cod_emp && p.Estado == "Activo");
            if (empleadocurriculum == null) return null;
            empleadocurriculum.Estado = "Borrado";
            await _context.SaveChangesAsync();
            return empleadocurriculum.toEmpleadoCurriculumDTO();
        }

        public async Task<EmpleadoCurriculumDTO?> HabilitarEmpleadoCurriculum(string cod_emp)
        {
            var empleadocurriculum = await _context.EmpleadoCurriculums.FirstOrDefaultAsync(p => p.Cod_Emp == cod_emp && p.Estado == "Borrado");
            if (empleadocurriculum == null) return null;
            empleadocurriculum.Estado = "Activo";
            await _context.SaveChangesAsync();
            return empleadocurriculum.toEmpleadoCurriculumDTO();
        }
    }
}
