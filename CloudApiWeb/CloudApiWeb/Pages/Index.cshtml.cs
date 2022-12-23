﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using Newtonsoft.Json;

namespace CloudApiWeb.Pages
{
    public class IndexModel : PageModel
    {
       
        static public string bearertoken = null;
        public static string apiKey = "1f9487e9-f30";

        private readonly ILogger<IndexModel> _logger;        

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
                       
        }
         
        
        public void Submit()
        {           
           
        }
        public void OnPostSubmit()
        {
            bearertoken= Get("1f9487e9-f30");
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
                bearertoken = tkn.token;
                return tkn.token;
            }
            catch (Exception ex)
            {
              return ex.Message;
            }
        }


        

            public class Token
            {
                public Guid correlationId { get; set; }
                public string? token { get; set; }
            }        

    }
}
       
    
