namespace RRHH.Core.DTOs
{
    public class DepartamentoDTO
    {
        public string Codigo { get; set; } = null!;
        public string Nombre { get; set; } = null!;

        //public ICollection<HistorialDTO>? HistorialDepartamentos { get; set; }
        //public ICollection<ReporteEmpleadoDTO>? ReportesEmitidos { get; set; }
    }
}
