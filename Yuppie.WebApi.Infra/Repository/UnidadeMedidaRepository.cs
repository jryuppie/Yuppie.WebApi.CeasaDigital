using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yuppie.WebApi.Infra.Context;
using Yuppie.WebApi.Infra.Models.Produto;

namespace Yuppie.WebApi.Infra.Repository
{
    public interface IUnidadeMedidaRepository
    {
        UnidadeMedidaModel GetUnMedidaById(int id);
        IEnumerable<UnidadeMedidaModel> GetAllUnMedidas();
        void AddUnMedida(UnidadeMedidaModel usuario);
        void UpdateUnMedida(UnidadeMedidaModel usuario);
        void DeleteUnMedida(int id);
    }

    public class UnidadeMedidaRepository : IUnidadeMedidaRepository
    {
        private readonly PostGreContext _dbContext;
        public UnidadeMedidaRepository(PostGreContext context) => _dbContext = context;

        public UnidadeMedidaModel GetUnMedidaById(int id)
        {
            return _dbContext.UnMedidas.Find(id);
        }

        public IEnumerable<UnidadeMedidaModel> GetAllUnMedidas()
        {
            return _dbContext.UnMedidas.ToList();
        }

        public void AddUnMedida(UnidadeMedidaModel usuario)
        {
            _dbContext.UnMedidas.Add(usuario);
            _dbContext.SaveChanges();
        }

        public void UpdateUnMedida(UnidadeMedidaModel usuario)
        {
            _dbContext.UnMedidas.Update(usuario);
            _dbContext.SaveChanges();
        }

        public void DeleteUnMedida(int id)
        {
            var usuario = _dbContext.UnMedidas.Find(id);
            _dbContext.UnMedidas.Remove(usuario);
            _dbContext.SaveChanges();
        }
    }
}