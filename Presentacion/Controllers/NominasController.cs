using Microsoft.AspNetCore.Mvc;
using RRHH.Core.Interfaces;

namespace RRHH.Presentacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NominasController : ControllerBase
    {
        private readonly INominaRepositorio _nominaRepositorio;

        public NominasController(INominaRepositorio nominaRepositorio)
        {
            _nominaRepositorio = nominaRepositorio;
        }

        /// <summary>
        /// Obtiene la lista de nominas activos.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetNomina()
        {
            var nominas = await _nominaRepositorio.GetNomina();
            return Ok(nominas);
        }
        /// <summary>
        /// Obtiene un nomina por su cod_nom.
        /// </summary>
        [HttpGet("{Cod_Nom}")]
        public async Task<IActionResult> GetNomina(string Cod_Nom)
        {
            var nomina = await _nominaRepositorio.GetNomina(Cod_Nom);
            if (nomina is null)
                return NotFound($"No se encontró un nomina con codigo {Cod_Nom}.");

            return Ok(nomina);
        }

        /// <summary>
        /// Obtiene la lista de nominas marcados como borrados.
        /// </summary>
        [HttpGet("borrados")]
        public async Task<IActionResult> GetNominasBorrados()
        {
            var nominas = await _nominaRepositorio.GetNominaBorrados();
            return Ok(nominas);
        }

        /// <summary>
        /// Crea un nuevo nomina.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> PostNomina(string cod_nom, string cod_emp, DateTime periodoInicio, DateTime periodoFin, decimal salario, decimal bonos, decimal descuentos, decimal total)
        {
            if (string.IsNullOrWhiteSpace(cod_nom) || string.IsNullOrWhiteSpace(cod_emp) || decimal.IsNegative(salario) || decimal.IsNegative(bonos) || decimal.IsNegative(descuentos) || decimal.IsNegative(total))
                return BadRequest("Los campos CI y Nombre son obligatorios.");

            var nominaCreado = await _nominaRepositorio.PostNomina(cod_nom, cod_emp, periodoInicio, periodoFin, salario, bonos, descuentos, total);

            return CreatedAtAction(nameof(GetNomina), new { cod_nom = nominaCreado.Cod_Nom }, nominaCreado);
        }

        /// <summary>
        /// Actualiza un nomina existente.
        /// </summary>
        [HttpPut("{cod_nom}")]
        public async Task<IActionResult> PutNomina(string cod_nom, string cod_emp, decimal salario, decimal bonos, decimal descuentos, decimal total)
        {
            if (string.IsNullOrWhiteSpace(cod_nom) || string.IsNullOrWhiteSpace(cod_emp) || decimal.IsNegative(salario) || decimal.IsNegative(bonos) || decimal.IsNegative(descuentos) || decimal.IsNegative(total))
                return BadRequest("Debe llenar todos los campos del nomina y no colocar negativos.");

            var nomina = await _nominaRepositorio.PutNomina(cod_nom, cod_emp, salario, bonos, descuentos, total);
            if (nomina is null)
                return NotFound($"No se encontró la nomina con código {cod_nom}.");

            return Ok(nomina);
        }

        /// <summary>
        /// Marca un nomina como borrado (eliminación lógica).
        /// </summary>
        [HttpDelete("{Cod_Nom}")]
        public async Task<IActionResult> DeleteNomina(string Cod_Nom)
        {
            var nominaEliminado = await _nominaRepositorio.DeleteNomina(Cod_Nom);
            if (nominaEliminado is null)
                return NotFound($"No se encontró una nomina con código {Cod_Nom}.");

            return Ok(nominaEliminado);
        }
        /// <summary>
        /// Habilita un nomina previamente borrado (reactivación lógica).
        /// </summary>
        [HttpPut("{Cod_Nom}/habilitar")]
        public async Task<IActionResult> HabilitarNomina(string Cod_Nom)
        {
            var nomina = await _nominaRepositorio.HabilitarNomina(Cod_Nom);

            if (nomina is null)
                return NotFound($"No se encontró una nomina con codigo {Cod_Nom}.");

            return Ok(nomina);
        }
    }
}
