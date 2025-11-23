using RRHH.Core.DTOs;
using RRHH.Core.Models;

namespace RRHH.Core.Mapedores
{
    public static class ReporteEmpleadoMapeador
    {
        public static ReporteEmpleadoDTO toReporteEmpleadoDTO(this ReporteEmpleado reporteempleado)
        {
            return new ReporteEmpleadoDTO() 
            {
                Cod_Dep = reporteempleado.Cod_Dep,
                Cod_Emp = reporteempleado.Cod_Emp,
                Fecha = reporteempleado.Fecha,
                Tipo = reporteempleado.Tipo,
                Descripcion = reporteempleado.Descripcion,
                //EmpleadoReportado = reporteempleado.EmpleadoReportado.toEmpleadoDTO(),
                //DepartamentoEmisor = reporteempleado.DepartamentoEmisor.toDepartamentoDTO()
            };
        }
    }
}
