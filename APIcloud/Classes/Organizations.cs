using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace APIcloud
{
    internal class Organizations
    {
        static public List<OrganizationsArray> orgId;
        #region GetOrganization
        public class Organizations_
        {
            public Guid correlationId { get; set; }

            [JsonProperty("organizations")]
            public List<OrganizationsArray> organizations { get; set; }
        }
        public class OrganizationsArray
        {

            public string responseType { get; set; }

            public string restaurantAddress { get; set; }

            public string latitude { get; set; }

            public string longitude { get; set; }

            public string useUaeAddressingSystem { get; set; }

            public string version { get; set; }

            public string currencyIsoName { get; set; }

            public string inn { get; set; }

            public bool isCloud { get; set; }

            public string id { get; set; }

            public string name { get; set; }

        }
        #endregion
        public static string Get()
        {

            var url = "https://api-ru.iiko.services/api/1/organizations";
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "POST";
            httpRequest.Headers["Authorization"] = "Bearer " + TokenKey.bearertoken;
            httpRequest.ContentType = "application/json";

            var data = @"{""organizationIds"": null,
                          ""returnAdditionalInfo"": true,
                          ""includeDisabled"": false}";
            using (StreamWriter streamWriter = new(httpRequest.GetRequestStream()))
            {
                streamWriter.Write(data);
            }
            try
            {
                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                using var streamReader = new StreamReader(httpResponse.GetResponseStream());
                var result = streamReader.ReadToEnd();
                Organizations_ org = JsonConvert.DeserializeObject<Organizations_>(result);
                for (int i = 0; i < org.organizations.Count; i++)
                {
                    orgId = org.organizations;
                }
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
