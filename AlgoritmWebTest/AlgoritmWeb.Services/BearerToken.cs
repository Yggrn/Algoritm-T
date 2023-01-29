using DBlayer.Entities;
using Newtonsoft.Json;
using System.Text;

namespace AlgoritmWeb.Services
{
    public static class BearerToken
    {
        static HttpClient httpClient = new HttpClient();
        
        public static string bearerToken = string.Empty;
        public static async Task GetToken(string apitoken = "1f9487e9-f30")
        {
            httpClient.DefaultRequestHeaders.UserAgent.TryParseAdd("MessageService/3.1");
            if (apitoken != null)
            {
                try
                {
                    string url = "https://api-ru.iiko.services/api/1/access_token";
                    var data = $"{{\"apiLogin\": \"{apitoken}\"}}";
                    var response = await httpClient.PostAsync(url, new StringContent(data, encoding: Encoding.UTF8, "application/json"));
                    response.EnsureSuccessStatusCode();
                    var result = await response.Content.ReadAsStringAsync();
                    TokenKey tkn = JsonConvert.DeserializeObject<TokenKey>(result);
                    bearerToken = tkn.token;

                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}
