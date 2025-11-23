using Microsoft.AspNetCore.Mvc;
using RRHH.Core.Interfaces;

namespace RRHH.Presentacodigo_depon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistorialController : ControllerBase
    {
        private readonly IHistorialRepositorio _HistorialDepartamentoRepositorio;

        public HistorialController(IHistorialRepositorio HistorialDepartamentoRepositorio)
        {
            _HistorialDepartamentoRepositorio = HistorialDepartamentoRepositorio;
        }

        /// <summary>
        /// Obtiene la lista de historialdepartamentos activos.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetHistorialDepartamento(string codigo_emp, string codigo_puesto, string cod_dep)
        {
            var historialdepartamentos = await _HistorialDepartamentoRepositorio.GetHistorialDepartamento(codigo_emp, codigo_puesto, cod_dep);
            return Ok(historialdepartamentos);
        }
        /// <summary>
        /// Obtiene un Historial por su CI.
        /// </summary>
        [HttpGet("Departamento/{cod_dep}")]
        public async Task<IActionResult> GetHistorialDepartamento(string cod_dep)
        {
            var Historial = await _HistorialDepartamentoRepositorio.GetHistorialDepartamento(cod_dep);
            if (Historial is null)
                return NotFound($"No se encontró un Historial con codigo {cod_dep}.");

            return Ok(Historial);
        }

        /// <summary>
        /// Obtiene la lista de historialdepartamentos marcados como borrados.
        /// </summary>
        [HttpGet("Departamento/borrados/{cod_dep}")]
        public async Task<IActionResult> GetHistorialDepartamentosBorrados(string cod_dep)
        {
            var historialdepartamentos = await _HistorialDepartamentoRepositorio.GetHistorialDepartamentoBorrados(cod_dep);
            return Ok(historialdepartamentos);
        }

        [HttpGet("Departamento/borrados/{cod_dep}/{cod_emp}")]
        public async Task<IActionResult> GetHistorialDepartamentosBorrados(string cod_emp, string cod_dep)
        {
            var historialdepartamentos = await _HistorialDepartamentoRepositorio.GetHistorialDepartamentoBorrados(cod_emp, cod_dep);
            return Ok(historialdepartamentos);
        }

        /// <summary>
        /// Crea un nuevo Historial.
        /// </summary>
        [HttpPost("Departamento/")]
        public async Task<IActionResult> PostHistorialDepartamento(string cod_emp, string codigo_puesto, string cod_dep, DateTime fechaInicio, DateTime? fechaFin = null)
        {
            if (string.IsNullOrWhiteSpace(cod_dep) || string.IsNullOrWhiteSpace(cod_emp) || string.IsNullOrEmpty(codigo_puesto))
                return BadRequest("Los campos de códigos y fecha de inicio son obligatorios.");

            var HistorialDepartamentoCreado = await _HistorialDepartamentoRepositorio.PostHistorialDepartamento(cod_emp, codigo_puesto, cod_dep, fechaInicio, fechaFin);
            if (HistorialDepartamentoCreado == null)
                return BadRequest("Error en la conexión o Registro no encontrado o inexistente");
            return CreatedAtAction(nameof(GetHistorialDepartamento), new { codigo_dep = HistorialDepartamentoCreado.Codigo_Dep }, HistorialDepartamentoCreado);
        }

        /// <summary>
        /// Actualiza un Historial existente.
        /// </summary>
        [HttpPut("Departamento/{cod_dep}/{cod_emp}")]
        public async Task<IActionResult> PutHistorialDepartamento(string cod_emp, string codigo_puesto, string cod_dep, DateTime? nuevaFechaFin)
        {
            if (string.IsNullOrWhiteSpace(cod_emp) || string.IsNullOrWhiteSpace(cod_dep))
                return BadRequest("Debe llenar todos los campos del Historial.");

            var Historial = await _HistorialDepartamentoRepositorio.PutHistorialDepartamento(cod_emp, codigo_puesto, cod_dep, nuevaFechaFin);
            if (Historial is null)
                return NotFound($"No se encontró el Historial con código {cod_dep}.");

            return Ok(Historial);
        }

        /// <summary>
        /// Marca un Historial como borrado (eliminacodigo_depón lógica).
        /// </summary>
        [HttpDelete("Departamento/{cod_dep}/{cod_emp}")]
        public async Task<IActionResult> DeleteHistorialDepartamento(string cod_emp, string codigo_puesto, string cod_dep)
        {
            var HistorialDepartamentoEliminado = await _HistorialDepartamentoRepositorio.DeleteHistorialDepartamento(cod_emp, codigo_puesto, cod_dep);
            if (HistorialDepartamentoEliminado is null)
                return NotFound($"No se encontró un Historial con CI {cod_emp}.");

            return Ok(HistorialDepartamentoEliminado);
        }
        /// <summary>
        /// Habilita un Historial previamente borrado (reactivacodigo_depón lógica).
        /// </summary>
        [HttpPut("Departamento/{cod_dep}/{cod_emp}/habilitar")]
        public async Task<IActionResult> HabilitarHistorialDepartamento(string cod_emp, string codigo_puesto, string codigo_dep)
        {
            var Historial = await _HistorialDepartamentoRepositorio.HabilitarHistorialDepartamento(cod_emp, codigo_puesto, codigo_dep);

            if (Historial is null)
                return NotFound($"No se encontró un Historial con codigo {cod_emp}.");

            return Ok(Historial);
        }
    }
}
