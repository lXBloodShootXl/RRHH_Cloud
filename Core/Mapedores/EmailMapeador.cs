using RRHH.Core.DTOs;
using RRHH.Core.Models;

namespace RRHH.Core.Mapedores
{
    public static class EmailMapeador
    {
        public static EmailDTO toEmailDTO(this Email email)
        {
            return new EmailDTO() 
            {
                Correo = email.Correo,
                /*PersonaEmails = email.PersonaEmails
                    .Select(x => x.toPersonaEmailDTO()) // Usa Select para proyectar los elementos
                    .ToList()  // Luego conviértelo en lista*/
            };
        }
    }
}
