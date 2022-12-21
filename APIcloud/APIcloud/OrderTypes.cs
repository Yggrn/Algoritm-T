using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace APIcloud
{
    internal class OrderTypes
    {
        public static string Get()
        {
            var url = "https://api-ru.iiko.services/api/1/deliveries/order_types";
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "POST";
            httpRequest.Headers["Authorization"] = "Bearer "+ TokenKey.bearertoken;
            httpRequest.ContentType = "application/json";
            var data = $"{{\"organizationIds\":[{JsonConvert.SerializeObject(Organizations.orgId[0].id)}]}}";

            using (StreamWriter streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
            {
                streamWriter.Write(data);
            }
            try
            {
                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                using var streamReader = new StreamReader(httpResponse.GetResponseStream());
                var result = streamReader.ReadToEnd();
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }
    }
}
