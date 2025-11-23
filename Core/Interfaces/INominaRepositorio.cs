using RRHH.Core.DTOs;

namespace RRHH.Core.Interfaces
{
    public interface INominaRepositorio
    {
        Task<NominaDTO> GetNomina(string cod_nom);
        Task<List<NominaDTO>> GetNomina();
        Task<List<NominaDTO>> GetNominaBorrados();
        Task<NominaDTO> PostNomina(string cod_nom, string cod_emp, DateTime periodoInicio, DateTime peridoFin, decimal salario, decimal bonos, decimal descuentos, decimal total);
        Task<NominaDTO> PutNomina(string cod_nom, string cod_emp, decimal salario, decimal bonos, decimal descuentos, decimal total);
        Task<NominaDTO> DeleteNomina(string cod_nom);
        Task<NominaDTO?> HabilitarNomina(string cod_nom);
    }
}
