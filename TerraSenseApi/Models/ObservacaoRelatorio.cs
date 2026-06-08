using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TerraSenseApi.Models;

[Table("TB_OBSERVACAO_RELATORIO")]
public class ObservacaoRelatorio
{
    [Key]
    [Column("ID_OBSERVACAO")]
    public int IdObservacao { get; set; }

    [Required]
    [Column("DS_OBSERVACAO")]
    public string Descricao { get; set; } = string.Empty;

    [Column("DT_CRIACAO")]
    public DateTime DataCriacao { get; set; }

    [Column("ID_RELATORIO")]
    public int RelatorioPlantacaoId { get; set; }

    [ForeignKey(nameof(RelatorioPlantacaoId))]
    public RelatorioPlantacao RelatorioPlantacao { get; set; } = null!;
}