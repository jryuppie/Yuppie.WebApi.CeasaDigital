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
        public Task<ProcessoNegociacaoModel> BuscarNegociacaoPorInformacoes(int idComprador, int idOferta, string status);
        public Task<List<ProcessoNegociacaoModel>> BuscarTodasNegociacoes();
        public Task<ObjectResult> AtualizarNegociacao(ProcessoNegociacaoModel negociacao);
        public Task<ObjectResult> DeletarNegociacao(int id);
        public Task<ObjectResult> AdicionarNegociacao(ProcessoNegociacaoModel negociacao);
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
        public async Task<ProcessoNegociacaoModel> BuscarNegociacaoPorInformacoes(int idComprador, int idOferta, string status)
        {
            try
            {
                return _dbContext.ProcessoNegociacoes.FirstOrDefault(x => x.id == idComprador && x.id_venda == idOferta && x.status_negociacao == status);
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
                negociacao.create_date= DateTime.Now;
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
                return _dbContext.ProcessoNegociacoes.Where(x => x.id_venda == IdVenda).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
