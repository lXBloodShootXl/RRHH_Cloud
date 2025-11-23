using RRHH.Core.DTOs;
using RRHH.Core.Models;
namespace RRHH.Core.Mapedores
{
    public static class EmpleadoCurriculumMapeador
    {
        public static EmpleadoCurriculumDTO toEmpleadoCurriculumDTO(this EmpleadoCurriculum empleadocurriculum)
        {
            return new EmpleadoCurriculumDTO()
            {
                Cod_Emp = empleadocurriculum.Cod_Emp,
                Perfil = empleadocurriculum.Perfil,
                FechaRecepcion = empleadocurriculum.FechaRecepcion,
            };
        }
    }
}
