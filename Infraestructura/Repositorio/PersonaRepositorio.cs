using Microsoft.EntityFrameworkCore;
using RRHH.Core.DTOs;
using RRHH.Core.Interfaces;
using RRHH.Core.Mapedores;
using RRHH.Core.Models;
using RRHH.Infraestructura.Data;

namespace RRHH.Infraestructura.Repositorio
{
    public class PersonaRepositorio : IPersonaRepositorio
    {
        private readonly RRHH_DBContext _context;

        public PersonaRepositorio(RRHH_DBContext context)
        {
            _context = context;
        }

        public async Task<PersonaDTO?> GetPersona(string ci)
        {
            return await _context.Personas
                .AsNoTracking()
                .Where(p => p.CI == ci && p.Estado != "Borrado")
                .Select(p => p.toPersonaDTO())
                .FirstOrDefaultAsync();
        }

        public async Task<List<PersonaDTO>> GetPersona()
        {
            return await _context.Personas
                .AsNoTracking()
                .Where(p => p.Estado != "Borrado")
                .Select(p => p.toPersonaDTO())
                .ToListAsync();
        }

        public async Task<List<PersonaDTO>> GetPersonaBorrados()
        {
            return await _context.Personas
                .AsNoTracking()
                .Where(p => p.Estado == "Borrado")
                .Select(p => p.toPersonaDTO())
                .ToListAsync();
        }

        public async Task<PersonaDTO> PostPersona(string ci, string nombre, string? apellidoPaterno, string? apellidoMaterno, string sexo, DateTime fechanacimiento)
        {
            if (string.IsNullOrWhiteSpace(ci) || string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(sexo))
                return null;
            var persona = new Persona
            {
                CI = ci,
                Nombre = nombre,
                ApellidoPaterno = apellidoPaterno,
                ApellidoMaterno = apellidoMaterno,
                Sexo = sexo,
                FechaNacimiento = fechanacimiento,
                Estado = "Activo"
            };
            _context.Personas.Add(persona);
            await _context.SaveChangesAsync();
            return persona.toPersonaDTO();
        }

        public async Task<PersonaDTO?> PutPersona(string ci, string ciNuevo, string nombre, string? apellidoPaterno, string? apellidoMaterno)
        {
            if (string.IsNullOrWhiteSpace(ci) || string.IsNullOrWhiteSpace(ciNuevo) || string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellidoPaterno) || string.IsNullOrWhiteSpace(apellidoMaterno))
                return null;
            var persona = await _context.Personas.FirstOrDefaultAsync(p => p.CI == ci && p.Estado != "Borrado");
            if (persona == null)
                return null;
            persona.CI = ciNuevo;
            persona.Nombre = nombre;
            persona.ApellidoPaterno = apellidoPaterno;
            persona.ApellidoMaterno = apellidoMaterno;
            await _context.SaveChangesAsync();
            return persona.toPersonaDTO();
        }

        public async Task<PersonaDTO?> DeletePersona(string ci)
        {
            var persona = await _context.Personas.FirstOrDefaultAsync(p => p.CI == ci && p.Estado == "Activo");
            if (persona == null) return null;
            persona.Estado = "Borrado";
            await _context.SaveChangesAsync();
            return persona.toPersonaDTO();
        }

        public async Task<PersonaDTO?> HabilitarPersona(string ci)
        {
            var persona = await _context.Personas.FirstOrDefaultAsync(p => p.CI == ci && p.Estado == "Borrado");
            if (persona == null) return null;
            persona.Estado = "Activo";
            await _context.SaveChangesAsync();
            return persona.toPersonaDTO();
        }
    }
}
