using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
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
        public Task<ObjectResult> AtualizarStatusUsuario(string documento, bool status);
    }

    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly PostGreContext _dbContext;
        public UsuarioRepository(PostGreContext context) => _dbContext = context;


        public async Task<UsuarioModel> BuscarUsuarioPorId(int id)
        {
            try
            {
                return _dbContext.Usuarios.Where(x => x.Id == id).FirstOrDefault();
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
                return _dbContext.Usuarios.Where(x => x.Documento == documento).FirstOrDefault();
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
                return _dbContext.Usuarios.Where(x => x.Documento == documento && x.Senha == senha).FirstOrDefault();
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
                usuario.DataCriacao = DateTime.Now;
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
                usuario.DataAtualizacao= DateTime.Now;
                _dbContext.Usuarios.Add(usuario);
                _dbContext.SaveChanges();
                return new ObjectResult(new { message = $"Usuário {usuario.Nome} foi atualizado com sucesso!" })
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = $"Falha ao atualizar o usuário: {usuario.Nome}!" })
                {
                    StatusCode = 400
                };
            }
        }

        public async Task<ObjectResult> AtualizarStatusUsuario(string documento, bool status)
        {
            try
            {
                var usuario = _dbContext.Usuarios.Where(x => x.Documento == documento).FirstOrDefault();
                if (usuario != null)
                {
                    usuario.Status = status;
                    usuario.DataAtualizacao= DateTime.Now;  
                }
                _dbContext.Usuarios.Update(usuario);
                _dbContext.SaveChanges();
                return new ObjectResult(new { message = $"O status do usuário {usuario.Nome} foi atualizado com sucesso!" })
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = $"Falha ao atualizar o status!" })
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
                return new ObjectResult(new { message = $"Usuário {usuario.Nome} foi excluído com sucesso!" })
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = $"Falha ao excluir o usuário!" })
                {
                    StatusCode = 400
                };
            }
        }     
    }
}
