using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TerraSenseApi.Models;

[Table("TB_RELATORIO_PLANTACAO")]
public class RelatorioPlantacao
{
    [Key]
    [Column("ID_RELATORIO")]
    public int IdRelatorio { get; set; }

    [Column("ID_PLANTACAO")]
    public int IdPlantacao { get; set; }
    
    [Required]
    public string NomePlantacao { get; set; } = string.Empty;
    
    [Required]
    public string NomePropriedade { get; set; } = string.Empty;
    
    [Required]
    public string Cidade { get; set; } = string.Empty;

    public decimal Ndvi { get; set; }
    
    [Required]
    public string StatusGeral { get; set; } = string.Empty;
    
    public decimal Temperatura { get; set; }
    
    public decimal Umidade { get; set; }
    
    public decimal Chuva { get; set; }
    
    public decimal RadiacaoSolar { get; set; }

    public DateTime DataRelatorio { get; set; }

    public ICollection<ObservacaoRelatorio> Observacoes { get; set; }
        = new List<ObservacaoRelatorio>();
}