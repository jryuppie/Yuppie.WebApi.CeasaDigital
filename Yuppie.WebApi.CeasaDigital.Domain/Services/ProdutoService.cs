using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yuppie.WebApi.CeasaDigital.Domain.Interfaces;
using Yuppie.WebApi.CeasaDigital.Domain.Models.Produto;
using Yuppie.WebApi.Infra.Repository;

namespace Yuppie.WebApi.CeasaDigital.Domain.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IMapper _mapper;
        private readonly IProdutoRepository _ProdutoRepository;
        public ProdutoService(IProdutoRepository ProdutoRepository, IMapper mapper)
        {
            _ProdutoRepository = ProdutoRepository;
            _mapper= mapper;    
        }

        public async Task<ObjectResult> BuscarTodosProdutos()
        {
            try
            {
                var produtos = _mapper.Map<List<ProdutoModel>>(await _ProdutoRepository.BuscarTodosProdutos());
                return new ObjectResult(produtos)
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (System.Exception ex)
            {
            }
            return null;
        }
    }
}
