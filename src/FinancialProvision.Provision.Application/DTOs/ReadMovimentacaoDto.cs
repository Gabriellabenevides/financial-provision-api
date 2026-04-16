namespace FinancialProvision.Provision.Application.DTOs;

public class ReadMovimentacaoDto
{
    public int Id { get; set; }

    public decimal Valor { get; set; }

    public string Descricao { get; set; }

    public DateTime DataCriacao { get; set; }
}
