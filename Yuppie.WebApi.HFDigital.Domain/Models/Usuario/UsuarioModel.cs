using System;
using Microsoft.EntityFrameworkCore;

namespace Yuppie.WebApi.CeasaDigital.Domain.Models.UsuarioModel
{
    public class UsuarioModel
    {
        public int id { get; set; }
        public string cep { get; set; }
        public DateTime create_date { get; set; }
        public string documento { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string nome { get; set; }
        public string senha { get; set; }
        public string sobrenome { get; set; }
        public bool status { get; set; }
        public string telefone { get; set; }
        public string tipo_usuario { get; set; }
        public string tipo_pessoa { get; set; }
        public DateTime update_date { get; set; }
    }

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<UsuarioModel> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");

            modelBuilder.Entity<UsuarioModel>().ToTable("usuario")
                .HasKey(u => u.id);

            modelBuilder.Entity<UsuarioModel>().Property(u => u.id)
                .HasDefaultValueSql("nextval('usuario_id_seq'::regclass)");
        }
    }
}
