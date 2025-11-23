using Microsoft.AspNetCore.Mvc;
using RRHH.Core.Interfaces;

namespace RRHH.Presentacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadosController : ControllerBase
    {
        private readonly IEmpleadoRepositorio _EmpleadoRepositorio;

        public EmpleadosController(IEmpleadoRepositorio EmpleadoRepositorio)
        {
            _EmpleadoRepositorio = EmpleadoRepositorio;
        }

        /// <summary>
        /// Obtiene la lista de empleados activos.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetEmpleado()
        {
            var empleados = await _EmpleadoRepositorio.GetEmpleado();
            return Ok(empleados);
        }
        /// <summary>
        /// Obtiene un Empleado por su CI.
        /// </summary>
        [HttpGet("{codigo}")]
        public async Task<IActionResult> GetEmpleado(string codigo)
        {
            var Empleado = await _EmpleadoRepositorio.GetEmpleado(codigo);
            if (Empleado is null)
                return NotFound($"No se encontró un Empleado con codigo {codigo}.");

            return Ok(Empleado);
        }

        /// <summary>
        /// Obtiene la lista de empleados marcados como borrados.
        /// </summary>
        [HttpGet("borrados")]
        public async Task<IActionResult> GetEmpleadosBorrados()
        {
            var empleados = await _EmpleadoRepositorio.GetEmpleadoBorrados();
            return Ok(empleados);
        }

        /// <summary>
        /// Crea un nuevo Empleado.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> PostEmpleado(string ci, string codigo, DateTime fechaingreso)
        {
            if (string.IsNullOrWhiteSpace(ci) || string.IsNullOrWhiteSpace(codigo))
                return BadRequest("Los campos CI y código son obligatorios.");
            //var EmpleadoCreado = await _EmpleadoRepositorio.PostEmpleado(ci, codigo, fechaingreso);

            //return CreatedAtAction(nameof(GetEmpleado), new { codigo = EmpleadoCreado.Codigo }, EmpleadoCreado);
            return Ok(await _EmpleadoRepositorio.PostEmpleado(ci, codigo, fechaingreso));        
        }

        /// <summary>
        /// Actualiza un Empleado existente.
        /// </summary>
        [HttpPut("{codigo}")]
        public async Task<IActionResult> PutEmpleado(string codigo, string codigoNuevo, string ci)
        {
            if (string.IsNullOrWhiteSpace(codigo) || string.IsNullOrWhiteSpace(codigoNuevo))
                return BadRequest("Debe llenar todos los campos del Empleado.");

            var Empleado = await _EmpleadoRepositorio.PutEmpleado(codigo, codigoNuevo, ci);
            if (Empleado is null)
                return NotFound($"No se encontró el Empleado con código {codigo}.");

            return Ok(Empleado);
        }

        /// <summary>
        /// Marca un Empleado como borrado (eliminación lógica).
        /// </summary>
        [HttpDelete("{codigo}")]
        public async Task<IActionResult> DeleteEmpleado(string codigo, string ci)
        {
            var EmpleadoEliminado = await _EmpleadoRepositorio.DeleteEmpleado(codigo, ci);
            if (EmpleadoEliminado is null)
                return NotFound($"No se encontró un Empleado con CI {codigo}.");

            return Ok(EmpleadoEliminado);
        }
        /// <summary>
        /// Habilita un Empleado previamente borrado (reactivación lógica).
        /// </summary>
        [HttpPut("{codigo}/habilitar")]
        public async Task<IActionResult> HabilitarEmpleado(string codigo, string ci)
        {
            var Empleado = await _EmpleadoRepositorio.HabilitarEmpleado(codigo, ci);

            if (Empleado is null)
                return NotFound($"No se encontró un Empleado con codigo {codigo}.");

            return Ok(Empleado);
        }
    }
}
