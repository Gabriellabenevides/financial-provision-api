namespace FinancialProvision.Provision.Application.Entities;

public class ProvisaoDevolucao
{
    public int Id { get; private set; }

    public int Mes { get; private set; }

    public int Ano { get; private set; }

    public decimal ValorPrevisto { get; private set; }

    public decimal ValorUtilizado { get; private set; }

    public decimal SaldoDisponivel => ValorPrevisto - ValorUtilizado;

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
        if (valor <= 0)
            throw new ArgumentException("Valor deve ser maior que zero");

        if (ValorUtilizado + valor > ValorPrevisto)
            throw new InvalidOperationException("Valor excede o provisionado");

        ValorUtilizado += valor;
    }

    public void AtualizarValor(decimal valorPrevisto, string? descricao)
    {
        ValorPrevisto = valorPrevisto;
        Descricao = descricao;
    }
}