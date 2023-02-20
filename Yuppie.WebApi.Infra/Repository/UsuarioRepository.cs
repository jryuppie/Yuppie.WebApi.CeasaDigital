using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuppie.WebApi.Infra.Context;
using Yuppie.WebApi.Infra.Models.Produto;
using Yuppie.WebApi.Infra.Models.UsuarioModel;

namespace Yuppie.WebApi.Infra.Repository
{
    public interface IUsuarioRepository
    {
        public Task<List<UsuarioModel>> BuscarTodosUsuarios();
        public Task<UsuarioModel> BuscarUsuarioPorDocumento(string documento);
        public Task<UsuarioModel> BuscarUsuarioPorId(int id);
        public Task<UsuarioModel> BuscarUsuarioLogin(string documento, string senha);
        public Task<ObjectResult> CadastrarUsuario(UsuarioModel usuario);
        public Task<ObjectResult> AtualizarUsuario(UsuarioModel usuario);        
        public Task<ObjectResult> RecuperarSenhaUsuario(UsuarioModel usuario);
    }

    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly PostGreContext _dbContext;
        public UsuarioRepository(PostGreContext context) => _dbContext = context;


        public async Task<UsuarioModel> BuscarUsuarioPorId(int id)
        {
            try
            {
                return _dbContext.Usuarios.Find(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<UsuarioModel> BuscarUsuarioPorDocumento(string documento)
        {
            try
            {
                return _dbContext.Usuarios.Where(x => x.documento == documento).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<UsuarioModel> BuscarUsuarioLogin(string documento, string senha)
        {
            try
            {
                return _dbContext.Usuarios.Where(x => x.documento == documento && x.senha == senha).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<List<UsuarioModel>> BuscarTodosUsuarios()
        {
            try
            {
                return _dbContext.Usuarios.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ObjectResult> CadastrarUsuario(UsuarioModel usuario)
        {
            try
            {
                _dbContext.Usuarios.Add(usuario);
                _dbContext.SaveChanges();
                return new ObjectResult(usuario)
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

        public async Task<ObjectResult> AtualizarUsuario(UsuarioModel usuario)
        {
            try
            {
                _dbContext.Usuarios.Add(usuario);
                _dbContext.SaveChanges();
                return new ObjectResult(usuario)
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

        public async Task<ObjectResult> ExcluirUsuario(int idUsuario)
        {
            try
            {
                var usuario = _dbContext.Usuarios.Find(idUsuario);
                _dbContext.Usuarios.Remove(usuario);
                _dbContext.SaveChanges();
                return new ObjectResult(usuario)
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
        public async Task<ObjectResult> RecuperarSenhaUsuario(UsuarioModel usuario)
        {
            try
            {
               // var usuario = _dbContext.Usuarios.Find(usuario.id);
                //TODO - ENTENDNER COMO É FEITO ESSE PROCESSO
                return new ObjectResult(usuario)
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
    }
}
