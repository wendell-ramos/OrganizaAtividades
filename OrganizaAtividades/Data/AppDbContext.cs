using Microsoft.EntityFrameworkCore;
using OrganizaAtividades.Models;

namespace OrganizaAtividades.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(
            DbContextOptions<AppDbContext> options
        ) : base(options)
        {
        }

        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Atividade>()
                .HasOne(a => a.Disciplina)
                .WithMany(d => d.Atividades)
                .HasForeignKey(a => a.DisciplinaId);

            modelBuilder.Entity<Atividade>()
                .HasOne(a => a.Categoria)
                .WithMany(c => c.Atividades)
                .HasForeignKey(a => a.CategoriaId);
        }

        public DbSet<Atividade> Atividades { get; set; }
        public DbSet<Disciplina> Disciplinas { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
