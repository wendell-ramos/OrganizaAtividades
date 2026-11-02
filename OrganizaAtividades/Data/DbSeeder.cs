using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OrganizaAtividades.Models;
using OrganizaAtividades.Security;

namespace OrganizaAtividades.Data
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(
            AppDbContext context,
            IPasswordHasher<Usuario> hasher)
        {
            await CriarUsuarioSeNaoExistir(
                context,
                hasher,
                "Administrador",
                "admin@organiza.com",
                "Admin@123",
                Perfis.Administrador
            );

            await CriarUsuarioSeNaoExistir(
                context,
                hasher,
                "Usuário Comum",
                "usuario@organiza.com",
                "Usuario@123",
                Perfis.Usuario
            );

            await context.SaveChangesAsync();
        }

        private static async Task CriarUsuarioSeNaoExistir(
            AppDbContext context,
            IPasswordHasher<Usuario> hasher,
            string nome,
            string email,
            string senha,
            string perfil)
        {
            var usuarioExiste = await context.Usuarios
                .AnyAsync(u => u.Email == email);

            if (usuarioExiste)
            {
                return;
            }

            var usuario = new Usuario
            {
                Nome = nome,
                Email = email,
                Perfil = perfil,
                Ativo = true
            };

            usuario.SenhaHash = hasher
                .HashPassword(usuario, senha);

            context.Usuarios.Add(usuario);
        }
    }
}
