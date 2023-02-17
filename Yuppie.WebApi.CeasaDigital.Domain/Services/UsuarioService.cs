using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Yuppie.WebApi.CeasaDigital.Domain.Interfaces;
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

        public Domain.Models.UsuarioModel.UsuarioModel BuscarUsuarioLogin(string user, string password)
        {
            try
            {
                return _mapper.Map<Domain.Models.UsuarioModel.UsuarioModel>(_usuarioRepository.BuscarUsuarioLogin(user, password));
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        public List<Domain.Models.UsuarioModel.UsuarioModel> BuscarUsuarios()
        {
            try
            {
                return _mapper.Map<List<Domain.Models.UsuarioModel.UsuarioModel>>(_usuarioRepository.BuscarUsuarios());

            }
            catch (Exception ex)
            {
            }
            return null;
        }

        public Domain.Models.UsuarioModel.UsuarioModel BuscarUsuarioPorDocumento(string documento)
        {
            try
            {
                return _mapper.Map<Domain.Models.UsuarioModel.UsuarioModel>(_usuarioRepository.BuscarUsuarioPorDocumento(documento));
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        public Domain.Models.UsuarioModel.UsuarioModel BuscarUsuarioPorId(int id)
        {
            try
            {
                return _mapper.Map<Domain.Models.UsuarioModel.UsuarioModel>(_usuarioRepository.BuscarUsuarioPorId(id));
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        public Domain.Models.UsuarioModel.UsuarioModel CadastrarUsuario(Domain.Models.UsuarioModel.UsuarioModel usuario)
        {
            try
            {
                //return JsonConvert.DeserializeObject<UsuarioModel>(JsonConvert.SerializeObject(_usuarioRepository.CadastrarUsuario(usuario)));
                return null;
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        public Domain.Models.UsuarioModel.UsuarioModel MudarStatusUsuario(Domain.Models.UsuarioModel.UsuarioModel usuario)
        {
            try
            {
                //return JsonConvert.DeserializeObject<UsuarioModel>(JsonConvert.SerializeObject(_usuarioRepository.MudarStatusUsuario(usuario)));
                return null;
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        public Domain.Models.UsuarioModel.UsuarioModel RecuperarSenhaUsuario(Domain.Models.UsuarioModel.UsuarioModel usuario)
        {
            try
            {
                //return JsonConvert.DeserializeObject<UsuarioModel>(JsonConvert.SerializeObject(_usuarioRepository.RecuperarSenhaUsuario(usuario)));
                return null;
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        public Domain.Models.UsuarioModel.UsuarioModel AtualizarUsuario(Domain.Models.UsuarioModel.UsuarioModel usuario)
        {
            try
            {
                //return JsonConvert.DeserializeObject<UsuarioModel>(JsonConvert.SerializeObject(_usuarioRepository.RecuperarSenhaUsuario(usuario)));
                return null;
            }
            catch (Exception ex)
            {
            }
            return null;
        }
    }
}
