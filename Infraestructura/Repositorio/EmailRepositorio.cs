using Microsoft.EntityFrameworkCore;
using RRHH.Core.DTOs;
using RRHH.Core.Interfaces;
using RRHH.Core.Mapedores;
using RRHH.Core.Models;
using RRHH.Infraestructura.Data;

namespace RRHH.Infraestructura.Repositorio
{
    public class EmailRepositorio : IEmailRepositorio
    {
        private readonly RRHH_DBContext _context;

        public EmailRepositorio(RRHH_DBContext context)
        {
            _context = context;
        }

        public async Task<EmailDTO?> GetEmail(string correo)
        {
            return await _context.Emails
                .AsNoTracking()
                .Where(e => e.Correo == correo && e.Estado != "Borrado")
                .Select(e => e.toEmailDTO())
                .FirstOrDefaultAsync();
        }

        public async Task<List<EmailDTO>> GetEmail()
        {
            return await _context.Emails
                .AsNoTracking()
                .Where(e => e.Estado != "Borrado")
                .Select(e => e.toEmailDTO())
                .ToListAsync();
        }

        public async Task<List<EmailDTO>> GetEmailBorrados()
        {
            return await _context.Emails
                .AsNoTracking()
                .Where(e => e.Estado == "Borrado")
                .Select(e => e.toEmailDTO())
                .ToListAsync();
        }

        public async Task<EmailDTO> PostEmail(string correo)
        {
            var email = new Email { Correo = correo, Estado = "Activo" };
            _context.Emails.Add(email);
            await _context.SaveChangesAsync();
            return email.toEmailDTO();
        }
        public async Task<EmailDTO?> PutEmail(string correo, string nuevoCorreo)
        {
            var email = await _context.Emails
                .FirstOrDefaultAsync(d => d.Correo == correo && d.Estado != "Borrado");

            if (email == null)
                return null;

            email.Correo = nuevoCorreo;
            await _context.SaveChangesAsync();
            return email.toEmailDTO();
        }
        public async Task<EmailDTO?> DeleteEmail(string correo)
        {
            var email = await _context.Emails.FirstOrDefaultAsync(e => e.Correo == correo && e.Estado == "Activo");
            if (email == null) return null;
            email.Estado = "Borrado";
            await _context.SaveChangesAsync();
            return email.toEmailDTO();
        }

        public async Task<EmailDTO?> HabilitarEmail(string correo)
        {
            var email = await _context.Emails.FirstOrDefaultAsync(e => e.Correo == correo && e.Estado == "Borrado");
            if (email == null) return null;
            email.Estado = "Activo";
            await _context.SaveChangesAsync();
            return email.toEmailDTO();
        }
    }
}
