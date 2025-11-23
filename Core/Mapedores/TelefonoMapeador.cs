using RRHH.Core.DTOs;
using RRHH.Core.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RRHH.Core.Mapedores
{
    public static class TelefonoMapeador
    {
        public static TelefonoDTO toTelefonoDTO(this Telefono telefono)
        {
            return new TelefonoDTO() 
            {
                Numero = telefono.Numero,
                /*PersonaTelefonos = telefono.PersonaTelefonos
                    .Select(x => x.toPersonaTelefonoDTO()) // Usa Select para proyectar los elementos
                    .ToList()  // Luego conviértelo en lista*/
            };
        }
    }
}
