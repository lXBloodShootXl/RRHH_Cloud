using RRHH.Core.DTOs;
using RRHH.Core.Models;
using System.Security.Principal;

namespace RRHH.Core.Mapedores
{
    public static class HistorialMapeador
    {
        public static HistorialDTO toHistorialDepartamentoDTO(this Historial historial)
        {
            return new HistorialDTO()
            {
                Codigo_Emp = historial.Codigo_Emp,
                Codigo_Dep = historial.Codigo_Dep,
                Codigo_Puesto = historial.Codigo_Puesto,
                FechaInicio = historial.FechaInicio,
                FechaFin = historial.FechaFin,
                //Empleado = historial.Empleado.toEmpleadoDTO(),
                //Departamento = historial.Departamento.toDepartamentoDTO()
            };
        }
    }
}
