using System.Collections.Generic;
using System.Linq;
using Yuppie.WebApi.Infra.Context;
using Yuppie.WebApi.Infra.Models.Negociacao;


namespace Yuppie.WebApi.Infra.Repository
{
    public interface IVendaRepository
    {
        VendaModel BuscarVendaPorId(int id);
        List<VendaModel> BuscarVendaPorIdVendedor(int id);
        List<VendaModel> BuscarVendaPorIdComprador(int id);
        List<VendaModel> BuscarTodasVendas();
        void AddVenda(VendaModel venda);
        void UpdateVenda(VendaModel venda);
        void DeleteVenda(int id);
    }

    public class VendaRepository : IVendaRepository
    {
        private readonly PostGreContext _dbContext;
        public VendaRepository(PostGreContext context) => _dbContext = context;

        public VendaModel BuscarVendaPorId(int id)
        {
            return _dbContext.Vendas.Find(id);
        }

        public List<VendaModel> BuscarTodasVendas()
        {
            return _dbContext.Vendas.ToList();
        }

        public List<VendaModel> BuscarVendaPorIdVendedor(int id)
        {
            return _dbContext.Vendas.Where(x => x.id_vendedor == id).ToList();
        }

        public List<VendaModel> BuscarVendaPorIdComprador(int id)
        {
            return _dbContext.Vendas.Where(x => x.id_comprador == id).ToList();
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
