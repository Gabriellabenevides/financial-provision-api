namespace FinancialProvision.Provision.Application.DTOs;

public class CreateProvisaoDevolucaoDto
{
    public int Mes { get; set; }

    public int Ano { get; set; }

    public decimal ValorPrevisto { get; set; }

    public string? Descricao { get; set; }
}