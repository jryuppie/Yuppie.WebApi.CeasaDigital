using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yuppie.WebApi.Infra.Context;
using Yuppie.WebApi.Infra.Models.UsuarioModel;

namespace Yuppie.WebApi.Infra.Repository
{
    public interface IUsuarioRepository
    {
        UsuarioModel GetUserById(int id);
        IEnumerable<UsuarioModel> GetAllUsers();
        void AddUser(UsuarioModel usuario);
        void UpdateUser(UsuarioModel usuario);
        void DeleteUser(int id);
    }

    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly PostGreContext _dbContext;
        public UsuarioRepository(PostGreContext context) => _dbContext = context;

        public UsuarioModel GetUserById(int id)
        {
            return _dbContext.Usuarios.Find(id);
        }

        public IEnumerable<UsuarioModel> GetAllUsers()
        {
            return _dbContext.Usuarios.ToList();
        }

        public void AddUser(UsuarioModel usuario)
        {
            _dbContext.Usuarios.Add(usuario);
            _dbContext.SaveChanges();
        }

        public void UpdateUser(UsuarioModel usuario)
        {
            _dbContext.Usuarios.Update(usuario);
            _dbContext.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            var usuario = _dbContext.Usuarios.Find(id);
            _dbContext.Usuarios.Remove(usuario);
            _dbContext.SaveChanges();
        }
    }
}
