using RRHH.Core.DTOs;
using RRHH.Core.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RRHH.Core.Mapedores
{
    public static class DepartamentoMapeador
    {
        public static DepartamentoDTO toDepartamentoDTO(this Departamento departamento)
        {
            return new DepartamentoDTO() 
            {
                Codigo = departamento.Codigo,
                Nombre = departamento.Nombre,
                /*HistorialDepartamentos = departamento.HistorialDepartamentos
                .Select(x => x.toHistorialDepartamentoDTO()) // Usa Select para proyectar los elementos
                    .ToList()  // Luego conviértelo en lista*/
                //ReportesEmitidos = departamento.ReportesEmitidos
            };
        }
    }
}
