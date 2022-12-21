using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APIcloud
{
    internal class TokenKey
    {
        static public string bearertoken = string.Empty;
        #region Token
        public class Token
        {
            public Guid correlationId { get; set; }
            public string token { get; set; }
        }
        #endregion
        public static async Task Get(string token)
        {
            var url = "https://api-ru.iiko.services/api/1/access_token";
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "POST";
            httpRequest.Accept = "application/json";
            httpRequest.ContentType = "application/json";
            var data = $"{{\"apiLogin\": \"{token}\"}}";
          
                using (StreamWriter streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                {
                    streamWriter.WriteAsync(data);
                }
                try
                {
                    var httpResponse = (HttpWebResponse) httpRequest.GetResponse();
                    using var streamReader = new StreamReader(httpResponse.GetResponseStream());
                    var result = await streamReader.ReadToEndAsync();
                    Token tkn = JsonConvert.DeserializeObject<Token>(result);
                    bearertoken = tkn.token;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
           
        }
    }

