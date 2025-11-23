namespace RRHH.Core.DTOs
{
    public class HistorialDTO
    {
        public string Codigo_Emp { get; set; } = null!;
        public string Codigo_Puesto { get; set; } = null!;
        public string Codigo_Dep { get; set; } = null!;
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
    }
}
