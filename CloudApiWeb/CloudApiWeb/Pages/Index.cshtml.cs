using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using Newtonsoft.Json;

namespace CloudApiWeb.Pages
{
    public class IndexModel : PageModel
    {
        public string apiKey;

        private readonly ILogger<IndexModel> _logger;        

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            apiKey = "1f9487e9-f30";
        }
        static int i = 1;
        static public int GG(int x, int y)
        {
            ;
            i++;
            return x+y+i;
        }
        public static string Get(string token)
        {
            var url = "https://api-ru.iiko.services/api/1/access_token";
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "POST";
            httpRequest.Accept = "application/json";
            httpRequest.ContentType = "application/json";
            var data = $"{{\"apiLogin\": \"{token}\"}}";

            using (StreamWriter streamWriter = new(httpRequest.GetRequestStream()))
            {
                streamWriter.WriteAsync(data);
            }
            try
            {
                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                using var streamReader = new StreamReader(httpResponse.GetResponseStream());
                var result = streamReader.ReadToEndAsync();
                Token tkn = JsonConvert.DeserializeObject<Token>(result.Result);
                return tkn.token;
            }
            catch (Exception ex)
            {
              return ex.Message;
            }
        }

       
            static public string bearertoken = string.Empty;

            public class Token
            {
                public Guid correlationId { get; set; }
                public string token { get; set; }
            }        

    }
}
       
    
