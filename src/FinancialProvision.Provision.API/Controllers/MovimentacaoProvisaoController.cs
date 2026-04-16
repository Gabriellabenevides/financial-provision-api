using FinancialProvision.Provision.Application.DTOs;
using FinancialProvision.Provision.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinancialProvision.Provision.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MovimentacaoProvisaoController : ControllerBase
{
    private readonly IMovimentacaoProvisaoService _service;

    public MovimentacaoProvisaoController(
        IMovimentacaoProvisaoService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult> Create(
        [FromBody] CreateMovimentacaoDto dto)
    {
        try
        {
            await _service.CreateAsync(dto);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{provisaoId}")]
    public async Task<ActionResult> GetByProvisao(int provisaoId)
    {
        var result = await _service.GetByProvisaoIdAsync(provisaoId);
        return Ok(result);
    }
}
