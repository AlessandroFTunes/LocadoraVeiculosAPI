using System.ComponentModel.DataAnnotations;

namespace LocadoraVeiculosAPI.Models
{
    // Representa a marca/fabricante do veículo (ex: Fiat, Chevrolet, Toyota).
    public class Fabricante
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(80)]
        public string Nome { get; set; } = string.Empty;

        [MaxLength(60)]
        public string? PaisOrigem { get; set; }

        // Um fabricante possui muitos veículos.
        public ICollection<Veiculo> Veiculos { get; set; } = new List<Veiculo>();
    }
}
