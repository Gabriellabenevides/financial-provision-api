namespace FinancialProvision.Provision.Application.DTOs;

public class CreateMovimentacaoDto
{
    public int ProvisaoDevolucaoId { get; set; }

    public decimal Valor { get; set; }

    public string Descricao { get; set; }
}
