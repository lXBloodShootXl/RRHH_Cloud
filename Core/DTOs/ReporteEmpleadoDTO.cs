namespace RRHH.Core.DTOs
{
    public class ReporteEmpleadoDTO
    {
        public string Cod_Emp { get; set; } = null!;
        public string Cod_Dep { get; set; } = null!;
        public DateTime Fecha { get; set; }
        public string? Tipo { get; set; }
        public string? Descripcion { get; set; }

        //public EmpleadoDTO? EmpleadoReportado { get; set; } = null!;
        //public DepartamentoDTO? DepartamentoEmisor { get; set; } = null!;
    }
}
