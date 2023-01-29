using DBlayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AlgoritmWeb.Controllers
{
    public class TokenController : Controller
    {

        public static string BearerToken = string.Empty;
        private HttpClient httpClient;
        public TokenController()
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.UserAgent.TryParseAdd("MessageService/3.1");
        }
        public async Task<IActionResult> AddTokenAsync()
        {
            await GetToken();
            return View();
        }
        async Task GetToken(string apitoken = "1f9487e9-f30")
        {
            if (apitoken != null)
            {
                try
                {
                    string url = "https://api-ru.iiko.services/api/1/access_token";
                    var data = $"{{\"apiLogin\": \"{apitoken}\"}}";
                    var response = await httpClient.PostAsync(url, new StringContent(data, encoding: System.Text.Encoding.UTF8, "application/json"));
                    response.EnsureSuccessStatusCode();
                    var result = await response.Content.ReadAsStringAsync();
                    TokenKey tkn = JsonConvert.DeserializeObject<TokenKey>(result);
                    BearerToken = tkn.token;
                    
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}
