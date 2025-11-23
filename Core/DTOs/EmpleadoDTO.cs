namespace RRHH.Core.DTOs
{
    public class EmpleadoDTO
    {
        public string Codigo { get; set; } = null!;
        public DateTime FechaIngreso { get; set; }
        public PersonaDTO? Persona { get; set; } = null!;
        //public ICollection<HistorialPuestoDTO>? HistorialPuestos { get; set; }
        //public ICollection<HistorialDTO>? HistorialDepartamentos { get; set; }
        //public ICollection<NominaDTO>? Nominas { get; set; }
        //public ICollection<ReporteEmpleadoDTO>? ReportesRecibidos { get; set; }
    }
}
