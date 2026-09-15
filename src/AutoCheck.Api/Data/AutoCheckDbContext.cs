using Microsoft.EntityFrameworkCore;
using AutoCheck.Domain;

namespace AutoCheck.Api.Data
{
    public class AutoCheckDbContext : DbContext
    {
        public AutoCheckDbContext(DbContextOptions<AutoCheckDbContext> options) : base(options)
        {
            public DbSet<Veiculo> Veiculos { get; set; }
            public DbSet<ItemVistoria> ItemVistorias { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Veiculo>()
                .HasDiscriminator<string>("TipoVeiculo")
                .HasValue<Veiculo>("Veiculo")
                .HasValue<Carro>("Carro")
                .HasValue<Moto>("Moto")
                .HasValue<Caminhao>("Caminhao");
        }
        
    }
}