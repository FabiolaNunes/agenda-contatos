using Microsoft.EntityFrameworkCore;

namespace AgendaDeContatos.Data;

public class AgendaContext : DbContext
{
    public DbSet<Contato> Contatos { get; set; }

    public DbSet<Endereco> Enderecos { get; set; }

    public AgendaContext(DbContextOptions<AgendaContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=agenda.db");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Endereco>()
           .HasOne(c => c.Contato)
           .WithMany(e => e.Enderecos)
           .HasForeignKey(c => c.ContatoId)
           .OnDelete(DeleteBehavior.Cascade);
    }
}