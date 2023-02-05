using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yuppie.WebApi.CeasaDigital.Domain.Interfaces;
using Yuppie.WebApi.Infra.Context;
using Yuppie.WebApi.CeasaDigital.Domain.Models.Produto;
using Yuppie.WebApi.Infra.Repository;

namespace Yuppie.WebApi.CeasaDigital.Domain.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _ProdutoRepository;
        public ProdutoService(IProdutoRepository ProdutoRepository)
        {
            _ProdutoRepository = ProdutoRepository;
        }

        public List<ProdutoModel> BuscarTodosProdutos()
        {
            try
            {
                return JsonConvert.DeserializeObject<List<ProdutoModel>>(JsonConvert.SerializeObject(_ProdutoRepository.GetAllProdutos())); ;
            }
            catch (System.Exception ex)
            {
            }
            return null;
        }
    }
}
