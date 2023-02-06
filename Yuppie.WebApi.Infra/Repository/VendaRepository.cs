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
        void AddVenda(VendaModel venda);
        void UpdateVenda(VendaModel venda);
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

        public void AddVenda(VendaModel venda)
        {
            _dbContext.Vendas.Add(venda);
            _dbContext.SaveChanges();
        }

        public void UpdateVenda(VendaModel venda)
        {
            _dbContext.Vendas.Update(venda);
            _dbContext.SaveChanges();
        }

        public void DeleteVenda(int id)
        {
            var venda = _dbContext.Vendas.Find(id);
            _dbContext.Vendas.Remove(venda);
            _dbContext.SaveChanges();
        }
    }
}
