using FinancialProvision.Provision.Application.DTOs;
using FinancialProvision.Provision.Application.Entities;
using FinancialProvision.Provision.Application.Interfaces;
using FinancialProvision.Provision.Application.Interfaces.Repositories;
using FinancialProvision.Provision.Application.Interfaces.Services;

namespace FinancialProvision.Provision.Application.Services;

public class MovimentacaoProvisaoService : IMovimentacaoProvisaoService
{
    private readonly IMovimentacaoProvisaoRepository _movRepository;
    private readonly IProvisaoDevolucaoRepository _provRepository;

    public MovimentacaoProvisaoService(IMovimentacaoProvisaoRepository movRepository, IProvisaoDevolucaoRepository provRepository)
    {
        _movRepository = movRepository;
        _provRepository = provRepository;
    }

    public async Task CreateAsync(CreateMovimentacaoDto dto)
    {
        var provisao =
            await _provRepository.GetByIdAsync(dto.ProvisaoDevolucaoId);

        if (provisao == null)
            throw new Exception("Provisão não encontrada");

        provisao.RegistrarUtilizacao(dto.Valor);

        var movimentacao =
            new MovimentacaoProvisao(
                dto.ProvisaoDevolucaoId,
                dto.Valor,
                dto.Descricao);

        await _movRepository.AddAsync(movimentacao);

        await _provRepository.UpdateAsync(provisao);
    }

    public async Task<IEnumerable<ReadMovimentacaoDto>> GetByProvisaoIdAsync(int provisaoId)
    {
        var list =
            await _movRepository.GetByProvisaoIdAsync(provisaoId);

        return list.Select(x => new ReadMovimentacaoDto
        {
            Id = x.Id,
            Valor = x.Valor,
            Descricao = x.Descricao,
            DataCriacao = x.DataCriacao
        });
    }
}
