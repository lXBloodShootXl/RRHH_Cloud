using RRHH.Core.DTOs;


namespace RRHH.Core.Interfaces
{
    public interface IEmpleadoCurriculumRepositorio
    {
        Task<EmpleadoCurriculumDTO> GetEmpleadoCurriculum(string cod_emp);
        Task<List<EmpleadoCurriculumDTO>> GetEmpleadoCurriculum();
        Task<List<EmpleadoCurriculumDTO>> GetEmpleadoCurriculumBorrados();
        Task<EmpleadoCurriculumDTO> PostEmpleadoCurriculum(string cod_emp, string perfil, DateTime fecharecepcion);
        Task<EmpleadoCurriculumDTO> PutEmpleadoCurriculum(string cod_emp, string perfil, DateTime fecharecepcion);
        Task<EmpleadoCurriculumDTO> DeleteEmpleadoCurriculum(string cod_emp);
        Task<EmpleadoCurriculumDTO?> HabilitarEmpleadoCurriculum(string cod_empci);
    }
}
