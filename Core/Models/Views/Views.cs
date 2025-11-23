namespace RRHH.Core.Models.Views
{
    public class EmpleadosActivosView
    {
        public string CodigoEmpleado { get; set; }
        public string CI { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Sexo { get; set; }
        public DateTime FechaIngreso { get; set; }
    }

    public class HistorialDepartamentosView
    {
        public int EmpleadoId { get; set; }
        public string CodigoEmpleado { get; set; }
        public string CodigoDepartamento { get; set; }
        public string NombreDepartamento { get; set; }
        public string CodigoPuesto { get; set; }
        public string NombrePuesto { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string Estado { get; set; }
    }

    public class ResumenNominaEmpleadoView
    {
        public int NominaId { get; set; }
        public string CodigoEmpleado { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime PeriodoInicio { get; set; }
        public DateTime PeriodoFin { get; set; }
        public decimal SalarioBase { get; set; }
        public decimal Bonos { get; set; }
        public decimal Descuentos { get; set; }
        public decimal TotalNeto { get; set; }
        public string EstadoNomina { get; set; }
    }

    public class ReportesEmpleadosView
    {
        public int ReporteId { get; set; }
        public string CodigoEmpleadoReportado { get; set; }
        public string CodigoDepartamentoEmisor { get; set; }
        public DateTime Fecha { get; set; }
        public string Tipo { get; set; }
        public string Descripcion { get; set; }
        public string EstadoReporte { get; set; }
    }

    public class EmpleadosSalariosPuestosView
    {
        public int EmpleadoId { get; set; }
        public string CodigoEmpleado { get; set; }
        public string NombreEmpleado { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public decimal SalarioBase { get; set; }
        public string NombrePuesto { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string EstadoEmpleado { get; set; }
    }

}

