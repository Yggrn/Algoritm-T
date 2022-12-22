using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace APIcloud
{
    class Discounts
    {
        #region Discount
        public class Discount
        {
            public string organizationId { get; set; }
            public List<Item> items { get; set; }
        }

        public class Item
        {
            public string id { get; set; }
            public string name { get; set; }
            public double percent { get; set; }
            public bool isCategorisedDiscount { get; set; }
            public List<ProductCategoryDiscount> productCategoryDiscounts { get; set; }
            public string comment { get; set; }
            public bool canBeAppliedSelectively { get; set; }
            public double? minOrderSum { get; set; }
            public string mode { get; set; }
            public double sum { get; set; }
            public bool canApplyByCardNumber { get; set; }
            public bool isManual { get; set; }
            public bool isCard { get; set; }
            public bool isAutomatic { get; set; }
            public bool isDeleted { get; set; }
        }

        public class ProductCategoryDiscount
        {
            public string categoryId { get; set; }
            public string categoryName { get; set; }
            public double percent { get; set; }
        }

        public class Root
        {
            public string correlationId { get; set; }
            public List<Discount> discounts { get; set; }
        }

        #endregion

        public static string Get()
        {
            var url = "https://api-ru.iiko.services/api/1/discounts";
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "POST";
            httpRequest.Headers["Authorization"] = "Bearer " + TokenKey.bearertoken;
            httpRequest.ContentType = "application/json";
            var data = $"{{\"organizationIds\":[{JsonConvert.SerializeObject(Organizations.orgId[0].id)}]}}";
            using (StreamWriter streamWriter = new(httpRequest.GetRequestStream()))
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
