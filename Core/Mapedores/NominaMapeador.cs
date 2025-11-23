using RRHH.Core.DTOs;
using RRHH.Core.Models;

namespace RRHH.Core.Mapedores
{
    public static class NominaMapeador
    {
        public static NominaDTO toNominaDTO(this Nomina nomina)
        {
            return new NominaDTO()
            {
                Cod_Nom = nomina.Cod_Nom,
                Cod_Emp = nomina.Cod_Emp,
                PeriodoInicio = nomina.PeriodoInicio,
                PeriodoFin = nomina.PeriodoFin,
                SalarioBase = nomina.SalarioBase,
                Bonos = nomina.Bonos,
                Descuentos = nomina.Descuentos,
                TotalNeto = nomina.TotalNeto
            };
        }
    }
}
