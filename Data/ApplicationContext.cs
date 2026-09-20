using LocadoraVeiculosAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LocadoraVeiculosAPI.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<Fabricante> Fabricantes { get; set; } = null!;
        public DbSet<Veiculo> Veiculos { get; set; } = null!;
        public DbSet<Cliente> Clientes { get; set; } = null!;
        public DbSet<Aluguel> Alugueis { get; set; } = null!;
        public DbSet<Devolucao> Devolucoes { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Fabricante (1) -> Veiculo (N)
            modelBuilder.Entity<Fabricante>()
                .HasKey(f => f.Id);

            modelBuilder.Entity<Veiculo>()
                .HasKey(v => v.Id);

            modelBuilder.Entity<Veiculo>()
                .HasIndex(v => v.Placa)
                .IsUnique();

            modelBuilder.Entity<Veiculo>()
                .HasOne(v => v.Fabricante)
                .WithMany(f => f.Veiculos)
                .HasForeignKey(v => v.FabricanteId)
                .OnDelete(DeleteBehavior.Restrict);

            // Cliente
            modelBuilder.Entity<Cliente>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Cliente>()
                .HasIndex(c => c.Cpf)
                .IsUnique();

            // Aluguel: chaves estrangeiras para Cliente e Veiculo.
            // DeleteBehavior.Restrict evita múltiplos caminhos de cascade delete no SQL Server.
            modelBuilder.Entity<Aluguel>()
                .HasKey(a => a.Id);

            modelBuilder.Entity<Aluguel>()
                .HasOne(a => a.Cliente)
                .WithMany(c => c.Alugueis)
                .HasForeignKey(a => a.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Aluguel>()
                .HasOne(a => a.Veiculo)
                .WithMany(v => v.Alugueis)
                .HasForeignKey(a => a.VeiculoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Devolucao: relação 1:1 com Aluguel.
            modelBuilder.Entity<Devolucao>()
                .HasKey(d => d.Id);

            modelBuilder.Entity<Devolucao>()
                .HasIndex(d => d.AluguelId)
                .IsUnique();

            modelBuilder.Entity<Devolucao>()
                .HasOne(d => d.Aluguel)
                .WithOne(a => a.Devolucao)
                .HasForeignKey<Devolucao>(d => d.AluguelId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
