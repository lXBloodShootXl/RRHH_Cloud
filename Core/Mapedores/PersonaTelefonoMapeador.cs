using RRHH.Core.DTOs;
using RRHH.Core.Models;
using System.Security.Principal;

namespace RRHH.Core.Mapedores
{
    public static class PersonaTelefonoMapeador
    {
        public static PersonaTelefonoDTO toPersonaTelefonoDTO(this PersonaTelefono personatelefono)
        {
            return new PersonaTelefonoDTO()
            {
                FechaInicio = personatelefono.FechaInicio,
                FechaFin = personatelefono.FechaFin,
                EsPrincipal = personatelefono.EsPrincipal,
                Persona = personatelefono.Persona.toPersonaDTO(),
                Telefono = personatelefono.Telefono.toTelefonoDTO()
            };
        }
    }
}
