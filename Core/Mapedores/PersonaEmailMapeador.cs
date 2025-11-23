using RRHH.Core.DTOs;
using RRHH.Core.Models;
using System.Security.Principal;

namespace RRHH.Core.Mapedores
{
    public static class PersonaEmailMapeador
    {
        public static PersonaEmailDTO toPersonaEmailDTO(this PersonaEmail email)
        {
            return new PersonaEmailDTO()
            {
                FechaInicio = email.FechaInicio,
                FechaFin = email.FechaFin,
                EsPrincipal = email.EsPrincipal,
                Persona = email.Persona.toPersonaDTO(),
                Email = email.Email.toEmailDTO()
            };
        }
    }
}
