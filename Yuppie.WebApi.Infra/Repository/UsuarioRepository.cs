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
        public List<UsuarioModel> BuscarUsuarios();
        public UsuarioModel BuscarUsuarioLogin(string documento, string senha);
        public UsuarioModel BuscarUsuarioPorDocumento(string documento);
        public UsuarioModel BuscarUsuarioPorId(int id);
        public UsuarioModel CadastrarUsuario(UsuarioModel usuario);
        public UsuarioModel AtualizarUsuario(UsuarioModel usuario);
        public UsuarioModel MudarStatusUsuario(UsuarioModel usuario);
        public UsuarioModel RecuperarSenhaUsuario(UsuarioModel usuario);        
    }

    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly PostGreContext _dbContext;
        public UsuarioRepository(PostGreContext context) => _dbContext = context;

        public UsuarioModel BuscarUsuarioPorId(int id)
        {
            return _dbContext.Usuarios.Find(id);
        }

        public UsuarioModel BuscarUsuarioLogin(string documento, string senha)
        {
            return _dbContext.Usuarios.Where(x=> x.documento == documento && x.senha == senha).FirstOrDefault();
        }

        public List<UsuarioModel> BuscarUsuarios()
        {
            return _dbContext.Usuarios.ToList();
        }

        public UsuarioModel CadastrarUsuario(UsuarioModel usuario)
        {
            _dbContext.Usuarios.Add(usuario);
            _dbContext.SaveChanges();
            return usuario;
        }

        public UsuarioModel AtualizarUsuario(UsuarioModel usuario)
        {
            _dbContext.Usuarios.Update(usuario);
            _dbContext.SaveChanges();
            return usuario;
        }

        public void DeletarUsuario(int id)
        {
            var usuario = _dbContext.Usuarios.Find(id);
            _dbContext.Usuarios.Remove(usuario);
            _dbContext.SaveChanges();
        }

        public UsuarioModel MudarStatusUsuario(UsuarioModel usuario)
        {
            _dbContext.Usuarios.Update(usuario);
            _dbContext.SaveChanges();
            return usuario;
        }

        public UsuarioModel RecuperarSenhaUsuario(UsuarioModel usuario)
        {
            return _dbContext.Usuarios.Find(usuario);
        }

        public UsuarioModel BuscarUsuarioPorDocumento(string documento)
        {
            return _dbContext.Usuarios.Where(x => x.documento == documento).FirstOrDefault();
        }
    }
}
