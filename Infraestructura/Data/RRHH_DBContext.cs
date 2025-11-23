using Microsoft.EntityFrameworkCore;
using RRHH.Core.Models;
using RRHH.Core.Models.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RRHH.Infraestructura.Data
{
    public class RRHH_DBContext : DbContext
    {
        public RRHH_DBContext(DbContextOptions<RRHH_DBContext> options) : base(options) { }

        public DbSet<Persona> Personas { get; set; } = default!;

        public DbSet<Telefono> Telefonos { get; set; } = default!;

        public DbSet<Email> Emails { get; set; } = default!;

        public DbSet<Departamento> Departamentos { get; set; } = default!;

        public DbSet<Puesto> Puestos { get; set; } = default!;

        public DbSet<Empleado> Empleados { get; set; } = default!;

        public DbSet<PersonaTelefono> PersonaTelefonos { get; set; } = default!;

        public DbSet<PersonaEmail> PersonaEmails { get; set; } = default!;

        public DbSet<Historial> HistorialDepartamentos { get; set; } = default!;

        public DbSet<Nomina> Nominas { get; set; } = default!;

        public DbSet<ReporteEmpleado> ReportesEmpleados { get; set; } = default!;
        public DbSet<EmpleadoCurriculum> EmpleadoCurriculums { get; set; } = default!;

        public DbSet<EmpleadosActivosView> EmpleadosActivosViews { get; set; }
        public DbSet<HistorialDepartamentosView> HistorialDepartamentosViews { get; set; }
        public DbSet<ResumenNominaEmpleadoView> ResumenNominaEmpleadoViews { get; set; }
        public DbSet<ReportesEmpleadosView> ReportesEmpleadosViews { get; set; }
        public DbSet<EmpleadosSalariosPuestosView> EmpleadosSalariosPuestosViews { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Recorre todas las entidades y propiedades DateTime
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    // Si la propiedad es DateTime o DateTime?
                    if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
                    {
                        property.SetColumnType("date"); // Se guarda como "date" en PostgreSQL
                    }
                }
            }
            // Claves compuestas
            modelBuilder.Entity<Empleado>()
            .HasOne(u => u.Persona)          // Un Usuario tiene un Perfil
            .WithOne(p => p.Empleado)        // Un Perfil pertenece a un Usuario
            .HasForeignKey<Empleado>(p => p.PersonaId);
            modelBuilder.Entity<PersonaTelefono>().HasKey(pt => new { pt.PersonaId, pt.TelefonoId, pt.FechaInicio });
            modelBuilder.Entity<PersonaEmail>().HasKey(pe => new { pe.PersonaId, pe.EmailId, pe.FechaInicio });
            modelBuilder.Entity<Historial>().HasKey(hd => new { hd.EmpleadoId, hd.DepartamentoId, hd.FechaInicio });
            modelBuilder.Entity<EmpleadosActivosView>().HasNoKey();
            modelBuilder.Entity<HistorialDepartamentosView>().HasNoKey();
            modelBuilder.Entity<ResumenNominaEmpleadoView>().HasNoKey();
            modelBuilder.Entity<ReportesEmpleadosView>().HasNoKey();
            modelBuilder.Entity<EmpleadosSalariosPuestosView>().HasNoKey();
        }
    }
}
