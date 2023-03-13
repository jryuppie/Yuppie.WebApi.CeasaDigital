using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Yuppie.WebApi.Infra.Models;
using Yuppie.WebApi.Infra.Models.Endereco;
using Yuppie.WebApi.Infra.Models.Negociacao;
using Yuppie.WebApi.Infra.Models.Produto;
using Yuppie.WebApi.Infra.Models.UsuarioModel;

namespace Yuppie.WebApi.Infra.Interfaces
{
    public interface IPostGreeContext
    {
        public DbSet<UsuarioModel> Usuarios { get; set; }
        public DbSet<ProdutoModel> Produtos { get; set; }
        public DbSet<UnidadeMedidaModel> UnMedidas { get; set; }
        public DbSet<VendaModel> Vendas { get; set; }
        public DbSet<OfertaModel> Ofertas { get; set; }
        public DbSet<ProcessoNegociacaoModel> Negociacoas { get; set; }
        public DbSet<EnderecoModel> Enderecos { get; set; }
    }
}
