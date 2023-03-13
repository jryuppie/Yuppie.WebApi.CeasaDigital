using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yuppie.WebApi.Infra.Context;
using Yuppie.WebApi.Infra.Models.Endereco;

namespace Yuppie.WebApi.Infra.Repository
{
    public interface IEnderecoRepository
    {
        public Task<List<EnderecoModel>> BuscarTodosEnderecos();
        public Task<EnderecoModel> BuscarEnderecoPorIdUsuario(int idUsuario);
        public Task<ObjectResult> CadastrarEndereco(EnderecoModel Endereco);
        public Task<ObjectResult> AtualizarEndereco(EnderecoModel Endereco);
        public Task<ObjectResult> AtualizarStatusEndereco(int idEndereco);
        public Task<ObjectResult> ExcluirEndereco(int idEndereco);
    }

    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly PostGreContext _dbContext;
        public EnderecoRepository(PostGreContext context) => _dbContext = context;

        public async Task<EnderecoModel> BuscarEnderecoPorIdUsuario(int idUsuario)
        {
            try
            {
                return _dbContext.Enderecos.Where(x => x.IdUsuario == idUsuario && x.Status == true).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<List<EnderecoModel>> BuscarTodosEnderecos()
        {
            try
            {
                return _dbContext.Enderecos.Where(x => x.Status == true).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<ObjectResult> CadastrarEndereco(EnderecoModel Endereco)
        {
            try
            {
                Endereco.DataCriacao = DateTime.Now;
                _dbContext.Enderecos.Add(Endereco);
                _dbContext.SaveChanges();
                return new ObjectResult(new { message = "Endereço cadastrado com sucesso!" })
                {
                    StatusCode = StatusCodes.Status201Created
                };
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = "Falha ao cadastrar o endereço!" })
                {
                    StatusCode = 400
                };
            }
        }
        public async Task<ObjectResult> AtualizarEndereco(EnderecoModel Endereco)
        {
            try
            {
                Endereco.DataAtualizacao = DateTime.Now;
                _dbContext.Update(Endereco);
                _dbContext.SaveChanges();
                return new ObjectResult(new { message = $"Endereço atualizado com sucesso!" })
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = $"Falha ao atualizar o endereço!" })
                {
                    StatusCode = 400
                };
            }
        }
        public async Task<ObjectResult> AtualizarStatusEndereco(int IdEndereco)
        {
            try
            {
                var endereco = _dbContext.Enderecos.Where(x => x.Id == IdEndereco).FirstOrDefault();
                if (endereco != null)
                {
                    endereco.Status = false;
                    _dbContext.SaveChanges();
                    return new ObjectResult(new { message = $"Endereço desabilitado com sucesso!" })
                    {
                        StatusCode = StatusCodes.Status200OK
                    };
                }
                return new ObjectResult(new { message = $"Endereço não encontrado!" })
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = $"Falha ao desabilitar o endereço!" })
                {
                    StatusCode = 400
                };
            }
        }
        public async Task<ObjectResult> ExcluirEndereco(int IdEndereco)
        {
            try
            {
                var endereco = _dbContext.Enderecos.Where(x => x.Id == IdEndereco).FirstOrDefault();
                if (endereco != null)
                {
                    _dbContext.Remove(endereco);
                    _dbContext.SaveChanges();
                    return new ObjectResult(new { message = $"Endereço excluído com sucesso!" })
                    {
                        StatusCode = StatusCodes.Status200OK
                    };
                }
                return new ObjectResult(new { message = $"Endereço não encontrado!" })
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = $"Falha ao excluir o endereço!" })
                {
                    StatusCode = 400
                };
            }
        }
    }
}
