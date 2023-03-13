using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yuppie.WebApi.CeasaDigital.Domain.Interfaces;
using Yuppie.WebApi.CeasaDigital.Domain.Models.Endereco;
using Yuppie.WebApi.Infra.Repository;

namespace Yuppie.WebApi.CeasaDigital.Domain.Services
{
    public class EnderecoService : IEnderecoService
    {
        private readonly IMapper _mapper;
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        public EnderecoService(IMapper mapper, IEnderecoRepository enderecoRepository, IUsuarioRepository usuarioRepository)
        {
            _mapper = mapper;
            _enderecoRepository = enderecoRepository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<ObjectResult> BuscarTodosEnderecos()
        {
            try
            {
                var enderecos = _mapper.Map<List<EnderecoModel>>(await _enderecoRepository.BuscarTodosEnderecos());
                return new ObjectResult(enderecos)
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (System.Exception ex)
            {
                return new ObjectResult(new { message = "Falha ao buscar os enderecos!" })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        public async Task<ObjectResult> BuscarEnderecoPorIdUsuario(int IdUsuario)
        {
            try
            {
                var enderecos = _mapper.Map<EnderecoModel>(await _enderecoRepository.BuscarEnderecoPorIdUsuario(IdUsuario));
                if (enderecos != null)                
                    return new ObjectResult(enderecos){StatusCode = StatusCodes.Status200OK};
                else
                    return new ObjectResult(new { message = $"Não há endereço cadastrado para esse usuário" }) { StatusCode = StatusCodes.Status404NotFound };
            }
            catch (System.Exception ex)
            {
                return new ObjectResult(new { message = $"Falha ao buscar os enderecos do usuário: {IdUsuario}!" })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
        public async Task<ObjectResult> CadastrarEndereco(EnderecoModel endereco)
        {
            try
            {
                var usuario = await _usuarioRepository.BuscarUsuarioPorId(endereco.IdUsuario);
                if (usuario == null) return new ObjectResult(new { message = "Verifique o campo usuário e tente novamente!" }) { StatusCode = StatusCodes.Status500InternalServerError };

                var enderecoUsuario = await _enderecoRepository.BuscarEnderecoPorIdUsuario(endereco.IdUsuario);
                if (enderecoUsuario != null)
                    return new ObjectResult(new { message = "Usuário já possui endereço cadastrado!" }) { StatusCode = StatusCodes.Status200OK };

                var enderecoCadastro = _mapper.Map<Infra.Models.Endereco.EnderecoModel>(endereco);
                return await _enderecoRepository.CadastrarEndereco(enderecoCadastro);
            }
            catch (System.Exception ex)
            {
                return new ObjectResult(new { message = "Falha ao cadastrar o endereco!" })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
        public async Task<ObjectResult> AtualizarEndereco(EnderecoModel endereco)
        {
            try
            {
                var usuario = await _usuarioRepository.BuscarUsuarioPorId(endereco.IdUsuario);
                if (usuario == null) return new ObjectResult(new { message = "Falha ao atualizar o endereço, verifique o campo usuário e tente novamente!" }) { StatusCode = StatusCodes.Status500InternalServerError };

                var enderecoAtual = await _enderecoRepository.BuscarEnderecoPorIdUsuario(endereco.IdUsuario);
                if (enderecoAtual != null)
                {
                    var enderecoAtualizar = AtribuirCamposParaAtualizar(enderecoAtual, endereco);
                    return await _enderecoRepository.AtualizarEndereco(enderecoAtualizar);
                }
                else
                    return new ObjectResult(new { message = "Falha ao identificar o endereço para atualização!" }) { StatusCode = StatusCodes.Status400BadRequest };


            }
            catch (System.Exception ex)
            {
                return new ObjectResult(new { message = "Falha ao buscar os enderecos!" })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
        public async Task<ObjectResult> DesabilitarEndereco(int IdEndereco)
        {
            try
            {
                return await _enderecoRepository.AtualizarStatusEndereco(IdEndereco);
            }
            catch (System.Exception ex)
            {
                return new ObjectResult(new { message = "Falha ao buscar desabiltiar endereço!" })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
        public async Task<ObjectResult> ExcluirEndereco(int IdEndereco)
        {
            try
            {
                return await _enderecoRepository.ExcluirEndereco(IdEndereco);
            }
            catch (System.Exception ex)
            {
                return new ObjectResult(new { message = "Falha ao buscar desabiltiar endereço!" })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
        private Infra.Models.Endereco.EnderecoModel AtribuirCamposParaAtualizar(Infra.Models.Endereco.EnderecoModel enderecoAtual, EnderecoModel enderecoAtualizar)
        {
            try
            {
                if (enderecoAtual != null && enderecoAtualizar != null)
                {
                    if (enderecoAtualizar.Numero > 0 && enderecoAtualizar.Numero != enderecoAtual.Numero)
                        enderecoAtual.Numero = enderecoAtualizar.Numero;
                    if (enderecoAtualizar.Radius > 0 && enderecoAtualizar.Radius != enderecoAtual.Radius)
                        enderecoAtual.Radius = enderecoAtualizar.Radius;
                    if (!string.IsNullOrWhiteSpace(enderecoAtualizar.Cep) && enderecoAtualizar.Cep != enderecoAtual.Cep)
                        enderecoAtual.Cep = enderecoAtualizar.Cep;
                    if (!string.IsNullOrWhiteSpace(enderecoAtualizar.Complemento) && enderecoAtualizar.Complemento != enderecoAtual.Complemento)
                        enderecoAtual.Complemento = enderecoAtualizar.Complemento;
                    if (!string.IsNullOrWhiteSpace(enderecoAtualizar.Bairro) && enderecoAtualizar.Bairro != enderecoAtual.Bairro)
                        enderecoAtual.Bairro = enderecoAtualizar.Bairro;
                    if (!string.IsNullOrWhiteSpace(enderecoAtualizar.Cidade) && enderecoAtualizar.Cidade != enderecoAtual.Cidade)
                        enderecoAtual.Cidade = enderecoAtualizar.Cidade;
                    if (!string.IsNullOrWhiteSpace(enderecoAtualizar.UF) && enderecoAtualizar.UF != enderecoAtual.UF)
                        enderecoAtual.UF = enderecoAtualizar.UF;
                    if (!string.IsNullOrWhiteSpace(enderecoAtualizar.Latitude) && enderecoAtualizar.Latitude != enderecoAtual.Latitude)
                        enderecoAtual.Latitude = enderecoAtualizar.Latitude;
                    if (!string.IsNullOrWhiteSpace(enderecoAtualizar.Longitude) && enderecoAtualizar.Longitude != enderecoAtual.Longitude)
                        enderecoAtual.Longitude = enderecoAtualizar.Longitude;
                }
                return enderecoAtual;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
