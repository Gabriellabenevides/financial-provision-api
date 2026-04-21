using FinancialProvision.Provision.Application.DTOs;
using FinancialProvision.Provision.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinancialProvision.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProvisaoDevolucaoController : ControllerBase
{
    private readonly IProvisaoDevolucaoService _service;

    public ProvisaoDevolucaoController(IProvisaoDevolucaoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProvisaoDevolucaoDto>>> GetAll()
    {
        var result = await _service.ObterTodosAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProvisaoDevolucaoDto>> GetById(int id)
    {
        var result = await _service.ObterPorIdAsync(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult> Create(CreateProvisaoDevolucaoDto dto)
    {
        var serviceDto = new ProvisaoDevolucaoDto
        {
            Mes = dto.Mes,
            Ano = dto.Ano,
            ValorPrevisto = dto.ValorPrevisto,
            Descricao = dto.Descricao
        };

        var result = await _service.CriarAsync(serviceDto);

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, UpdateProvisaoDevolucaoDto dto)
    {
        var serviceDto = new ProvisaoDevolucaoDto
        {
            Id = id,
            ValorPrevisto = dto.ValorPrevisto,
            Descricao = dto.Descricao
        };

        await _service.AtualizarAsync(serviceDto);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _service.RemoverAsync(id);
        return NoContent();
    }
}