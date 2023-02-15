using System;
using System.Collections.Generic;
using System.Linq;
using Yuppie.WebApi.Infra.Context;
using Yuppie.WebApi.Infra.Models.Negociacao;

namespace Yuppie.WebApi.Infra.Repository
{
    public interface IProcessoNegociacaoRepository
    {
        ProcessoNegociacaoModel GetNegociacaoById(int id);
        public ProcessoNegociacaoModel GetNegociacaoByInformations(int idComprador, int idOferta, string status);
        IEnumerable<ProcessoNegociacaoModel> GetAllNegociacaos();
        void AddNegociacao(ProcessoNegociacaoModel produto);
        void UpdateNegociacao(ProcessoNegociacaoModel produto);
        void DeleteNegociacao(int id);
    }

    public class ProcessoNegociacaoRepository : IProcessoNegociacaoRepository
    {
        private readonly PostGreContext _dbContext;
        public ProcessoNegociacaoRepository(PostGreContext context) => _dbContext = context;

        public ProcessoNegociacaoModel GetNegociacaoById(int id)
        {
            return _dbContext.ProcessoNegociacoes.Find(id);
        }
        public ProcessoNegociacaoModel GetNegociacaoByInformations(int idComprador, int idOferta, string status)
        {
            return _dbContext.ProcessoNegociacoes.FirstOrDefault(x => x.id == idComprador && x.id_venda == idOferta && x.status_negociacao == status);
        }

        public IEnumerable<ProcessoNegociacaoModel> GetAllNegociacaos()
        {
            return _dbContext.ProcessoNegociacoes.ToList();
        }

        public void AddNegociacao(ProcessoNegociacaoModel produto)
        {
            _dbContext.ProcessoNegociacoes.Add(produto);
            _dbContext.SaveChanges();
        }

        public void UpdateNegociacao(ProcessoNegociacaoModel produto)
        {
            _dbContext.ProcessoNegociacoes.Update(produto);
            _dbContext.SaveChanges();
        }

        public void DeleteNegociacao(int id)
        {
            var produto = _dbContext.ProcessoNegociacoes.Find(id);
            _dbContext.ProcessoNegociacoes.Remove(produto);
            _dbContext.SaveChanges();
        }
    }
}
