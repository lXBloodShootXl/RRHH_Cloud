using Microsoft.EntityFrameworkCore;
using Npgsql;
using RRHH.Core.Interfaces;
using RRHH.Infraestructura.Data;
using RRHH.Infraestructura.Repositorio;
var builder = WebApplication.CreateBuilder(args);
var url = Environment.GetEnvironmentVariable("DATABASE_URL");
/*Console.WriteLine($"La cadena de conexión es: {url}");
try
{
    using (var conn = new NpgsqlConnection(url))
    {
        conn.Open();
        Console.WriteLine("Conexión exitosa");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error al conectar: {ex.Message}");
}*/
builder.WebHost.UseUrls("http://0.0.0.0:8080");

//Registrar el DbContext con la cadena de conexión de PostgreSQL
builder.Services.AddDbContext<RRHH_DBContext>(options =>
    options.UseNpgsql(url));
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDepartamentoRepositorio, DepartamentoRepositorio>();
builder.Services.AddScoped<IEmailRepositorio, EmailRepositorio>();
builder.Services.AddScoped<IHistorialRepositorio, HistorialRepositorio>();
builder.Services.AddScoped<IPersonaRepositorio, PersonaRepositorio>();
builder.Services.AddScoped<IPuestoRepositorio, PuestoRepositorio>();
builder.Services.AddScoped<IEmpleadoRepositorio, EmpleadoRepositorio>();
builder.Services.AddScoped<INominaRepositorio, NominaRepositorio>();
builder.Services.AddScoped<IReporteEmpleadoRepositorio, ReporteEmpleadoRepositorio>();
builder.Services.AddScoped<IEmpleadoCurriculumRepositorio, EmpleadoCurriculumRepositorio>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyApp", policybuilder =>
    {
        policybuilder.AllowAnyOrigin();
        policybuilder.AllowAnyHeader();
        policybuilder.AllowAnyMethod();
    });
});

var app = builder.Build();

//Vistas
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<RRHH_DBContext>();

    // Ejecuta SQL para crear la vista en la base de datos
    dbContext.Database.Migrate();
    await CrearVistas(dbContext);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("MyApp");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

async Task CrearVistas(RRHH_DBContext dbContext)
{
    try
    {
        var sql = @"
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
        
        await dbContext.Database.ExecuteSqlRawAsync(sql);
        await dbContext.Database.ExecuteSqlRawAsync(sql2);
        await dbContext.Database.ExecuteSqlRawAsync(sql3);
        await dbContext.Database.ExecuteSqlRawAsync(sql4);
        await dbContext.Database.ExecuteSqlRawAsync(sql5);
    }
    catch (Exception ex)
    {
        // Manejar el error, por ejemplo, si la vista ya existe
        Console.WriteLine($"Error creando la vista: {ex.Message}");
    }
}