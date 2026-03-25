using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinancialProvision.Provision.Infrastructure.Persistence.Context;
using FinancialProvision.Provision.Domain.Entities;

namespace FinancialProvision.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProvisaoDevolucaoController : ControllerBase
{
    private readonly FinancialProvisionDbContext _context;

    public ProvisaoDevolucaoController(FinancialProvisionDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Lista todas as provisões de devolução
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProvisaoDevolucao>>> GetAll()
    {
        var result = await _context.ProvisoesDevolucao.ToListAsync();

        return Ok(result);
    }

    /// <summary>
    /// Busca provisão de devolução por Id
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<ProvisaoDevolucao>> GetById(int id)
    {
        var entity = await _context.ProvisoesDevolucao.FindAsync(id);

        if (entity == null)
            return NotFound();

        return Ok(entity);
    }

    /// <summary>
    /// Cria nova provisão de devolução
    /// </summary>
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] ProvisaoDevolucao entity)
    {
        _context.ProvisoesDevolucao.Add(entity);

        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
    }

    /// <summary>
    /// Atualiza provisão de devolução
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] ProvisaoDevolucao request)
    {
        var entity = await _context.ProvisoesDevolucao.FindAsync(id);

        if (entity == null)
            return NotFound();

        entity.AtualizarValor(request.ValorPrevisto, request.Descricao);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    /// <summary>
    /// Remove provisão de devolução
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var entity = await _context.ProvisoesDevolucao.FindAsync(id);

        if (entity == null)
            return NotFound();

        _context.ProvisoesDevolucao.Remove(entity);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}