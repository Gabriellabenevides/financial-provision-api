using FinancialProvision.Provision.Application.DTOs;
using FinancialProvision.Provision.Application.Interfaces.Services;
using FinancialProvision.Provision.Domain.Entities;
using FinancialProvision.Provision.Domain.Interfaces.Repositories;

namespace FinancialProvision.Provision.Application.Services
{
    public class ProvisaoDevolucaoService : IProvisaoDevolucaoService
    {
        private readonly IProvisaoDevolucaoRepository _repository;

        public ProvisaoDevolucaoService(IProvisaoDevolucaoRepository repository)
        {
            _repository = repository;
        }

        public async Task<ProvisaoDevolucaoDto> CriarAsync(ProvisaoDevolucaoDto dto)
        {
            var provisao = new ProvisaoDevolucao(
                dto.Mes,
                dto.Ano,
                dto.ValorPrevisto,
                dto.Descricao
            );
            await _repository.AddAsync(provisao);

            return MapToDto(provisao);
        }

        public async Task AtualizarAsync(ProvisaoDevolucaoDto dto)
        {
            var provisao = await _repository.GetByIdAsync(dto.Id);

            if (provisao == null)
                throw new Exception("Provisão não encontrada");

            provisao.AtualizarValor(dto.ValorPrevisto, dto.Descricao);

            await _repository.UpdateAsync(provisao);
        }

        public async Task<ProvisaoDevolucaoDto?> ObterPorIdAsync(int id)
        {
            var provisao = await _repository.GetByIdAsync(id);

            if (provisao == null)
                return null;
            return MapToDto(provisao);
        }

        public async Task<ProvisaoDevolucaoDto?> ObterPorMesAnoAsync(int mes, int ano)
        {
            var provisao = await _repository.GetByMesAnoAsync(mes, ano);

            if (provisao == null)
                return null;

            return MapToDto(provisao);
        }

        public async Task<IEnumerable<ProvisaoDevolucaoDto>> ObterTodosAsync()
        {
            var lista = await _repository.GetAllAsync();

            return lista.Select(MapToDto);
        }
        private ProvisaoDevolucaoDto MapToDto(ProvisaoDevolucao provisao)
        {
            return new ProvisaoDevolucaoDto
            {
                Id = provisao.Id,
                Mes = provisao.Mes,
                Ano = provisao.Ano,
                ValorPrevisto = provisao.ValorPrevisto,
                ValorUtilizado = provisao.ValorUtilizado,
                SaldoDisponivel = provisao.ValorPrevisto - provisao.ValorUtilizado,
                Descricao = provisao.Descricao
            };
        }
    }

}
