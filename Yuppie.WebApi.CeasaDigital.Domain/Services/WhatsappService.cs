using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Yuppie.WebApi.CeasaDigital.Domain.Interfaces;
using Newtonsoft.Json;
using System.Text;

namespace Yuppie.WebApi.CeasaDigital.Domain.Services
{
    public class WhatsappService : IWhatsappService
    {
        private readonly IMapper _mapper;
        public WhatsappService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<ObjectResult> EnviarMensagem(string destinatario, string mensagem)
        {
            try
            {
                using var client = new HttpClient();
                var request = new
                {
                    phone = destinatario,
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
            catch (Exception)
            {
                return new ObjectResult(new { message = "Falha ao enviar a mensagem!" })
                {
                    StatusCode = 500
                };
            }
        }
    }
}
