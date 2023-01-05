using Microsoft.EntityFrameworkCore;
using TesteCandidatoWeb.Models;

namespace TesteCandidatoWeb.Data;
public class ApplicationDbContext : DbContext
{
    public DbSet<Endereco> Ceps { get; set; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Endereco>()
            .ToTable("CEP", "dbo")
            .HasKey(e => e.Id);

        base.OnModelCreating(modelBuilder);
    }
}