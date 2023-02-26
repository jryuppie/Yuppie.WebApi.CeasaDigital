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
                return new ObjectResult(new { message = "Falha ao buscar os produtos!" })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }            
        }

        public async Task<ObjectResult> AtualizarProduto(string categoria, string nome)
        {
            try
            {
                var produtos = _mapper.Map<List<ProdutoModel>>(await _ProdutoRepository.AtualizarProduto( categoria, nome));
                return new ObjectResult(produtos)
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (System.Exception ex)
            {
                return new ObjectResult(new { message = $"Falha ao atualizar o produto: {nome}!" })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }           
        }

        public async Task<ObjectResult> DeletarProduto(string categoria, string nome)
        {
            try
            {
                var produtos = _mapper.Map<ProdutoModel>(await _ProdutoRepository.ExcluirProduto(categoria,nome));
                return new ObjectResult(produtos)
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (System.Exception ex)
            {
                return new ObjectResult(new { message = $"Falha ao atualizar o produto: {nome}!" })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        public async Task<ObjectResult> BuscarProdutoPorNome(string nome)
        {
            try
            {
                var produtos = _mapper.Map<ProdutoModel>(await _ProdutoRepository.BuscarProdutoPorNome(nome));
                return new ObjectResult(produtos)
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (System.Exception ex)
            {
                return new ObjectResult(new { message = $"Falha ao atualizar o produto: {nome}!" })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }



        public async Task<ObjectResult> CadastrarProduto(string categoria, string nome)
        {
            try
            {
                var produtos = _mapper.Map<List<ProdutoModel>>(await _ProdutoRepository.AdicionarProduto(categoria,nome));
                return new ObjectResult(produtos)
                {
                    StatusCode = StatusCodes.Status201Created
                };
            }
            catch (System.Exception ex)
            {
                return new ObjectResult(new { message = $"Falha ao atualizar o produto: {nome}!" })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
    }
}
