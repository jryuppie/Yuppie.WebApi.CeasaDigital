using System.Collections.Generic;
using System.Linq;
using Yuppie.WebApi.Infra.Context;
using Yuppie.WebApi.Infra.Models.Negociacao;


namespace Yuppie.WebApi.Infra.Repository
{
    public interface IVendaRepository
    {
        VendaModel GetVendaById(int id);
        IEnumerable<VendaModel> GetAllVendas();
        void AddVenda(VendaModel produto);
        void UpdateVenda(VendaModel produto);
        void DeleteVenda(int id);
    }

    public class VendaRepository : IVendaRepository
    {
        private readonly PostGreContext _dbContext;
        public VendaRepository(PostGreContext context) => _dbContext = context;

        public VendaModel GetVendaById(int id)
        {
            return _dbContext.Vendas.Find(id);
        }

        public IEnumerable<VendaModel> GetAllVendas()
        {
            return _dbContext.Vendas.ToList();
        }

        public void AddVenda(VendaModel produto)
        {
            _dbContext.Vendas.Add(produto);
            _dbContext.SaveChanges();
        }

        public void UpdateVenda(VendaModel produto)
        {
            _dbContext.Vendas.Update(produto);
            _dbContext.SaveChanges();
        }

        public void DeleteVenda(int id)
        {
            var produto = _dbContext.Vendas.Find(id);
            _dbContext.Vendas.Remove(produto);
            _dbContext.SaveChanges();
        }
    }
}
