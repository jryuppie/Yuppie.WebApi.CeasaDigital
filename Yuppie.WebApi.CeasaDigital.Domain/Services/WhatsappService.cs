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
            _ProdutoRepository= produtoRepository;
            _usuarioRepository= usuarioRepository;
        }

        public async Task<ObjectResult> EnviarMensagem(int IdComprador, int IdVendedor, int IdProduto,  string prefixo = "55")
        {
            try
            {
                var produto = await _ProdutoRepository.BuscarProdutoPorId(IdProduto);
                var vendedor = await _usuarioRepository.BuscarUsuarioPorId(IdVendedor);
                var comprador = await _usuarioRepository.BuscarUsuarioPorId(IdComprador);
                var mensagem = await CriarConteudoMensagem(vendedor.nome, comprador.nome, produto.nome);
                if(mensagem != "")
                {
                    using var client = new HttpClient();
                    var request = new
                    {
                        phone = prefixo + vendedor.telefone,
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


        public async Task<string> CriarConteudoMensagem(string Vendedor, string Comprador, string Produto)
        {
            try
            {      var messageBuilder = new StringBuilder();
                messageBuilder.Append($"Olá, {Vendedor}.\r\n");
                messageBuilder.Append($"O nosso parceiro {Comprador} demonstrou interesse na sua oferta de: *{Produto}* :)\r\n\r\n");
                messageBuilder.Append("Acesse o aplicativo e dê continuidade ao processo de venda.\r\n\r\n");
                messageBuilder.Append($"Link do Aplicativo: {appLink}\r\n\r\n");
                messageBuilder.Append("Atenciosamente,\r\nEquipe Ceasa Digital");

                return messageBuilder.ToString();                 
            }
            catch (Exception)
            {
                return "";              
            }
        }
    }
}
