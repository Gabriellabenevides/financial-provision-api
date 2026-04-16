namespace FinancialProvision.Provision.Domain.Entities;

public class MovimentacaoProvisao
{
    public int Id { get; private set; }

    public int ProvisaoDevolucaoId { get; private set; }

    public decimal Valor { get; private set; }

    public string Descricao { get; private set; }

    public DateTime DataCriacao { get; private set; }

    public ProvisaoDevolucao ProvisaoDevolucao { get; private set; }

    private MovimentacaoProvisao() { }

    public MovimentacaoProvisao(
        int provisaoId,
        decimal valor,
        string descricao)
    {
        ProvisaoDevolucaoId = provisaoId;
        Valor = valor;
        Descricao = descricao;
        DataCriacao = DateTime.UtcNow;
    }
}