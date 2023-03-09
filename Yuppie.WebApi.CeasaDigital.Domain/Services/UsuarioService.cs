using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yuppie.WebApi.CeasaDigital.Domain.Interfaces;
using Yuppie.WebApi.CeasaDigital.Domain.Models.Enums;
using Yuppie.WebApi.CeasaDigital.Domain.Models.UsuarioModel;
using Yuppie.WebApi.Infra.Repository;

namespace Yuppie.WebApi.CeasaDigital.Domain.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IMapper _mapper;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IWhatsappService _whatsappService;
        public UsuarioService(IMapper mapper, IUsuarioRepository usuarioRepository, IWhatsappService whatsappService)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
            _whatsappService = whatsappService;
        }

        public async Task<ObjectResult> BuscarUsuarioLogin(string user, string password)
        {
            try
            {
                var usuario = _mapper.Map<UsuarioModel>(await _usuarioRepository.BuscarUsuarioLogin(user, password));
                return new ObjectResult(usuario)
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (System.Exception ex)
            {
                return new ObjectResult(new { message = $"Falha ao buscar usuário: {user}!" })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        public async Task<ObjectResult> BuscarUsuarios()
        {
            try
            {
                var usuarios = _mapper.Map<List<UsuarioModel>>(await _usuarioRepository.BuscarTodosUsuarios());
                return new ObjectResult(usuarios)
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (System.Exception ex)
            {
                return new ObjectResult(new { message = "Falha ao buscar os usuários!" })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        public async Task<ObjectResult> BuscarUsuarioPorDocumento(string documento)
        {
            try
            {
                var usuario = _mapper.Map<UsuarioModel>(await _usuarioRepository.BuscarUsuarioPorDocumento(documento));
                return new ObjectResult(usuario)
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (System.Exception ex)
            {
                return new ObjectResult(new { message = "Falha ao buscar os usuários!" })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        public async Task<ObjectResult> BuscarUsuarioPorId(int id)
        {
            try
            {
                var usuarios = _mapper.Map<UsuarioModel>(await _usuarioRepository.BuscarUsuarioPorId(id));
                return new ObjectResult(usuarios)
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (System.Exception ex)
            {
                return new ObjectResult(new { message = "Falha ao buscar os usuários!" })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        public async Task<ObjectResult> CadastrarUsuario(UsuarioModel usuario)
        {
            try
            {
                var usuarioCadastro = _mapper.Map<Yuppie.WebApi.Infra.Models.UsuarioModel.UsuarioModel>(usuario);
                if (await _usuarioRepository.BuscarUsuarioPorDocumento(usuarioCadastro.Documento) == null)
                {
                    if (_usuarioRepository.CadastrarUsuario(usuarioCadastro).Result.StatusCode == 201)
                        _whatsappService.EnviarMensagemUsuario(usuarioCadastro.Documento, false);
                }
                else
                {
                    return new ObjectResult(new { message = $"Usuário {usuario.Nome} já cadastrado!" })
                    {
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                }
                return new ObjectResult(new { message = $"Usuário cadastrado com sucesso!" })
                {
                    StatusCode = StatusCodes.Status201Created
                };
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = $"Falha ao cadastrar o usuário: {usuario.Nome}!" })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        public async Task<ObjectResult> AtualizarStatusUsuario(string documento, bool status)
        {
            try
            {
                return await _usuarioRepository.AtualizarStatusUsuario(documento, status);
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = $"Falha ao atualizar o status do usuário: {documento}!" })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        public async Task<ObjectResult> RecuperarSenhaUsuario(string documento, string telefone)
        {
            try
            {
                var recuperaSenha = await _whatsappService.EnviarMensagemUsuario(documento, true, telefone);
                if (recuperaSenha.StatusCode == 200)
                {
                    return new ObjectResult(new { message = "Caso os seus dados estejam corretos, você receberá uma mensagem em seu whatsapp com a senha de acesso" })
                    {
                        StatusCode = StatusCodes.Status200OK
                    };
                }
                return recuperaSenha;
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
                var usuarioDB = await _usuarioRepository.BuscarUsuarioPorId(usuario.Id);
                usuarioDB = AtribuirCamposParaAtualizar(usuarioDB, usuario);
                if (usuarioDB != null)                                  
                    return await _usuarioRepository.AtualizarUsuario(usuarioDB);                
                else
                {
                    return new ObjectResult(new { message = $"Usuário não encontrado!" })
                    {
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }

            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = $"Falha ao atualizar os dados do usuário: {usuario.Nome}!" })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        private Infra.Models.UsuarioModel.UsuarioModel AtribuirCamposParaAtualizar(Infra.Models.UsuarioModel.UsuarioModel usuarioDb, UsuarioModel usuarioAtualiza)
        {
            try
            {
                if (usuarioDb != null && usuarioAtualiza != null)
                {
                    if (!string.IsNullOrWhiteSpace(usuarioAtualiza.Documento))
                        usuarioDb.Documento = usuarioAtualiza.Documento;
                    if (!string.IsNullOrWhiteSpace(usuarioAtualiza.Cep))
                        usuarioDb.Cep = usuarioAtualiza.Cep;
                    if (!string.IsNullOrWhiteSpace(usuarioAtualiza.Nome))
                        usuarioDb.Nome = usuarioAtualiza.Nome;
                    if (!string.IsNullOrWhiteSpace(usuarioAtualiza.Telefone))
                        usuarioDb.Telefone = usuarioAtualiza.Telefone;
                    if (!string.IsNullOrWhiteSpace(usuarioAtualiza.Sobrenome))
                        usuarioDb.Sobrenome = usuarioAtualiza.Sobrenome;
                    if (!string.IsNullOrWhiteSpace(usuarioAtualiza.Senha))
                        usuarioDb.Senha = usuarioAtualiza.Senha;
                    if (!string.IsNullOrWhiteSpace(usuarioAtualiza.Latitude))
                        usuarioDb.Latitude = usuarioAtualiza.Latitude;
                    if (!string.IsNullOrWhiteSpace(usuarioAtualiza.Longitude))
                        usuarioDb.Longitude = usuarioAtualiza.Longitude;
                }
                return usuarioDb;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
