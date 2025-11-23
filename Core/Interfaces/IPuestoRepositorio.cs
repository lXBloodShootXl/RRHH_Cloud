using RRHH.Core.DTOs;

namespace RRHH.Core.Interfaces
{
    public interface IPuestoRepositorio
    {
        Task<PuestoDTO> GetPuesto(string codigo);
        Task<List<PuestoDTO>> GetPuesto();
        Task<List<PuestoDTO>> GetPuestoBorrados();
        Task<PuestoDTO> PostPuesto(string codigo, string nombre);
        Task<PuestoDTO> PutPuesto(string codigo, string nuevoNombre);
        Task<PuestoDTO> DeletePuesto(string codigo);
        Task<PuestoDTO?> HabilitarPuesto(string codigo);
    }
}
