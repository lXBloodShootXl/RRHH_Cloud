using Microsoft.AspNetCore.Mvc;
using RRHH.Core.Interfaces;

namespace RRHH.PresentaCod_Empon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoCurriculumsController : ControllerBase
    {
        private readonly IEmpleadoCurriculumRepositorio _EmpleadoCurriculumRepositorio;

        public EmpleadoCurriculumsController(IEmpleadoCurriculumRepositorio EmpleadoCurriculumRepositorio)
        {
            _EmpleadoCurriculumRepositorio = EmpleadoCurriculumRepositorio;
        }

        /// <summary>
        /// Obtiene la lista de EmpleadoCurriculums activos.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetEmpleadoCurriculum()
        {
            var EmpleadoCurriculums = await _EmpleadoCurriculumRepositorio.GetEmpleadoCurriculum();
            return Ok(EmpleadoCurriculums);
        }
        /// <summary>
        /// Obtiene un EmpleadoCurriculum por su Cod_Emp.
        /// </summary>
        [HttpGet("{Cod_Emp}")]
        public async Task<IActionResult> GetEmpleadoCurriculum(string Cod_Emp)
        {
            var EmpleadoCurriculum = await _EmpleadoCurriculumRepositorio.GetEmpleadoCurriculum(Cod_Emp);
            if (EmpleadoCurriculum is null)
                return NotFound($"No se encontró un EmpleadoCurriculum con Cod_Emp {Cod_Emp}.");

            return Ok(EmpleadoCurriculum);
        }

        /// <summary>
        /// Obtiene la lista de EmpleadoCurriculums marcados como borrados.
        /// </summary>
        [HttpGet("borrados")]
        public async Task<IActionResult> GetEmpleadoCurriculumsBorrados()
        {
            var EmpleadoCurriculums = await _EmpleadoCurriculumRepositorio.GetEmpleadoCurriculumBorrados();
            return Ok(EmpleadoCurriculums);
        }

        /// <summary>
        /// Crea un nuevo EmpleadoCurriculum.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> PostEmpleadoCurriculum(string cod_emp, string perfil, DateTime fecharecepcion)
        {
            if (string.IsNullOrWhiteSpace(cod_emp) || string.IsNullOrWhiteSpace(perfil))
                return BadRequest("El campo Cod_Emp es obligatorio.");

            var EmpleadoCurriculumCreado = await _EmpleadoCurriculumRepositorio.PostEmpleadoCurriculum(cod_emp, perfil, fecharecepcion);

            return CreatedAtAction(nameof(GetEmpleadoCurriculum), new { Cod_Emp = EmpleadoCurriculumCreado.Cod_Emp }, EmpleadoCurriculumCreado);
        }

        /// <summary>
        /// Actualiza un EmpleadoCurriculum existente.
        /// </summary>
        [HttpPut("{cod_emp}")]
        public async Task<IActionResult> PutEmpleadoCurriculum(string cod_emp, string perfil, DateTime fecharecepcion)
        {
            if (string.IsNullOrWhiteSpace(cod_emp) || string.IsNullOrWhiteSpace(perfil))
                return BadRequest("Debe llenar todos los campos del EmpleadoCurriculum.");

            var EmpleadoCurriculum = await _EmpleadoCurriculumRepositorio.PutEmpleadoCurriculum(cod_emp, perfil, fecharecepcion);
            if (EmpleadoCurriculum is null)
                return NotFound($"No se encontró el EmpleadoCurriculum con Cod_Emp {cod_emp}.");

            return Ok(EmpleadoCurriculum);
        }

        /// <summary>
        /// Marca un EmpleadoCurriculum como borrado (eliminaCod_Empón lógica).
        /// </summary>
        [HttpDelete("{Cod_Emp}")]
        public async Task<IActionResult> DeleteEmpleadoCurriculum(string Cod_Emp)
        {
            var EmpleadoCurriculumEliminado = await _EmpleadoCurriculumRepositorio.DeleteEmpleadoCurriculum(Cod_Emp);
            if (EmpleadoCurriculumEliminado is null)
                return NotFound($"No se encontró un EmpleadoCurriculum con Cod_Emp {Cod_Emp}.");

            return Ok(EmpleadoCurriculumEliminado);
        }
        /// <summary>
        /// Habilita un EmpleadoCurriculum previamente borrado (reactivaCod_Empón lógica).
        /// </summary>
        [HttpPut("{Cod_Emp}/habilitar")]
        public async Task<IActionResult> HabilitarEmpleadoCurriculum(string Cod_Emp)
        {
            var EmpleadoCurriculum = await _EmpleadoCurriculumRepositorio.HabilitarEmpleadoCurriculum(Cod_Emp);

            if (EmpleadoCurriculum is null)
                return NotFound($"No se encontró un EmpleadoCurriculum con Cod_Emp {Cod_Emp}.");

            return Ok(EmpleadoCurriculum);
        }
    }
}
