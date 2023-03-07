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

namespace Yuppie.WebApi.CeasaDigital.Domain.Services
{
    public class WhatsappService : IWhatsappService
    {
        private readonly IMapper _mapper;
        private readonly IProdutoRepository _ProdutoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private static string appLink = "https://www.yuppie.in/";
        private static string telefonePrefixo = "55";
        public WhatsappService(IMapper mapper, IProdutoRepository produtoRepository, IUsuarioRepository usuarioRepository)
        {
            _mapper = mapper;
            _ProdutoRepository = produtoRepository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<ObjectResult> EnviarMensagem(MensagemModel model)
        {
            try
            {
                //TODO -  CRIAR ESTRUTURA PARA IDENTIFICAR QUAL O TIPO DE CONTEÚDO DA MENSAGEM.               
                //NEGOCIACAO - NOME COMPRADOR E VENDEDOR E NOME PRODUTO.
                //CRIAR USUÁRIO E RECUPERA SENHA - NOME USUARIO E SENHA
                //VENDA - NOME VENDEDOR E COMPRADOR , NOME PRODUTO


                var produto = await _ProdutoRepository.BuscarProdutoPorId(model.IdProduto);
                var vendedor = await _usuarioRepository.BuscarUsuarioPorId(model.IdVendedor);
                var comprador = await _usuarioRepository.BuscarUsuarioPorId(model.IdComprador);

                string contato1 = vendedor.nome;
                string contato2 = comprador.nome;
                string telefone = vendedor.telefone;
                var mensagem = "";
                if (model.EnvioComprador)
                {
                    contato1 = comprador.nome;
                    contato2 = vendedor.nome;
                    telefone = comprador.telefone;
                }
                if (model.TipoMensagem.ToUpper() == "OFERTA")
                    mensagem = await CriarConteudoMensagemOferta(contato1, contato2, produto.nome);
                else if (model.TipoMensagem.ToUpper() == "NEGOCIACAO")
                {
                    bool tipoEnvio = model.EnvioComprador ? true : false;
                    mensagem = await CriarConteudoMensagemNegociacao(contato1, contato2, telefone, tipoEnvio);
                }

                if (mensagem != "")
                {
                    using var client = new HttpClient();
                    var request = new
                    {
                        phone = model.prefixo + telefone,
                        message = mensagem
                    };
                    var requestContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync("http://192.168.2.222:3000/whatsapp/send-text", requestContent);
                    response.EnsureSuccessStatusCode();
                    var responseString = await response.Content.ReadAsStringAsync();
                    return new ObjectResult(new { message = "Mensagem enviada com sucesso!" })
                    {
                        StatusCode = StatusCodes.Status200OK
                    };
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
        public async Task<ObjectResult> EnviarMensagemUsuario(int IdUsuario, bool RecuperarSenha)
        {
            try
            {
                string mensagem = "";
                var usuario = await _usuarioRepository.BuscarUsuarioPorId(IdUsuario);
                if (usuario != null)
                {
                    mensagem = RecuperarSenha ? await CriarConteudoMensagemRecupearUsuario(usuario.nome, usuario.senha)
                                              : await CriarConteudoMensagemCriarUsuario(usuario.nome);
                    if (mensagem != "")
                    {
                        using var client = new HttpClient();
                        var request = new
                        {
                            phone = "+55" + usuario.telefone,
                            message = mensagem
                        };
                        var requestContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                        var response = await client.PostAsync("http://192.168.2.222:3000/whatsapp/send-text", requestContent);
                        response.EnsureSuccessStatusCode();
                        var responseString = await response.Content.ReadAsStringAsync();
                        return new ObjectResult(new { message = "Mensagem enviada com sucesso!" })
                        {
                            StatusCode = StatusCodes.Status200OK
                        };
                    }
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
                var messageBuilder = new StringBuilder();
                messageBuilder.Append($"Olá, {Contato1}.\r\n");
                messageBuilder.Append($"seu processo negocial com {Contato2}\r\n\r\n");
                messageBuilder.Append($"relacionado a {tipo} de: *{Produto}.\r\n\r\n");
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

    }
}
