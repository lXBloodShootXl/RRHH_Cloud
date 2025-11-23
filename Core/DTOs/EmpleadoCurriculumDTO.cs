using System.ComponentModel.DataAnnotations.Schema;

namespace RRHH.Core.DTOs
{
    public class EmpleadoCurriculumDTO
    {
        public string Cod_Emp { get; set; } = null!;
        public string Perfil { get; set; } = null!;
        public DateTime FechaRecepcion { get; set; } = DateTime.Now;
    }
}
