using System.ComponentModel.DataAnnotations;

namespace FinancialProvision.Provision.Application.DTOs;

public class CreateMovimentacaoDto
{
    public int ProvisaoDevolucaoId { get; set; }

    public decimal Valor { get; set; }

    [Required]
    public required string Descricao { get; set; }
}
