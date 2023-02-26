﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yuppie.WebApi.CeasaDigital.Domain.Interfaces;
using Yuppie.WebApi.CeasaDigital.Domain.Models.UsuarioModel;
using Yuppie.WebApi.Infra.Repository;

namespace Yuppie.WebApi.CeasaDigital.Domain.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IMapper _mapper;
        private readonly IUsuarioRepository _usuarioRepository;
        public UsuarioService(IMapper mapper, IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public async Task<ObjectResult> BuscarUsuarioLogin(string user, string password)
        {
            try
            {
                var usuario =  _mapper.Map<UsuarioModel>(await _usuarioRepository.BuscarUsuarioLogin(user, password));
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
                var usuarios = _mapper.Map<UsuarioModel>(_usuarioRepository.BuscarUsuarioPorDocumento(documento));
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
              //TODO
                return new ObjectResult(true)
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (System.Exception ex)
            {
                return new ObjectResult(new { message = $"Falha ao cadastrar o usuário: {usuario.nome}!" })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        public async Task<ObjectResult> MudarStatusUsuario(Domain.Models.UsuarioModel.UsuarioModel usuario)
        {
            try
            {
                //TODO
                return new ObjectResult(true)
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        public async Task<ObjectResult> RecuperarSenhaUsuario(Domain.Models.UsuarioModel.UsuarioModel usuario)
        {
            try
            {
                //TODO
                return new ObjectResult(true)
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        public async Task<ObjectResult> AtualizarUsuario(Domain.Models.UsuarioModel.UsuarioModel usuario)
        {
            try
            {   //TODO
                return new ObjectResult(true)
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (Exception ex)
            {
            }
            return null;
        }
    }
}
