namespace RRHH.Core.DTOs
{
    public class NominaDTO
    {
        public string Cod_Nom { get; set; } = null!;
        public string Cod_Emp { get; set; } = null!;
        public DateTime PeriodoInicio { get; set; }
        public DateTime PeriodoFin { get; set; }
        public decimal SalarioBase { get; set; }
        public decimal Bonos { get; set; }
        public decimal Descuentos { get; set; }
        public decimal TotalNeto { get; set; }
    }
}
