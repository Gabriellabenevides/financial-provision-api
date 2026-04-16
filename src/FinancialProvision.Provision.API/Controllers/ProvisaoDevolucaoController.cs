using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinancialProvision.Provision.Infrastructure.Persistence.Context;
using FinancialProvision.Provision.Domain.Entities;
using FinancialProvision.Provision.Application.DTOs;

namespace FinancialProvision.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProvisaoDevolucaoController : ControllerBase
{
    private readonly FinancialProvisionDbContext _context;

    public ProvisaoDevolucaoController(
        FinancialProvisionDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReadProvisaoDevolucaoDto>>> GetAll()
    {
        var result =
            await _context.ProvisoesDevolucao
            .Select(x => new ReadProvisaoDevolucaoDto
            {
                Id = x.Id,
                Mes = x.Mes,
                Ano = x.Ano,
                ValorPrevisto = x.ValorPrevisto,
                ValorUtilizado = x.ValorUtilizado,
                SaldoDisponivel =
                    x.ValorPrevisto - x.ValorUtilizado,
                Descricao = x.Descricao,
                DataCriacao = x.DataCriacao
            })
            .ToListAsync();

        return Ok(result);
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<ReadProvisaoDevolucaoDto>> GetById(int id)
    {
        var entity =
            await _context.ProvisoesDevolucao
            .Where(x => x.Id == id)
            .Select(x => new ReadProvisaoDevolucaoDto
            {
                Id = x.Id,
                Mes = x.Mes,
                Ano = x.Ano,
                ValorPrevisto = x.ValorPrevisto,
                ValorUtilizado = x.ValorUtilizado,
                SaldoDisponivel =
                    x.ValorPrevisto - x.ValorUtilizado,
                Descricao = x.Descricao,
                DataCriacao = x.DataCriacao
            })
            .FirstOrDefaultAsync();

        if (entity == null)
            return NotFound();

        return Ok(entity);
    }


    [HttpPost]
    public async Task<ActionResult> Create(
        CreateProvisaoDevolucaoDto dto)
    {
        var entity =
            new ProvisaoDevolucao(
                dto.Mes,
                dto.Ano,
                dto.ValorPrevisto,
                dto.Descricao);

        _context.ProvisoesDevolucao.Add(entity);

        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetById),
            new { id = entity.Id },
            entity);
    }


    [HttpPut("{id}")]
    public async Task<ActionResult> Update(
        int id,
        UpdateProvisaoDevolucaoDto dto)
    {
        var entity =
            await _context.ProvisoesDevolucao
            .FindAsync(id);

        if (entity == null)
            return NotFound();

        entity.AtualizarValor(
            dto.ValorPrevisto,
            dto.Descricao);

        await _context.SaveChangesAsync();

        return NoContent();
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var entity =
            await _context.ProvisoesDevolucao
            .FindAsync(id);

        if (entity == null)
            return NotFound();

        _context.ProvisoesDevolucao.Remove(entity);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}