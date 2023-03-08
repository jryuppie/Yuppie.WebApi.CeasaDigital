using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Yuppie.WebApi.CeasaDigital.Domain.Interfaces;

namespace Yuppie.WebApi.CeasaDigital.Domain.Services
{
    public class LoginService : ILoginService
    {
        private readonly IHttpClientFactory _clientFactory;
        public LoginService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<string> Login(string Usuario, string Senha)
        {
            try
            {
                var client = _clientFactory.CreateClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:8080/oauth/token");
                request.Headers.Add("Authorization", "Basic OTk1MjdiMTgtMjljMi00NTk4LTk0YTYtZDQ2MGU2MDZhYmYwOjhkMWUyMjk4LTRkYTgtNDFkNy05MmJhLThiOTlhMzE1MmUxOQ==");

                var formData = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("username", Usuario),
                        new KeyValuePair<string, string>("password", Senha),
                        new KeyValuePair<string, string>("grant_type", "password")
                    };

                request.Content = new FormUrlEncodedContent(formData);
                var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();
                return responseContent;
            }
            catch (System.Exception ex)
            {
                var esMsg = ex.Message;
                return esMsg.ToString();
            }
         
        }

    }
}
