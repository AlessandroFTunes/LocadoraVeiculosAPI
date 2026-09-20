using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocadoraVeiculosAPI.Models
{
    // Todo veículo pertence a um fabricante e possui modelo, ano de fabricação e quilometragem.
    public class Veiculo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(80)]
        public string Modelo { get; set; } = string.Empty;

        [Required]
        public int AnoFabricacao { get; set; }

        [Required]
        public int Quilometragem { get; set; }

        [Required]
        [MaxLength(8)]
        public string Placa { get; set; } = string.Empty;

        // Chave estrangeira: todo veículo pertence a um fabricante.
        [Required]
        [ForeignKey(nameof(Fabricante))]
        public int FabricanteId { get; set; }
        public Fabricante Fabricante { get; set; } = null!;

        // Um veículo pode ter vários aluguéis ao longo do tempo.
        public ICollection<Aluguel> Alugueis { get; set; } = new List<Aluguel>();
    }
}
