using RRHH.Core.DTOs;
using RRHH.Core.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RRHH.Core.Mapedores
{
    public static class PuestoMapeador
    {
        public static PuestoDTO toPuestoDTO(this Puesto puesto)
        {
            return new PuestoDTO() 
            {
                Codigo = puesto.Codigo,
                Nombre = puesto.Nombre,
                /*HistorialPuestos = puesto.HistorialPuestos
                .Select(x => x.toHistorialPuestoDTO()) // Usa Select para proyectar los elementos
                .ToList()  // Luego conviértelo en lista*/
            };
        }
    }
}
