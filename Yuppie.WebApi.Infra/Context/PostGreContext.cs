using Microsoft.EntityFrameworkCore;
using Yuppie.WebApi.Infra.Models.Endereco;
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
        public DbSet<EnderecoModel> Enderecos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");

            modelBuilder.Entity<UsuarioModel>(entity =>
            {
                entity.ToTable("usuario");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('usuario_id_seq'::regclass)");

                entity.Property(e => e.Cep)
                    .HasColumnName("cep")
                    .HasMaxLength(8);

                entity.Property(e => e.DataCriacao)
                    .HasColumnName("create_date")
                    .HasColumnType("timestamp without time zone");

                entity.Property(e => e.Documento)
                    .HasColumnName("documento")
                    .HasMaxLength(16);

                entity.Property(e => e.Latitude)
                    .HasColumnName("latitude")
                    .HasMaxLength(50);

                entity.Property(e => e.Longitude)
                    .HasColumnName("longitude")
                    .HasMaxLength(50);

                entity.Property(e => e.Nome)
                    .HasColumnName("nome")
                    .HasMaxLength(255);

                entity.Property(e => e.Senha)
                    .HasColumnName("senha")
                    .HasMaxLength(255);

                entity.Property(e => e.Sobrenome)
                    .HasColumnName("sobrenome")
                    .HasMaxLength(100);

                entity.Property(e => e.Status)
                    .HasColumnName("status");

                entity.Property(e => e.Telefone)
                    .HasColumnName("telefone")
                    .HasMaxLength(11);

                entity.Property(e => e.TipoPessoa)
                    .HasColumnName("tipo_pessoa")
                    .HasMaxLength(8);

                entity.Property(e => e.TipoUsuario)
                    .HasColumnName("tipo_usuario")
                    .HasMaxLength(10);

                entity.Property(e => e.DataAtualizacao)
                    .HasColumnName("update_date")
                    .HasColumnType("timestamp without time zone");

                entity.HasKey(e => e.Id)
                    .HasName("usuario_pkey");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('usuario_id_seq'::regclass)");

                entity.ToTable("usuario");
            });

            modelBuilder.Entity<ProdutoModel>(entity =>
            {
                entity.ToTable("produtos");

                entity.HasKey(e => e.Id)
                    .HasName("produtos_pkey");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd()
                    .HasDefaultValueSql("nextval('produtos_id_seq'::regclass)");

                entity.Property(e => e.DataCriacao)
                    .HasColumnName("create_date")
                  .HasColumnType("timestamp without time zone");

                entity.Property(e => e.Nome)
                    .HasColumnName("nome")
                    .HasMaxLength(100);

                entity.Property(e => e.DataAtualizacao)
                    .HasColumnName("update_date")
                      .HasColumnType("timestamp without time zone");

                entity.Property(e => e.Categoria)
                    .HasColumnName("categoria")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<ProcessoNegociacaoModel>(entity =>
            {
                entity.ToTable("processo_negociacao");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.AprovacaoComprador)
                    .HasColumnName("aprovacao_comprador");

                entity.Property(e => e.AprovacaoVendedor)
                    .HasColumnName("aprovacao_vendedor");

                entity.Property(e => e.AvisaCancelamento)
                    .HasColumnName("avisa_cancelamento");

                entity.Property(e => e.AvisaConclusaoVenda)
                    .HasColumnName("avisa_conclusao_venda");

                entity.Property(e => e.AvisaNegociacaoPendente)
                    .HasColumnName("avisa_negociacao_pendente");

                entity.Property(e => e.AvisaPropostaCancelada)
                    .HasColumnName("avisa_proposta_cancelamento");

                entity.Property(e => e.AvisaInicioNegociacao)
                    .HasColumnName("avisa_inicio_negociacao");

                entity.Property(e => e.DataCriacao)
                    .HasColumnName("create_date")
                      .HasColumnType("timestamp without time zone");

                entity.Property(e => e.IdVenda)
                    .HasColumnName("id_venda");

                entity.Property(e => e.QtdComprada)
                    .HasColumnName("qtd_comprada");

                entity.Property(e => e.StatusNegociacao)
                    .HasColumnName("status_negociacao");

                entity.Property(e => e.SubStatusNegociacao)
                    .HasColumnName("sub_status_negociacao");

                entity.Property(e => e.DataAtualizacao)
                    .HasColumnName("update_date")
                      .HasColumnType("timestamp without time zone");

                entity.Property(e => e.DataEnvioMensagem)
                  .HasColumnName("sent_message_date")
                    .HasColumnType("timestamp without time zone");

                entity.Property(e => e.EnvioMensagemContador)
                      .HasColumnName("sent_message_counter");
            });

            modelBuilder.Entity<OfertaModel>(entity =>
            {
                entity.ToTable("oferta");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd()
                    .HasDefaultValueSql("nextval('oferta_id_seq'::regclass)");

                entity.Property(e => e.DataCriacao)
                    .HasColumnName("create_date")
                      .HasColumnType("timestamp without time zone");

                entity.Property(e => e.IdProduto)
                    .HasColumnName("id_produto");

                entity.Property(e => e.IdUnMedida)
                    .HasColumnName("id_un_medida");

                entity.Property(e => e.IdVendedor)
                    .HasColumnName("id_vendedor");

                entity.Property(e => e.PesoUnMedida)
                    .HasColumnName("peso_un_medida");

                entity.Property(e => e.QtdDisponivel)
                    .HasColumnName("qtd_disponivel");

                entity.Property(e => e.Status)
                    .HasColumnName("status");

                entity.Property(e => e.DataAtualizacao)
                    .HasColumnName("update_date")
                      .HasColumnType("timestamp without time zone");

                entity.Property(e => e.ValorKg)
                    .HasColumnName("vlkg");

                entity.Property(e => e.ValorUnMedida)
                    .HasColumnName("vl_un_medida");

                entity.Property(e => e.DataEnvioMensagem)
                      .HasColumnName("sent_message_date")
                        .HasColumnType("timestamp without time zone");

                entity.Property(e => e.EnvioMensagemContador)
                      .HasColumnName("sent_message_counter");
            });

            modelBuilder.Entity<UnidadeMedidaModel>(entity =>
            {
                entity.ToTable("unidade_medidas");

                entity.HasKey(e => e.Id)
                    .HasName("unidade_medidas_pkey");

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.DataCriacao)
                    .HasColumnName("create_date")
                      .HasColumnType("timestamp without time zone");

                entity.Property(e => e.Nome)
                    .HasColumnName("nome")
                    .HasMaxLength(50);

                entity.Property(e => e.DataAtualizacao)
                    .HasColumnName("update_date")
                      .HasColumnType("timestamp without time zone");
            });

            modelBuilder.Entity<VendaModel>(entity =>
            {
                entity.ToTable("venda");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.AvaliacaoComprador)
                    .HasColumnName("avaliacao_comprador");

                entity.Property(e => e.AvaliacaoVendedor)
                    .HasColumnName("avaliacao_vendedor");

                entity.Property(e => e.DataCriacao)
                    .HasColumnName("create_date")
                      .HasColumnType("timestamp without time zone");

                entity.Property(e => e.IdComprador)
                    .HasColumnName("id_comprador");

                entity.Property(e => e.IdOferta)
                    .HasColumnName("id_oferta");

                entity.Property(e => e.IdVendedor)
                    .HasColumnName("id_vendedor");

                entity.Property(e => e.QtdComprada)
                    .HasColumnName("qtd_comprada");

                entity.Property(e => e.DataAtualizacao)
                    .HasColumnName("update_date")
                      .HasColumnType("timestamp without time zone");

                entity.Property(e => e.ValorTransacao)
                    .HasColumnName("valor_transacao");

                entity.Property(e => e.VendaStatus)
                    .HasColumnName("venda_status");
            });

            modelBuilder.Entity<EnderecoModel>(entity =>
            {
                entity.ToTable("enderecos");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.UF)
                    .HasColumnName("uf")
                    .HasMaxLength(2);

                entity.Property(e => e.Bairro)
                    .HasColumnName("bairro")
                    .HasMaxLength(255);

                entity.Property(e => e.Cep)
                    .HasColumnName("cep")
                    .HasMaxLength(15);

                entity.Property(e => e.Cidade)
                    .HasColumnName("cidade")
                    .HasMaxLength(50);

                entity.Property(e => e.Complemento)
                    .HasColumnName("complemento")
                    .HasMaxLength(100);

                entity.Property(e => e.DataCriacao)
                    .HasColumnName("create_date")
                    .HasColumnType("timestamp without time zone");

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("id_usuario");

                entity.Property(e => e.Latitude)
                    .HasColumnName("latitude")
                    .HasMaxLength(50);

                entity.Property(e => e.Logradouro)
                    .HasColumnName("logradouro")
                    .HasMaxLength(255);

                entity.Property(e => e.Longitude)
                    .HasColumnName("longitude")
                    .HasMaxLength(50);

                entity.Property(e => e.Numero)
                    .HasColumnName("numero");

                entity.Property(e => e.Radius)
                    .HasColumnName("radius");

                entity.Property(e => e.Status)
                    .HasColumnName("status");

                entity.Property(e => e.DataAtualizacao)
                    .HasColumnName("update_date")
                    .HasColumnType("timestamp without time zone");

                entity.HasCheckConstraint("enderecos_id_usuario_check", "id_usuario >= 1 AND id_usuario <= '9223372036854775807'::bigint");

                entity.HasCheckConstraint("enderecos_radius_check", "radius >= 1 AND radius <= '9223372036854775807'::bigint");
            });
        }
    }
}




