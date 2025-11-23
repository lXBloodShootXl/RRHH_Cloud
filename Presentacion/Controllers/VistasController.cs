using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RRHH.Core.Models.Views;
using RRHH.Infraestructura.Data;

[Route("api/[controller]")]
[ApiController]
public class VistasController : ControllerBase
{
    private readonly RRHH_DBContext _context;

    public VistasController(RRHH_DBContext context)
    {
        _context = context;
    }

    // Obtener Empleados Activos
    [HttpGet("empleados-activos")]
    public async Task<ActionResult<IEnumerable<EmpleadosActivosView>>> GetEmpleadosActivos()
    {
        var empleadosActivos = await _context
            .Set<EmpleadosActivosView>()
            .FromSqlRaw("SELECT * FROM public.\"vw_empleadosactivos\"")
            .ToListAsync();

        return Ok(empleadosActivos);
    }

    // Obtener Historial de Departamentos
    [HttpGet("historial-departamentos")]
    public async Task<ActionResult<IEnumerable<HistorialDepartamentosView>>> GetHistorialDepartamentos()
    {
        var historialDepartamentos = await _context
            .Set<HistorialDepartamentosView>()
            .FromSqlRaw("SELECT * FROM public.\"vw_historialdepartamentos\"")
            .ToListAsync();

        return Ok(historialDepartamentos);
    }

    // Obtener Resumen de Nómina del Empleado
    [HttpGet("resumen-nomina")]
    public async Task<ActionResult<IEnumerable<ResumenNominaEmpleadoView>>> GetResumenNomina()
    {
        var resumenNomina = await _context
            .Set<ResumenNominaEmpleadoView>()
            .FromSqlRaw("SELECT * FROM public.\"vw_resumennominaempleado\"")
            .ToListAsync();

        return Ok(resumenNomina);
    }

    // Obtener Reportes de Empleados
    [HttpGet("reportes-empleados")]
    public async Task<ActionResult<IEnumerable<ReportesEmpleadosView>>> GetReportesEmpleados()
    {
        var reportesEmpleados = await _context
            .Set<ReportesEmpleadosView>()
            .FromSqlRaw("SELECT * FROM public.\"vw_reportesempleados\"")
            .ToListAsync();

        return Ok(reportesEmpleados);
    }

    // Obtener Empleados con Salarios y Puestos
    [HttpGet("empleados-salarios-puestos")]
    public async Task<ActionResult<IEnumerable<EmpleadosSalariosPuestosView>>> GetEmpleadosSalariosPuestos()
    {
        var empleadosSalariosPuestos = await _context
            .Set<EmpleadosSalariosPuestosView>()
            .FromSqlRaw("SELECT * FROM public.\"vw_empleadossalariospuestos\"")
            .ToListAsync();

        return Ok(empleadosSalariosPuestos);
    }
}
