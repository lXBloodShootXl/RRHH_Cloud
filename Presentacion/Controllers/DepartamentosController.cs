using Microsoft.AspNetCore.Mvc;
using RRHH.Core.Interfaces;

namespace RRHH.Presentacion.Controllers
{
    [Route("api/Departamentos[controller]")]
    [ApiController]
    public class DepartamentosController : ControllerBase
    {
        private readonly IDepartamentoRepositorio _departamentoRepositorio;

        public DepartamentosController(IDepartamentoRepositorio departamentoRepositorio)
        {
            _departamentoRepositorio = departamentoRepositorio;
        }

        /// <summary>
        /// Obtiene la lista de departamentos activos.
        /// </summary>
        [HttpGet("/GET")]
        public async Task<IActionResult> GetDepartamento()
        {
            var departamentos = await _departamentoRepositorio.GetDepartamento();
            return Ok(departamentos);
        }
        /// <summary>
        /// Obtiene un departamento por su CI.
        /// </summary>
        [HttpGet("/GET/{codigo}")]
        public async Task<IActionResult> GetDepartamento(string codigo)
        {
            var departamento = await _departamentoRepositorio.GetDepartamento(codigo);
            if (departamento is null)
                return NotFound($"No se encontró un departamento con CI {codigo}.");

            return Ok(departamento);
        }

        /// <summary>
        /// Obtiene la lista de departamentos marcados como borrados.
        /// </summary>
        [HttpGet("/GET/borrados")]
        public async Task<IActionResult> GetDepartamentosBorrados()
        {
            var departamentos = await _departamentoRepositorio.GetDepartamentoBorrados();
            return Ok(departamentos);
        }

        /// <summary>
        /// Crea un nuevo departamento.
        /// </summary>
        [HttpPost("/POST")]
        public async Task<IActionResult> PostDepartamento(string codigo, string nombre)
        {
            if (string.IsNullOrWhiteSpace(codigo) || string.IsNullOrWhiteSpace(nombre))
                return BadRequest("Los campos código y Nombre son obligatorios.");

            var departamentoCreado = await _departamentoRepositorio.PostDepartamento(codigo,nombre); ;

            return CreatedAtAction(nameof(GetDepartamento), new {codigo= departamentoCreado.Codigo }, departamentoCreado);
        }

        /// <summary>
        /// Actualiza un departamento existente.
        /// </summary>
        [HttpPut("/PUT/{codigo}")]
        public async Task<IActionResult> PutDepartamento(string codigo, string nombreNuevo, string codigoNuevo)
        {
            if (string.IsNullOrWhiteSpace(codigo) || string.IsNullOrWhiteSpace(nombreNuevo) || string.IsNullOrWhiteSpace(codigoNuevo))
                return BadRequest("Debe llenar todos los campos del departamento.");

            var departamento = await _departamentoRepositorio.PutDepartamento(codigo, nombreNuevo, codigoNuevo);
            if (departamento is null)
                return NotFound($"No se encontró el departamento con el código {codigo}.");

            return Ok(departamento);
        }

        /// <summary>
        /// Marca un departamento como borrado (eliminación lógica).
        /// </summary>
        [HttpDelete("/DELETE/{codigo}")]
        public async Task<IActionResult> DeleteDepartamento(string codigo)
        {
            var departamentoEliminado = await _departamentoRepositorio.DeleteDepartamento(codigo);
            if (departamentoEliminado is null)
                return NotFound($"No se encontró un departamento con el código {codigo}.");

            return Ok(departamentoEliminado);
        }
        /// <summary>
        /// Habilita un departamento previamente borrado (reactivación lógica).
        /// </summary>
        [HttpPut("/HABILITAR/{codigo}")]
        public async Task<IActionResult> HabilitarDepartamento(string codigo)
        {
            var departamento = await _departamentoRepositorio.HabilitarDepartamento(codigo);

            if (departamento is null)
                return NotFound($"No se encontró un departamento con el código {codigo}.");

            return Ok(departamento);
        }
    }
}
