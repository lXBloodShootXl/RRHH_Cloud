using Microsoft.AspNetCore.Mvc;
using RRHH.Core.Interfaces;

namespace RRHH.Presentacodigo_depon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReporteEmpleadoController : ControllerBase
    {
        private readonly IReporteEmpleadoRepositorio _ReporteEmpleadoRepositorio;

        public ReporteEmpleadoController(IReporteEmpleadoRepositorio ReporteEmpleadoRepositorio)
        {
            _ReporteEmpleadoRepositorio = ReporteEmpleadoRepositorio;
        }

        /// <summary>
        /// Obtiene la lista de reporteempleados activos.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetReporteEmpleado()
        {
            var reporteempleados = await _ReporteEmpleadoRepositorio.GetReporteEmpleado();
            return Ok(reporteempleados);
        }
        /// <summary>
        /// Obtiene un ReporteEmpleado por su codigos.
        /// </summary>
        [HttpGet("/{cod_dep}")]
        public async Task<IActionResult> GetReporteEmpleado(string codigo_emp, string cod_dep, DateTime fecha)
        {
            var ReporteEmpleado = await _ReporteEmpleadoRepositorio.GetReporteEmpleado(codigo_emp, cod_dep, fecha);
            if (ReporteEmpleado is null)
                return NotFound($"No se encontró un ReporteEmpleado con codigo {codigo_emp} y {cod_dep}.");

            return Ok(ReporteEmpleado);
        }

        /// <summary>
        /// Obtiene la lista de reporteempleados marcados como borrados.
        /// </summary>
        [HttpGet("/borrados/")]
        public async Task<IActionResult> GetReporteEmpleadosBorrados()
        {
            var reporteempleados = await _ReporteEmpleadoRepositorio.GetReporteEmpleadoBorrados();
            return Ok(reporteempleados);
        }

        /// <summary>
        /// Crea un nuevo ReporteEmpleado.
        /// </summary>
        [HttpPost("/")]
        public async Task<IActionResult> PostReporteEmpleado(string cod_emp, string cod_dep, DateTime fecha, string tipo, string desc)
        {
            if (string.IsNullOrWhiteSpace(cod_dep) || string.IsNullOrWhiteSpace(cod_emp))
                return BadRequest("Los campos de códigos y fecha de inicio son obligatorios.");

            var ReporteEmpleadoCreado = await _ReporteEmpleadoRepositorio.PostReporteEmpleado(cod_emp, cod_dep, fecha, tipo, desc);
            if (ReporteEmpleadoCreado == null)
                return BadRequest("Error en la conexión o Registro no encontrado o inexistente");
            return CreatedAtAction(nameof(GetReporteEmpleado), new { codigo_dep = ReporteEmpleadoCreado.Cod_Dep }, ReporteEmpleadoCreado);
        }

        /// <summary>
        /// Actualiza un ReporteEmpleado existente.
        /// </summary>
        [HttpPut("/{cod_dep}/{cod_emp}")]
        public async Task<IActionResult> PutReporteEmpleado(string cod_emp, string cod_dep, DateTime fecha, string tipo, string desc)
        {
            if (string.IsNullOrWhiteSpace(cod_emp) || string.IsNullOrWhiteSpace(cod_dep))
                return BadRequest("Debe llenar todos los campos del ReporteEmpleado.");

            var ReporteEmpleado = await _ReporteEmpleadoRepositorio.PutReporteEmpleado(cod_emp, cod_dep, fecha, tipo, desc);
            if (ReporteEmpleado is null)
                return NotFound($"No se encontró el ReporteEmpleado con código {cod_emp} y {cod_dep}.");

            return Ok(ReporteEmpleado);
        }

        /// <summary>
        /// Marca un ReporteEmpleado como borrado (eliminacodigo_depón lógica).
        /// </summary>
        [HttpDelete("/{cod_dep}/{cod_emp}")]
        public async Task<IActionResult> DeleteReporteEmpleado(string cod_emp, string cod_dep, DateTime fecha)
        {
            var ReporteEmpleadoEliminado = await _ReporteEmpleadoRepositorio.DeleteReporteEmpleado(cod_emp, cod_dep, fecha);
            if (ReporteEmpleadoEliminado is null)
                return NotFound($"No se encontró un ReporteEmpleado con codigos {cod_emp} y {cod_dep}.");

            return Ok(ReporteEmpleadoEliminado);
        }
        /// <summary>
        /// Habilita un ReporteEmpleado previamente borrado (reactivacodigo_depón lógica).
        /// </summary>
        [HttpPut("/{cod_dep}/{cod_emp}/habilitar")]
        public async Task<IActionResult> HabilitarReporteEmpleado(string cod_emp, string cod_dep, DateTime fecha)
        {
            var ReporteEmpleado = await _ReporteEmpleadoRepositorio.HabilitarReporteEmpleado(cod_emp, cod_dep, fecha);

            if (ReporteEmpleado is null)
                return NotFound($"No se encontró un ReporteEmpleado con codigo {cod_emp}.");

            return Ok(ReporteEmpleado);
        }
    }
}
