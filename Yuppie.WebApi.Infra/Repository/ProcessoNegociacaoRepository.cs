using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yuppie.WebApi.Infra.Context;
using Yuppie.WebApi.Infra.Models.Negociacao;

namespace Yuppie.WebApi.Infra.Repository
{
    public interface IProcessoNegociacaoRepository
    {
        public Task<ProcessoNegociacaoModel> BuscarNegociacaoPorId(int id);
        public Task<ProcessoNegociacaoModel> BuscarNegociacaoPorIdVenda(int IdVenda);
        public Task<ProcessoNegociacaoModel> BuscarNegociacaoPorInformacoes(int idComprador, int idOferta, string status);
        public Task<List<ProcessoNegociacaoModel>> BuscarTodasNegociacoes();
        public Task<ObjectResult> AtualizarNegociacao(ProcessoNegociacaoModel negociacao);
        public Task<ObjectResult> DeletarNegociacao(int id);
        public Task<ObjectResult> AdicionarNegociacao(ProcessoNegociacaoModel negociacao);
        public Task<ObjectResult> AdicionarNegociacao(int IdNegociacao, int Quantidade, string status);
        public Task<ProcessoNegociacaoModel> BuscarNegociacaoPorVenda(int IdVenda);

    }

    public class ProcessoNegociacaoRepository : IProcessoNegociacaoRepository
    {
        private readonly PostGreContext _dbContext;
        public ProcessoNegociacaoRepository(PostGreContext context) => _dbContext = context;

        public async Task<ProcessoNegociacaoModel> BuscarNegociacaoPorId(int id)
        {
            try
            {
                return _dbContext.ProcessoNegociacoes.Find(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<ProcessoNegociacaoModel> BuscarNegociacaoPorIdVenda(int IdVenda)
        {
            try
            {
                return _dbContext.ProcessoNegociacoes.Where(x => x.IdVenda == IdVenda).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<ProcessoNegociacaoModel> BuscarNegociacaoPorInformacoes(int idComprador, int idOferta, string status)
        {
            try
            {
                return _dbContext.ProcessoNegociacoes.FirstOrDefault(x => x.Id == idComprador && x.IdVenda == idOferta && x.StatusNegociacao == status);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<ProcessoNegociacaoModel>> BuscarTodasNegociacoes()
        {
            try
            {
                return _dbContext.ProcessoNegociacoes.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ObjectResult> AdicionarNegociacao(ProcessoNegociacaoModel negociacao)
        {
            try
            {
                negociacao.DataCriacao = DateTime.Now;
                _dbContext.ProcessoNegociacoes.Add(negociacao);
                _dbContext.SaveChanges();
                return new ObjectResult(negociacao)
                {
                    StatusCode = StatusCodes.Status201Created
                };
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message })
                {
                    StatusCode = 400
                };
            }
        }

        public async Task<ObjectResult> AdicionarNegociacao(int IdNegociacao, int Quantidade, string status)
        {
            try
            {
                var negociacao = new ProcessoNegociacaoModel()
                {
                    IdVenda = IdNegociacao,
                    QtdComprada = Quantidade,
                    DataCriacao = DateTime.Now,
                    StatusNegociacao = status
                };
                _dbContext.ProcessoNegociacoes.Add(negociacao);
                _dbContext.SaveChanges();
                return new ObjectResult(negociacao)
                {
                    StatusCode = StatusCodes.Status201Created
                };
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message })
                {
                    StatusCode = 400
                };
            }
        }

        public async Task<ObjectResult> AtualizarNegociacao(ProcessoNegociacaoModel negociacao)
        {
            try
            {
                negociacao.DataAtualizacao = DateTime.Now;
                _dbContext.ProcessoNegociacoes.Update(negociacao);
                _dbContext.SaveChanges();
                return new ObjectResult(negociacao)
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message })
                {
                    StatusCode = 400
                };
            }
        }

        public async Task<ObjectResult> DeletarNegociacao(int id)
        {
            try
            {
                var produto = _dbContext.ProcessoNegociacoes.Find(id);
                _dbContext.ProcessoNegociacoes.Remove(produto);
                _dbContext.SaveChanges();
                return new ObjectResult(produto)
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message })
                {
                    StatusCode = 400
                };
            }
        }


        public async Task<ProcessoNegociacaoModel> BuscarNegociacaoPorVenda(int IdVenda)
        {
            try
            {
                return _dbContext.ProcessoNegociacoes.Where(x => x.IdVenda == IdVenda).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
