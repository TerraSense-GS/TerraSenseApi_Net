using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TerraSenseApi.Data;
using TerraSenseApi.Models;

namespace TerraSenseApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RelatoriosController : ControllerBase
{
    private readonly AppDbContext _context;

    public RelatoriosController(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Retorna todos os relatórios
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<RelatorioPlantacao>>> GetRelatorios()
    {
        return await _context.RelatoriosPlantacoes
            .Include(r => r.Observacoes)
            .ToListAsync();
    }

    /// <summary>
    /// Busca um relatório por ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RelatorioPlantacao>> GetRelatorio(int id)
    {
        var relatorio = await _context.RelatoriosPlantacoes
            .Include(r => r.Observacoes)
            .FirstOrDefaultAsync(r => r.IdRelatorio == id);

        if (relatorio == null)
            return NotFound();

        return relatorio;
    }

    /// <summary>
    /// Busca um relatório pelo ID da plantação
    /// </summary>
    [HttpGet("plantacao/{idPlantacao}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<RelatorioPlantacao>>> GetRelatoriosPorPlantacao(int idPlantacao)
    {
        return await _context.RelatoriosPlantacoes
            .Where(r => r.IdPlantacao == idPlantacao)
            .Include(r => r.Observacoes)
            .ToListAsync();
    }

    /// <summary>
    /// Cadastra um novo relatório
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RelatorioPlantacao>> PostRelatorio(RelatorioPlantacao relatorio)
    {
        relatorio.DataRelatorio = DateTime.Now;

        _context.RelatoriosPlantacoes.Add(relatorio);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetRelatorio), new { id = relatorio.IdRelatorio }, relatorio);
    }

    /// <summary>
    /// Atualiza um relatório
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutRelatorio(int id, RelatorioPlantacao relatorio)
    {
        if (id != relatorio.IdRelatorio)
            return BadRequest();

        var relatorioExistente = await _context.RelatoriosPlantacoes.FindAsync(id);

        if (relatorioExistente == null)
            return NotFound();

        relatorioExistente.IdPlantacao = relatorio.IdPlantacao;
        relatorioExistente.NomePlantacao = relatorio.NomePlantacao;
        relatorioExistente.NomePropriedade = relatorio.NomePropriedade;
        relatorioExistente.Cidade = relatorio.Cidade;
        relatorioExistente.Ndvi = relatorio.Ndvi;
        relatorioExistente.StatusGeral = relatorio.StatusGeral;
        relatorioExistente.Temperatura = relatorio.Temperatura;
        relatorioExistente.Umidade = relatorio.Umidade;
        relatorioExistente.Chuva = relatorio.Chuva;
        relatorioExistente.RadiacaoSolar = relatorio.RadiacaoSolar;
        relatorioExistente.DataRelatorio = relatorio.DataRelatorio;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    /// <summary>
    /// Deleta um relatório e suas observações vinculadas
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRelatorio(int id)
    {
        var relatorio = await _context.RelatoriosPlantacoes.FindAsync(id);

        if (relatorio == null)
            return NotFound();

        _context.RelatoriosPlantacoes.Remove(relatorio);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}