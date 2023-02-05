using Microsoft.EntityFrameworkCore;
using Yuppie.WebApi.Infra.Models.Negociacao;
using Yuppie.WebApi.Infra.Models.Produto;
using Yuppie.WebApi.Infra.Models.UsuarioModel;


namespace Yuppie.WebApi.Infra.Context
{


    public class PostGreContext : DbContext
    {

        public PostGreContext(DbContextOptions<PostGreContext> options)
            : base(options)
        { }

        public DbSet<UsuarioModel> Usuarios { get; set; }
        public DbSet<ProdutoModel> Produtos { get; set; }
        public DbSet<UnidadeMedidaModel> UnMedidas { get; set; }
        public DbSet<VendaModel> Vendas { get; set; }
        public DbSet<OfertaModel> Ofertas { get; set; }
        public DbSet<ProcessoNegociacaoModel> ProcessoNegociacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");

            modelBuilder.Entity<UsuarioModel>().ToTable("usuario")
                .HasKey(u => u.id);

            modelBuilder.Entity<UsuarioModel>().Property(u => u.id)
                .HasDefaultValueSql("nextval('usuario_id_seq'::regclass)");

            modelBuilder.Entity<ProdutoModel>(entity =>
            {
                entity.ToTable("produtos");

                entity.HasKey(e => e.id)
                    .HasName("produtos_pkey");

                entity.Property(e => e.id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd()
                    .HasDefaultValueSql("nextval('produtos_id_seq'::regclass)");

                entity.Property(e => e.create_date)
                    .HasColumnName("create_date");

                entity.Property(e => e.nome)
                    .HasColumnName("nome")
                    .HasMaxLength(100);

                entity.Property(e => e.update_date)
                    .HasColumnName("update_date");

                entity.Property(e => e.categoria)
                    .HasColumnName("categoria")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<ProcessoNegociacaoModel>(entity =>
            {
                entity.ToTable("processo_negociacao");

                entity.HasKey(e => e.id);

                entity.Property(e => e.id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.aprovacao_comprador)
                    .HasColumnName("aprovacao_comprador");

                entity.Property(e => e.aprovacao_vendedor)
                    .HasColumnName("aprovacao_vendedor");

                entity.Property(e => e.avisa_cancelamento)
                    .HasColumnName("avisa_cancelamento");

                entity.Property(e => e.avisa_conclusao_venda)
                    .HasColumnName("avisa_conclusao_venda");

                entity.Property(e => e.avisa_negociacao_pendente)
                    .HasColumnName("avisa_negociacao_pendente");

                entity.Property(e => e.avisa_proposta_cancelamento)
                    .HasColumnName("avisa_proposta_cancelamento");

                entity.Property(e => e.avisa_inicio_negociacao)
                    .HasColumnName("avisa_inicio_negociacao");

                entity.Property(e => e.create_date)
                    .HasColumnName("create_date");

                entity.Property(e => e.id_venda)
                    .HasColumnName("id_venda");

                entity.Property(e => e.qtd_comprada)
                    .HasColumnName("qtd_comprada");

                entity.Property(e => e.status_negociacao)
                    .HasColumnName("status_negociacao");

                entity.Property(e => e.sub_status_negociacao)
                    .HasColumnName("sub_status_negociacao");

                entity.Property(e => e.update_date)
                    .HasColumnName("update_date");
            });

            modelBuilder.Entity<OfertaModel>(entity =>
            {
                entity.ToTable("oferta");

                entity.HasKey(e => e.id);

                entity.Property(e => e.id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd()
                    .HasDefaultValueSql("nextval('oferta_id_seq'::regclass)");

                entity.Property(e => e.create_date)
                    .HasColumnName("create_date");

                entity.Property(e => e.id_produto)
                    .HasColumnName("id_produto");

                entity.Property(e => e.id_un_medida)
                    .HasColumnName("id_un_medida");

                entity.Property(e => e.id_vendedor)
                    .HasColumnName("id_vendedor");

                entity.Property(e => e.peso_un_medida)
                    .HasColumnName("peso_un_medida");

                entity.Property(e => e.qtd_disponivel)
                    .HasColumnName("qtd_disponivel");

                entity.Property(e => e.status)
                    .HasColumnName("status");

                entity.Property(e => e.update_date)
                    .HasColumnName("update_date");

                entity.Property(e => e.vlkg)
                    .HasColumnName("vlkg");

                entity.Property(e => e.vl_un_medida)
                    .HasColumnName("vl_un_medida");
            });
            modelBuilder.Entity<UnidadeMedidaModel>(entity =>
            {
                entity.ToTable("public.unidade_medidas");

                entity.HasKey(e => e.id)
                    .HasName("unidade_medidas_pkey");

                entity.Property(e => e.id)
                    .HasColumnName("id");

                entity.Property(e => e.create_date)
                    .HasColumnName("create_date");

                entity.Property(e => e.nome)
                    .HasColumnName("nome")
                    .HasMaxLength(50);

                entity.Property(e => e.update_date)
                    .HasColumnName("update_date");
            });
        }
    }
}




