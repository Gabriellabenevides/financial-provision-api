namespace FinancialProvision.Provision.Domain.Entities
{
    public class ProvisaoDevolucao
    {
        public int Id { get; private set; }

        public int Mes { get; private set; }

        public int Ano { get; private set; }

        public decimal ValorPrevisto { get; private set; }

        public decimal ValorUtilizado { get; private set; }

        public string? Descricao { get; private set; }

        public DateTime DataCriacao { get; private set; }
        private ProvisaoDevolucao() { }

        public ProvisaoDevolucao(int mes, int ano, decimal valorPrevisto, string? descricao)
        {
            Mes = mes;
            Ano = ano;
            ValorPrevisto = valorPrevisto;
            ValorUtilizado = 0;
            Descricao = descricao;
            DataCriacao = DateTime.UtcNow;
        }

        public void RegistrarUtilizacao(decimal valor)
        {
            ValorUtilizado += valor;
        }

        public void AtualizarValor(decimal valorPrevisto, string? descricao)
        {
            ValorPrevisto = valorPrevisto;
            Descricao = descricao;
        }
    }
}