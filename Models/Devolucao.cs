using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocadoraVeiculosAPI.Models
{
    // Quinta entidade: registra a devolução do veículo referente a um aluguel,
    // incluindo a quilometragem final ao término da locação.
    public class Devolucao
    {
        [Key]
        public int Id { get; set; }

        // Chave estrangeira: aluguel ao qual esta devolução pertence (relação 1:1).
        [Required]
        [ForeignKey(nameof(Aluguel))]
        public int AluguelId { get; set; }
        public Aluguel Aluguel { get; set; } = null!;

        [Required]
        public DateTime DataDevolucao { get; set; }

        [Required]
        public int QuilometragemFinal { get; set; }

        [MaxLength(300)]
        public string? Observacoes { get; set; }
    }
}
