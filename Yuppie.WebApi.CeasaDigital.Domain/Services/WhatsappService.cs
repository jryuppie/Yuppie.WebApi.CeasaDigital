using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Yuppie.WebApi.CeasaDigital.Domain.Interfaces;
using Newtonsoft.Json;
using System.Text;
using Yuppie.WebApi.Infra.Repository;
using Yuppie.WebApi.CeasaDigital.Domain.Models;
using static Google.Protobuf.WellKnownTypes.Field.Types;
using System.Security.Cryptography;
using System.Collections.Generic;

namespace Yuppie.WebApi.CeasaDigital.Domain.Services
{
    public class WhatsappService : IWhatsappService
    {
        private readonly IMapper _mapper;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IOfertaRepository _ofertaRepository;
        private static string appLink = "http://app.ceasadigital.com.br:3001";
        private static string telefonePrefixo = "55";
        private static string urlEnvioMensagem = "http://192.168.2.222:3000/whatsapp/send-text";
        public WhatsappService(IMapper mapper, IProdutoRepository produtoRepository, IUsuarioRepository usuarioRepository, IOfertaRepository ofertaRepository)
        {
            _mapper = mapper;
            _produtoRepository = produtoRepository;
            _usuarioRepository = usuarioRepository;
            _ofertaRepository = ofertaRepository;
        }

        public async Task<ObjectResult> EnviarMensagemOferta(MensagemModel model)
        {
            try
            {
                var produto = await _produtoRepository.BuscarProdutoPorId(model.IdProduto);
                var vendedor = await _usuarioRepository.BuscarUsuarioPorId(model.IdVendedor);
                var comprador = await _usuarioRepository.BuscarUsuarioPorId(model.IdComprador);
                if (produto != null && vendedor != null && comprador != null)
                {
                    var mensagem = await CriarConteudoMensagemOferta(vendedor.Nome, comprador.Nome, produto.Nome);
                    if (mensagem != "")
                        return await ExecutarPostAsync(model.Prefixo, vendedor.Telefone, mensagem);
                }
                return new ObjectResult(new { message = "Erro ao enviar a mensagem!" })
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
            catch (Exception)
            {
                return new ObjectResult(new { message = "Falha ao enviar a mensagem!" })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
        public async Task<ObjectResult> EnviarMensagemNegociacao(MensagemModel model)
        {
            try
            {
                var IdProduto = model.IdProduto != null ? model.IdProduto : 0;

                if (IdProduto == 0 && model.IdOferta != 0)
                {
                    var oferta = await _ofertaRepository.BuscarOfertaPorId(model.IdOferta);
                    IdProduto = oferta.IdProduto;
                }
                var produto = await _produtoRepository.BuscarProdutoPorId(IdProduto);
                var vendedor = await _usuarioRepository.BuscarUsuarioPorId(model.IdVendedor);
                var comprador = await _usuarioRepository.BuscarUsuarioPorId(model.IdComprador);
                if (produto != null && vendedor != null && comprador != null)
                {
                    string contato1 = vendedor.Nome;
                    string contato2 = comprador.Nome;
                    string telefone = vendedor.Telefone;
                    string produtoNome = produto.Nome;
                    var mensagem = "";
                    if (model.EnvioComprador)
                    {
                        contato1 = comprador.Nome;
                        contato2 = vendedor.Nome;
                        telefone = comprador.Telefone;
                    }

                    bool tipoEnvio = model.EnvioComprador ? true : false;
                    if (model.ConclusaoVenda) { tipoEnvio = model.ConclusaoVenda; }
                    mensagem = await CriarConteudoMensagemNegociacao(contato1, contato2, produtoNome, tipoEnvio);
                    if (mensagem != "")
                        return await ExecutarPostAsync(model.Prefixo, telefone, mensagem);

                }
                return new ObjectResult(new { message = "Erro ao enviar a mensagem!" })
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
            catch (Exception)
            {
                return new ObjectResult(new { message = "Falha ao enviar a mensagem!" })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
        public async Task<ObjectResult> EnviarMensagemVenda(MensagemModel model, bool EdicaoVenda)
        {
            try
            {
                var oferta = await _ofertaRepository.BuscarOfertaPorId(model.IdOferta);
                if (oferta != null)
                {
                    var produto = await _produtoRepository.BuscarProdutoPorId(oferta.IdProduto);
                    var vendedor = await _usuarioRepository.BuscarUsuarioPorId(model.IdVendedor);
                    var comprador = await _usuarioRepository.BuscarUsuarioPorId(model.IdComprador);
                    if (produto != null && vendedor != null && comprador != null)
                    {
                        if (EdicaoVenda)
                        {
                            var mensagem = await CriarConteudoMensagemEditarVenda(vendedor.Nome, comprador.Nome, produto.Nome, EdicaoVenda);
                            if (mensagem != "")
                                return await ExecutarPostAsync(model.Prefixo, vendedor.Telefone, mensagem);
                        }
                    }
                }
                return new ObjectResult(new { message = $"Erro ao enviar a mensagem de edição da venda!" })
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
            catch (Exception)
            {
                return new ObjectResult(new { message = $"Erro ao enviar a mensagem de edição da venda!" })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
        public async Task<ObjectResult> EnviarMensagemUsuario(string Documento, bool RecuperarSenha, string telefone = "")
        {
            try
            {
                string mensagem = "";
                var usuario = await _usuarioRepository.BuscarUsuarioPorDocumento(Documento);
                if (usuario != null)
                {
                    if (RecuperarSenha)
                    {
                        if (telefone == usuario.Telefone)
                            mensagem = await CriarConteudoMensagemRecupearUsuario(usuario.Nome, usuario.Senha);
                    }
                    else
                        mensagem = await CriarConteudoMensagemCriarUsuario(usuario.Nome);

                    if (mensagem != "")
                        return await ExecutarPostAsync("55", usuario.Telefone, mensagem);
                }
                return new ObjectResult(new { message = "Erro ao enviar a mensagem!" })
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
            catch (Exception)
            {
                return new ObjectResult(new { message = "Falha ao enviar a mensagem!" })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }


        #region Utilitarios
        private async Task<ObjectResult> ExecutarPostAsync(string prefixo, string telefone, string mensagem)
        {
            try
            {
                var parameters = new Dictionary<string, string>
                   {
                        { "phone",prefixo + telefone },
                        { "message",  mensagem }
                    };
                var encodedContent = new FormUrlEncodedContent(parameters);
                using var client = new HttpClient();
                var response = await client.PostAsync(urlEnvioMensagem, encodedContent);
                var responseContent = await response.Content.ReadAsStringAsync();
                return new ObjectResult(new { message = "Mensagem enviada com sucesso!" })
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region StringBuilders Mensagem
        public async Task<string> CriarConteudoMensagemOferta(string Contato1, string Contato2, string Produto)
        {
            try
            {
                var messageBuilder = new StringBuilder();
                messageBuilder.Append($"Olá, {Contato1}.\r\n");
                messageBuilder.Append($"O nosso parceiro {Contato2} demonstrou interesse na sua oferta de: *{Produto}* :)\r\n\r\n");
                messageBuilder.Append("Acesse o aplicativo e dê continuidade ao processo de venda.\r\n\r\n");
                messageBuilder.Append($"Link do Aplicativo: {appLink} \U0001F4F1 \r\n\r\n");
                messageBuilder.Append("Atenciosamente,\r\nEquipe Ceasa Digital \U0001F600");

                return messageBuilder.ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }
        public async Task<string> CriarConteudoMensagemNegociacao(string Contato1, string Contato2, string Produto, bool venda)
        {
            try
            {
                string tipo = venda ? "venda" : "compra";
                string status = venda ? "concluído" : "cancelado";
                var messageBuilder = new StringBuilder();
                messageBuilder.Append($"Olá, {Contato1}.\r\n");
                messageBuilder.Append($"seu processo negocial com {Contato2}\r\n\r\n");
                messageBuilder.Append($"relacionado a {tipo} de: *{Produto}* foi *{status}* com sucesso!.\r\n\r\n");
                messageBuilder.Append($"Link do Aplicativo: {appLink} \U0001F4F1 \r\n\r\n");
                messageBuilder.Append("Atenciosamente,\r\nEquipe Ceasa Digital \U0001F600");

                return messageBuilder.ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }
        public async Task<string> CriarConteudoMensagemCriarUsuario(string Contato1)
        {
            try
            {
                var messageBuilder = new StringBuilder();
                messageBuilder.Append($"Olá, {Contato1}.\r\n");
                messageBuilder.Append($"Estamos muito felizes que se juntou ao Ceasa Digital \r\n\r\n");
                messageBuilder.Append("Seja muito bem - vindo! 😀\r\n\r\n");
                messageBuilder.Append($"Link do Aplicativo: {appLink} \U0001F4F1 \r\n\r\n");
                messageBuilder.Append("Atenciosamente,\r\nEquipe Ceasa Digital \U0001F600");

                return messageBuilder.ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }
        public async Task<string> CriarConteudoMensagemRecupearUsuario(string Contato1, string senha)
        {
            try
            {
                var messageBuilder = new StringBuilder();
                messageBuilder.Append($"Olá, {Contato1}.\r\n");
                messageBuilder.Append($"Segue sua senha de acesso: *{senha}*\r\n\r\n");
                messageBuilder.Append($"📱 Link do Aplicativo: {appLink}\r\n\r\n");
                messageBuilder.Append("Atenciosamente,\r\nEquipe Ceasa Digital \U0001F600");

                return messageBuilder.ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }
        public async Task<string> CriarConteudoMensagemEditarVenda(string Contato1, string Contato2, string Produto, bool MensagemEditar)
        {
            try
            {
                var messageBuilder = new StringBuilder();
                messageBuilder.Append($"Olá, {Contato1}.\r\n");
                messageBuilder.Append($"O nosso parceiro {Contato2}\r\n\r\n");
                messageBuilder.Append($"fez uma alteração na negociação de: *{Produto}*.\r\n\r\n");
                messageBuilder.Append($"Acesse o aplicativo e de continuidade ao processo de venda\r\n\r\n");
                messageBuilder.Append($"Link do Aplicativo: {appLink} \U0001F4F1 \r\n\r\n");
                messageBuilder.Append("Atenciosamente,\r\nEquipe Ceasa Digital \U0001F600");

                return messageBuilder.ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }
        #endregion
    }
}
