using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocadoraVeiculosAPI.Models
{
    // Todo aluguel está atrelado a um cliente e um veículo, em um dado período de tempo.
    public class Aluguel
    {
        [Key]
        public int Id { get; set; }

        // Chave estrangeira: cliente responsável pelo aluguel.
        [Required]
        [ForeignKey(nameof(Cliente))]
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; } = null!;

        // Chave estrangeira: veículo alugado.
        [Required]
        [ForeignKey(nameof(Veiculo))]
        public int VeiculoId { get; set; }
        public Veiculo Veiculo { get; set; } = null!;

        [Required]
        public DateTime DataInicio { get; set; }

        [Required]
        public DateTime DataFimPrevista { get; set; }

        [Required]
        public int QuilometragemInicial { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal ValorDiaria { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal ValorTotal { get; set; }

        // Um aluguel possui no máximo um registro de devolução (relação 1:1).
        public Devolucao? Devolucao { get; set; }
    }
}
