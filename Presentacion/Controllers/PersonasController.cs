using Microsoft.AspNetCore.Mvc;
using RRHH.Core.Interfaces;

namespace RRHH.Presentacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        private readonly IPersonaRepositorio _personaRepositorio;

        public PersonasController(IPersonaRepositorio personaRepositorio)
        {
            _personaRepositorio = personaRepositorio;
        }

        /// <summary>
        /// Obtiene la lista de personas activos.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetPersona()
        {
            var personas = await _personaRepositorio.GetPersona();
            return Ok(personas);
        }
        /// <summary>
        /// Obtiene un persona por su CI.
        /// </summary>
        [HttpGet("{ci}")]
        public async Task<IActionResult> GetPersona(string ci)
        {
            var persona = await _personaRepositorio.GetPersona(ci);
            if (persona is null)
                return NotFound($"No se encontró un persona con CI {ci}.");

            return Ok(persona);
        }

        /// <summary>
        /// Obtiene la lista de personas marcados como borrados.
        /// </summary>
        [HttpGet("borrados")]
        public async Task<IActionResult> GetPersonasBorrados()
        {
            var personas = await _personaRepositorio.GetPersonaBorrados();
            return Ok(personas);
        }

        /// <summary>
        /// Crea un nuevo persona.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> PostPersona(string ci, string nombre, string? apellidoPaterno, string? apellidoMaterno, string sexo, DateTime fechanacimiento)
        {
            if (string.IsNullOrWhiteSpace(ci) || string.IsNullOrWhiteSpace(nombre))
                return BadRequest("Los campos CI y Nombre son obligatorios.");

            var personaCreado = await _personaRepositorio.PostPersona(ci, nombre, apellidoPaterno, apellidoMaterno, sexo, fechanacimiento);

            return CreatedAtAction(nameof(GetPersona), new { ci = personaCreado.CI }, personaCreado);
        }

        /// <summary>
        /// Actualiza un persona existente.
        /// </summary>
        [HttpPut("{ci}")]
        public async Task<IActionResult> PutPersona(string ci, string ciNuevo, string nombre, string? apellidoPaterno, string? apellidoMaterno)
        {
            if (string.IsNullOrWhiteSpace(ci) || string.IsNullOrWhiteSpace(ciNuevo) || string.IsNullOrWhiteSpace(nombre))
                return BadRequest("Debe llenar todos los campos del persona.");

            var persona = await _personaRepositorio.PutPersona(ci, ciNuevo, nombre, apellidoPaterno, apellidoMaterno);
            if (persona is null)
                return NotFound($"No se encontró el persona con CI {ci}.");

            return Ok(persona);
        }

        /// <summary>
        /// Marca un persona como borrado (eliminación lógica).
        /// </summary>
        [HttpDelete("{ci}")]
        public async Task<IActionResult> DeletePersona(string ci)
        {
            var personaEliminado = await _personaRepositorio.DeletePersona(ci);
            if (personaEliminado is null)
                return NotFound($"No se encontró un persona con CI {ci}.");

            return Ok(personaEliminado);
        }
        /// <summary>
        /// Habilita un persona previamente borrado (reactivación lógica).
        /// </summary>
        [HttpPut("{ci}/habilitar")]
        public async Task<IActionResult> HabilitarPersona(string ci)
        {
            var persona = await _personaRepositorio.HabilitarPersona(ci);

            if (persona is null)
                return NotFound($"No se encontró un persona con CI {ci}.");

            return Ok(persona);
        }
    }
}
