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

    /// <summary>
    /// Retorna todas as observações
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ObservacaoRelatorio>>> GetObservacoes()
    {
        return await _context.ObservacoesRelatorio.ToListAsync();
    }

    /// <summary>
    /// Retorna a observação com o ID compatível
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ObservacaoRelatorio>> GetObservacao(int id)
    {
        var observacao = await _context.ObservacoesRelatorio.FindAsync(id);

        if (observacao == null)
            return NotFound();

        return observacao;
    }

    /// <summary>
    /// Cadastra uma nova observação vinculada a um relatório existente
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ObservacaoRelatorio>> PostObservacao(ObservacaoRelatorio observacao)
    {
        // Tratamento de erro: quando selecionado um relatório deletado ou não existente, retornar BadRequest.
        var relatorio = await _context.RelatoriosPlantacoes
            .FindAsync(observacao.RelatorioPlantacaoId);

        if (relatorio == null)
        {
            return BadRequest("Relatório informado não existe.");
        }
        
        observacao.DataCriacao = DateTime.Now;

        _context.ObservacoesRelatorio.Add(observacao);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetObservacao), new { id = observacao.IdObservacao }, observacao);
    }

    /// <summary>
    /// Remove uma observação específica
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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