namespace FinancialProvision.Provision.Application.DTOs
{
    public class ProvisaoDevolucaoDto
    {
        public int Id { get; set; }

        public int Mes { get; set; }

        public int Ano { get; set; }

        public decimal ValorPrevisto { get; set; }

        public decimal ValorUtilizado { get; set; }

        public decimal SaldoDisponivel { get; set; }

        public string Descricao { get; set; }
    }
}
