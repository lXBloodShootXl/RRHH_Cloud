using Microsoft.EntityFrameworkCore;
using RRHH.Core.Interfaces;
using RRHH.Infraestructura.Data;
using RRHH.Infraestructura.Repositorio;

var builder = WebApplication.CreateBuilder(args);

// Obtener la cadena de conexión desde las variables de entorno
var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL")
                       ?? builder.Configuration.GetConnectionString("RRHHContext");

// Configurar DbContext con Npgsql
builder.Services.AddDbContext<RRHH_DBContext>(options =>
    options.UseNpgsql(connectionString, npgsqlOptions =>
    {
        npgsqlOptions.EnableRetryOnFailure(); // Intentar reconectar en caso de fallo
    }));

// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyApp", policyBuilder =>
    {
        policyBuilder.AllowAnyOrigin();
        policyBuilder.AllowAnyHeader();
        policyBuilder.AllowAnyMethod();
    });
});

// Añadir controladores, Swagger y la API de endpoints
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registrar repositorios
builder.Services.AddScoped<IDepartamentoRepositorio, DepartamentoRepositorio>();
builder.Services.AddScoped<IEmailRepositorio, EmailRepositorio>();
builder.Services.AddScoped<IHistorialRepositorio, HistorialRepositorio>();
builder.Services.AddScoped<IPersonaRepositorio, PersonaRepositorio>();
builder.Services.AddScoped<IPuestoRepositorio, PuestoRepositorio>();
builder.Services.AddScoped<IEmpleadoRepositorio, EmpleadoRepositorio>();
builder.Services.AddScoped<INominaRepositorio, NominaRepositorio>();
builder.Services.AddScoped<IReporteEmpleadoRepositorio, ReporteEmpleadoRepositorio>();
builder.Services.AddScoped<IEmpleadoCurriculumRepositorio, EmpleadoCurriculumRepositorio>();

var app = builder.Build();

// Aplicar migraciones al iniciar la aplicación
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<RRHH_DBContext>();
    try
    {
        dbContext.Database.Migrate(); // Aplica migraciones si no existen
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error aplicando migraciones: " + ex.Message);
    }

    // Ejecutar creación de vistas en la base de datos
    await CrearVistas(dbContext);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware
app.UseCors("MyApp");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

// Función para crear las vistas en la base de datos
async Task CrearVistas(RRHH_DBContext dbContext)
{
    try
    {
        var sql1 = @"
            CREATE OR REPLACE VIEW vw_EmpleadosActivos AS
            SELECT 
                e.""Codigo"" AS ""CodigoEmpleado"", 
                p.""CI"", 
                p.""Nombre"", 
                p.""ApellidoPaterno"", 
                p.""ApellidoMaterno"", 
                p.""FechaNacimiento"", 
                p.""Sexo"", 
                e.""FechaIngreso""
            FROM 
                public.""Empleados"" e
            JOIN 
                public.""Personas"" p ON e.""PersonaId"" = p.""PersonaId""
            WHERE 
                e.""Estado"" = 'Activo';
        ";
        var sql2 = @"
            CREATE OR REPLACE VIEW vw_HistorialDepartamentos AS
            SELECT 
                h.""EmpleadoId"", 
                e.""Codigo"" AS ""CodigoEmpleado"", 
                d.""Codigo"" AS ""CodigoDepartamento"", 
                d.""Nombre"" AS ""NombreDepartamento"", 
                p.""Codigo"" AS ""CodigoPuesto"", 
                p.""Nombre"" AS ""NombrePuesto"", 
                h.""FechaInicio"", 
                h.""FechaFin"", 
                h.""Estado""
            FROM 
                public.""HistorialDepartamentos"" h
            JOIN 
                public.""Empleados"" e ON h.""EmpleadoId"" = e.""EmpleadoId""
            JOIN 
                public.""Departamentos"" d ON h.""DepartamentoId"" = d.""DepartamentoId""
            JOIN 
                public.""Puestos"" p ON h.""PuestoId"" = p.""PuestoId""
            WHERE 
                h.""Estado"" = 'Activo';
        ";
        var sql3 = @"
            CREATE OR REPLACE VIEW vw_ResumenNominaEmpleado AS
            SELECT 
                n.""NominaId"", 
                e.""Codigo"" AS ""CodigoEmpleado"", 
                e.""FechaIngreso"", 
                n.""PeriodoInicio"", 
                n.""PeriodoFin"", 
                n.""SalarioBase"", 
                n.""Bonos"", 
                n.""Descuentos"", 
                n.""TotalNeto"", 
                n.""Estado"" AS ""EstadoNomina""
            FROM 
                public.""Nominas"" n
            JOIN 
                public.""Empleados"" e ON n.""EmpleadoId"" = e.""EmpleadoId""
            WHERE 
                n.""Estado"" = 'Activo';
        ";
        var sql4 = @"
            CREATE OR REPLACE VIEW vw_ReportesEmpleados AS
            SELECT 
                r.""ReporteId"", 
                e.""Codigo"" AS ""CodigoEmpleadoReportado"", 
                d.""Codigo"" AS ""CodigoDepartamentoEmisor"", 
                r.""Fecha"", 
                r.""Tipo"", 
                r.""Descripcion"", 
                r.""Estado"" AS ""EstadoReporte""
            FROM 
                public.""ReportesEmpleados"" r
            JOIN 
                public.""Empleados"" e ON r.""EmpleadoReportadoId"" = e.""EmpleadoId""
            JOIN 
                public.""Departamentos"" d ON r.""DepartamentoEmisorId"" = d.""DepartamentoId""
            WHERE 
                r.""Estado"" = 'Activo';
        ";
        var sql5 = @"
            CREATE OR REPLACE VIEW vw_EmpleadosSalariosPuestos AS
            SELECT 
                e.""EmpleadoId"", 
                e.""Codigo"" AS ""CodigoEmpleado"", 
                p.""Nombre"" AS ""NombreEmpleado"", 
                p.""ApellidoPaterno"", 
                p.""ApellidoMaterno"", 
                s.""SalarioBase"", 
                pue.""Nombre"" AS ""NombrePuesto"", 
                e.""FechaIngreso"", 
                e.""Estado"" AS ""EstadoEmpleado""
            FROM 
                public.""Empleados"" e
            JOIN 
                public.""Personas"" p ON e.""PersonaId"" = p.""PersonaId""
            JOIN 
                public.""Nominas"" s ON e.""EmpleadoId"" = s.""EmpleadoId""
            JOIN 
                public.""HistorialDepartamentos"" h ON e.""EmpleadoId"" = h.""EmpleadoId""
            JOIN 
                public.""Puestos"" pue ON h.""PuestoId"" = pue.""PuestoId""
            WHERE 
                e.""Estado"" = 'Activo' 
                AND s.""Estado"" = 'Activo' 
                AND pue.""Estado"" = 'Activo';
        ";

        await dbContext.Database.ExecuteSqlRawAsync(sql1);
        await dbContext.Database.ExecuteSqlRawAsync(sql2);
        await dbContext.Database.ExecuteSqlRawAsync(sql3);
        await dbContext.Database.ExecuteSqlRawAsync(sql4);
        await dbContext.Database.ExecuteSqlRawAsync(sql5);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error creando las vistas: {ex.Message}");
    }
}
