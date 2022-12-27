using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace APIcloud
{
    internal class CommandStatus
    {
        #region CommandClasses
        public class Status
        {
            public string organizationId { get; set; }
            public string correlationId { get; set; }
            public int timeout { get; set; }
        }

        public class Except
        {
            public string orderId { get; set; }
            public TerminalGroup terminalGroup { get; set; }
            public long timestamp { get; set; }
            public int code { get; set; }
            public string message { get; set; }
            public string description { get; set; }
            public object additionalData { get; set; }
        }

        public class Request
        {
            public string state { get; set; }
            public Except exception { get; set; }
        }

        public class TerminalGroup
        {
            public string id { get; set; }
            public string name { get; set; }
        }
        #endregion
        public static async Task<string> Get(string correlationId)
        {
            var url = "https://api-ru.iiko.services/api/1/commands/status";
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "POST";
            httpRequest.Headers["Authorization"] = "Bearer " + TokenKey.bearertoken;
            httpRequest.ContentType = "application/json";
            var data = $"{{\"organizationId\":{JsonConvert.SerializeObject(Organizations.orgId[0].id)}, \"correlationId\": \"{correlationId}\"}}";
            using (StreamWriter streamWriter = new(httpRequest.GetRequestStream()))
            {
                streamWriter.Write(data);
                Thread.Sleep(10000);
            }
            try
            {
                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                using var streamReader = new StreamReader(httpResponse.GetResponseStream());
                var result = streamReader.ReadToEnd();
                var Info = JsonConvert.DeserializeObject<Request>(result);
                if (Info.state == "Success" || Info.state == "InProgress") return $"{DateTime.Now} {Info.state} OK! {Environment.NewLine}";
                else return $"{DateTime.Now} {Info.state.ToUpper()} {Environment.NewLine} {Info.exception.description}{Environment.NewLine}";
            }
            catch (Exception ex)
            {
                return ex.Message + Environment.NewLine;
            }
        }
    }
}
