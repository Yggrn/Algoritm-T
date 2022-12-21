using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace APIcloud
{
    internal class TerminalGroups
    {
        #region TerminalGroups
        public class TerminaGroups_
        {
            public Guid correlationId { get; set; }


            public List<TerminalGroup> terminalGroups { get; set; }
        }
        public class TerminalGroup
        {

            public List<ItemsArray> items { get; set; }
        }
        public class ItemsArray
        {


            public string address { get; set; }

            public string name { get; set; }

            public string timeZone { get; set; }

            public Guid id { get; set; }

            public Guid organizationId { get; set; }

        }
        #endregion
        public static string Get()
        {
            var url = "https://api-ru.iiko.services/api/1/terminal_groups";
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "POST";
            httpRequest.Headers["Authorization"] = "Bearer "+ TokenKey.bearertoken;
            httpRequest.ContentType = "application/json";
            var data = $"{{\"organizationIds\":[{JsonConvert.SerializeObject(Organizations.orgId[0].id)}], \"includeDisabled\": true}}";

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
