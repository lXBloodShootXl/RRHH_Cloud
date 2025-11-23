using RRHH.Core.DTOs;
using RRHH.Core.Models;
namespace RRHH.Core.Mapedores
{
    public static class EmpleadoMapeador
    {
        public static EmpleadoDTO toEmpleadoDTO(this Empleado empleado)
        {
            return new EmpleadoDTO()
            {
                Codigo = empleado.Codigo,
                FechaIngreso = empleado.FechaIngreso,
                Persona = empleado.Persona.toPersonaDTO(),
                /*HistorialPuestos = empleado.HistorialPuestos
                    .Select(x => x.toHistorialPuestoDTO()) // Usa Select para proyectar los elementos
                    .ToList(), // Luego conviértelo en lista
                HistorialDepartamentos = empleado.HistorialDepartamentos
                .Select(x => x.toHistorialDepartamentoDTO()) // Usa Select para proyectar los elementos
                    .ToList(), // Luego conviértelo en lista
                Nominas = empleado.Nominas
                .Select(x => x.toNominaDTO()) // Usa Select para proyectar los elementos
                    .ToList(), // Luego conviértelo en lista
                //ReportesRecibidos = empleado.ReportesRecibidos*/
            };
        }
    }
}
