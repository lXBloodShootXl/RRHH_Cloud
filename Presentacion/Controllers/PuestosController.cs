using Microsoft.AspNetCore.Mvc;
using RRHH.Core.Interfaces;

namespace RRHH.Presentacodigoon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PuestosController : ControllerBase
    {
        private readonly IPuestoRepositorio _puestoRepositorio;

        public PuestosController(IPuestoRepositorio puestoRepositorio)
        {
            _puestoRepositorio = puestoRepositorio;
        }

        /// <summary>
        /// Obtiene la lista de puestos activos.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetPuesto()
        {
            var puestos = await _puestoRepositorio.GetPuesto();
            return Ok(puestos);
        }
        /// <summary>
        /// Obtiene un puesto por su codigo.
        /// </summary>
        [HttpGet("{codigo}")]
        public async Task<IActionResult> GetPuesto(string codigo)
        {
            var puesto = await _puestoRepositorio.GetPuesto(codigo);
            if (puesto is null)
                return NotFound($"No se encontró un puesto con codigo {codigo}.");

            return Ok(puesto);
        }

        /// <summary>
        /// Obtiene la lista de puestos marcados como borrados.
        /// </summary>
        [HttpGet("borrados")]
        public async Task<IActionResult> GetPuestosBorrados()
        {
            var puestos = await _puestoRepositorio.GetPuestoBorrados();
            return Ok(puestos);
        }

        /// <summary>
        /// Crea un nuevo puesto.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> PostPuesto(string codigo, string nombre)
        {
            if (string.IsNullOrWhiteSpace(codigo) || string.IsNullOrWhiteSpace(nombre))
                return BadRequest("Los campos codigo y Nombre son obligatorios.");

            var puestoCreado = await _puestoRepositorio.PostPuesto(codigo, nombre);
            return CreatedAtAction(nameof(GetPuesto), new { codigo = puestoCreado.Codigo }, puestoCreado);
        }

        /// <summary>
        /// Actualiza un puesto existente.
        /// </summary>
        [HttpPut("{codigo}")]
        public async Task<IActionResult> PutPuesto(string codigo, string nuevoNombre)
        {
            if (string.IsNullOrWhiteSpace(codigo) || string.IsNullOrWhiteSpace(nuevoNombre))
                return BadRequest("Debe llenar todos los campos del puesto.");

            var puesto = await _puestoRepositorio.PutPuesto(codigo, nuevoNombre);
            if (puesto is null)
                return NotFound($"No se encontró el puesto con codigo {codigo}.");

            return Ok(puesto);
        }

        /// <summary>
        /// Marca un puesto como borrado (eliminacodigoón lógica).
        /// </summary>
        [HttpDelete("{codigo}")]
        public async Task<IActionResult> DeletePuesto(string codigo)
        {
            var puestoEliminado = await _puestoRepositorio.DeletePuesto(codigo);
            if (puestoEliminado is null)
                return NotFound($"No se encontró un puesto con codigo {codigo}.");

            return Ok(puestoEliminado);
        }
        /// <summary>
        /// Habilita un puesto previamente borrado (reactivacodigoón lógica).
        /// </summary>
        [HttpPut("{codigo}/habilitar")]
        public async Task<IActionResult> HabilitarPuesto(string codigo)
        {
            var puesto = await _puestoRepositorio.HabilitarPuesto(codigo);

            if (puesto is null)
                return NotFound($"No se encontró un puesto con codigo {codigo}.");

            return Ok(puesto);
        }
    }
}
