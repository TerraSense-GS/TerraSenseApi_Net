using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TerraSenseApi.Data;
using TerraSenseApi.Models;

namespace TerraSenseApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ObservacoesController : ControllerBase
{
    private readonly AppDbContext _context;

    public ObservacoesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ObservacaoRelatorio>>> GetObservacoes()
    {
        return await _context.ObservacoesRelatorio.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ObservacaoRelatorio>> GetObservacao(int id)
    {
        var observacao = await _context.ObservacoesRelatorio.FindAsync(id);

        if (observacao == null)
            return NotFound();

        return observacao;
    }

    [HttpPost]
    public async Task<ActionResult<ObservacaoRelatorio>> PostObservacao(ObservacaoRelatorio observacao)
    {
        // Tratamento de erro: quando selecionado um relatório deletado ou não existente, retornar BadRequest.
        var relatorioExiste = await _context.RelatoriosPlantacoes
            .AnyAsync(r => r.IdRelatorio == observacao.RelatorioPlantacaoId);

        if (!relatorioExiste)
        {
            return BadRequest("Relatório informado não existe.");
        }
        
        observacao.DataCriacao = DateTime.Now;

        _context.ObservacoesRelatorio.Add(observacao);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetObservacao), new { id = observacao.IdObservacao }, observacao);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteObservacao(int id)
    {
        var observacao = await _context.ObservacoesRelatorio.FindAsync(id);

        if (observacao == null)
            return NotFound();

        _context.ObservacoesRelatorio.Remove(observacao);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}