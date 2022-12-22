using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;

namespace APIcloud
{
    class CreateDelivery
    {
        #region CreateDeliveryClass
        public class Address
        {

            public Street street { get; set; }
            public string index { get; set; }
            public string house { get; set; }
            public string building { get; set; }
            public string flat { get; set; }
            public string entrance { get; set; }
            public string floor { get; set; }
            public string doorphone { get; set; }
            public string regionId { get; set; }
        }

        public class Card
        {
            public string track { get; set; }
        }

        public class Combo
        {
            public string id { get; set; }
            public string name { get; set; }
            public int amount { get; set; }
            public int price { get; set; }
            public string sourceId { get; set; }
            public string programId { get; set; }
        }

        public class ComboInformation
        {
            public string comboId { get; set; }
            public string comboSourceId { get; set; }
            public string comboGroupId { get; set; }
        }

        public class Coordinates
        {
            public int latitude { get; set; }
            public int longitude { get; set; }
        }

        public class CreateOrderSettings
        {
            public int transportToFrontTimeout { get; set; }
        }

        public class Customer
        {
            public string id { get; set; }
            public string name { get; set; }
            public string type { get; set; }
        }

        public class DeliveryPoint
        {
            public Coordinates coordinates { get; set; }
            public Address address { get; set; }
            public string externalCartographyId { get; set; }
            public string comment { get; set; }
        }

        public class Discount
        {
            public string discountTypeId { get; set; }
            public double sum { get; set; }
            public string type { get; set; }
            public string selectivePositions { get; set; }
        }

        public class DiscountsInfo
        {
            public Card card { get; set; }
            public List<Discount> discounts { get; set; }
        }

        public class Guests
        {
            public int count { get; set; }
            public bool splitBetweenPersons { get; set; }
        }

        public class IikoCard5Info
        {
            public string coupon { get; set; }
            public List<string> applicableManualConditions { get; set; }
        }

        public class Item
        {
            public string productId { get; set; }
            public string type { get; set; }
            public double amount { get; set; }
            public string productSizeId { get; set; }
            public ComboInformation comboInformation { get; set; }
            public string comment { get; set; }
            public double price { get; set; }
        }

        public class Order
        {
            public string id { get; set; }
            public string externalNumber { get; set; }
            public string completeBefore { get; set; }
            public string phone { get; set; }
            public string orderTypeId { get; set; }
            public string orderServiceType { get; set; }
            public DeliveryPoint deliveryPoint { get; set; }
            public string comment { get; set; }
            public Customer customer { get; set; }
            public Guests guests { get; set; }
            public string marketingSourceId { get; set; }
            public string operatorId { get; set; }
            public List<Item> items { get; set; }
            public List<Combo> combos { get; set; }
            public List<Payment> payments { get; set; }
            public List<Tip> tips { get; set; }
            public string sourceKey { get; set; }
            public DiscountsInfo discountsInfo { get; set; }
            public IikoCard5Info iikoCard5Info { get; set; }
        }

        public class Payment
        {
            public string paymentTypeKind { get; set; }
            public int sum { get; set; }
            public string paymentTypeId { get; set; }
            public bool isProcessedExternally { get; set; }
            public PaymentAdditionalData paymentAdditionalData { get; set; }
            public bool isFiscalizedExternally { get; set; }
        }

        public class PaymentAdditionalData
        {
            public string type { get; set; }
        }

        public class DeliveryCreate
        {
            public string organizationId { get; set; }
            public string terminalGroupId { get; set; }
            public CreateOrderSettings createOrderSettings { get; set; }
            public Order order { get; set; }
        }

        public class Street
        {
            public string classifierId { get; set; }
            public string id { get; set; }
            public string name { get; set; }
            public string city { get; set; }
        }

        public class Tip
        {
            public string paymentTypeKind { get; set; }
            public string tipsTypeId { get; set; }
            public int sum { get; set; }
            public string paymentTypeId { get; set; }
            public bool isProcessedExternally { get; set; }
            public PaymentAdditionalData paymentAdditionalData { get; set; }
            public bool isFiscalizedExternally { get; set; }
        }
        #endregion
        public static string Create(string jsonstr)
        {
            var url = "https://api-ru.iiko.services/api/1/deliveries/create";
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "POST";
            httpRequest.Headers["Authorization"] = "Bearer " + TokenKey.bearertoken;
            httpRequest.ContentType = "application/json";
            using (StreamWriter streamWriter = new(httpRequest.GetRequestStream()))
            {
                streamWriter.Write(jsonstr);
            }
            try
            {
                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                using var streamReader = new StreamReader(httpResponse.GetResponseStream());
                var result = streamReader.ReadToEnd();
                var cid = JsonConvert.DeserializeObject<CommandStatus.Status>(result);
                return cid.correlationId;

            }
            catch (Exception ex)
            {

                return ex.Message;

            }

        }

    }
}
