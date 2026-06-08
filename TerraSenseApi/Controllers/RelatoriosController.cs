using Microsoft.AspNetCore.Http.HttpResults;
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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RelatorioPlantacao>>> GetRelatorios()
    {
        return await _context.RelatoriosPlantacoes
            .Include(r => r.Observacoes)
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RelatorioPlantacao>> GetRelatorio(int id)
    {
        var relatorio = await _context.RelatoriosPlantacoes
            .Include(r => r.Observacoes)
            .FirstOrDefaultAsync(r => r.IdRelatorio == id);

        if (relatorio == null)
            return NotFound();

        return relatorio;
    }

    [HttpGet("plantacao/{idPlantacao}")]
    public async Task<ActionResult<IEnumerable<RelatorioPlantacao>>> GetRelatoriosPorPlantacao(int idPlantacao)
    {
        return await _context.RelatoriosPlantacoes
            .Where(r => r.IdPlantacao == idPlantacao)
            .Include(r => r.Observacoes)
            .ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<RelatorioPlantacao>> PostRelatorio(RelatorioPlantacao relatorio)
    {
        relatorio.DataRelatorio = DateTime.Now;

        _context.RelatoriosPlantacoes.Add(relatorio);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetRelatorio), new { id = relatorio.IdRelatorio }, relatorio);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutRelatorio(int id, RelatorioPlantacao relatorio)
    {
        if (id != relatorio.IdRelatorio)
            return BadRequest();

        _context.Entry(relatorio).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
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