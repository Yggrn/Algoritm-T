using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace APIcloud.Classes
{
    internal class LoyaltySystem
    {
        #region LoyaltyClasses  
        public class Action
        {
            public string id { get; set; }
            public string settings { get; set; }
            public string typeName { get; set; }
            public string checkSum { get; set; }
        }

        public class Condition
        {
            public string id { get; set; }
            public string settings { get; set; }
            public string typeName { get; set; }
            public string checkSum { get; set; }
        }

        public class GuestRegistrationActionConditionBinding
        {
            public string id { get; set; }
            public bool stopFurtherExecution { get; set; }
            public List<Action> actions { get; set; }
            public List<Condition> conditions { get; set; }
        }

        public class MarketingCampaign
        {
            public string id { get; set; }
            public string programId { get; set; }
            public string name { get; set; }
            public string description { get; set; }
            public bool isActive { get; set; }
            public string periodFrom { get; set; }
            public string periodTo { get; set; }
            public List<OrderActionConditionBinding> orderActionConditionBindings { get; set; }
            public List<PeriodicActionConditionBinding> periodicActionConditionBindings { get; set; }
            public List<OverdraftActionConditionBinding> overdraftActionConditionBindings { get; set; }
            public List<GuestRegistrationActionConditionBinding> guestRegistrationActionConditionBindings { get; set; }
        }

        public class OrderActionConditionBinding
        {
            public string id { get; set; }
            public bool stopFurtherExecution { get; set; }
            public List<Action> actions { get; set; }
            public List<Condition> conditions { get; set; }
        }

        public class OverdraftActionConditionBinding
        {
            public string id { get; set; }
            public bool stopFurtherExecution { get; set; }
            public List<Action> actions { get; set; }
            public List<Condition> conditions { get; set; }
        }

        public class PeriodicActionConditionBinding
        {
            public string id { get; set; }
            public bool stopFurtherExecution { get; set; }
            public List<Action> actions { get; set; }
            public List<Condition> conditions { get; set; }
        }

        public class Program
        {
            public string id { get; set; }
            public string name { get; set; }
            public string description { get; set; }
            public string serviceFrom { get; set; }
            public string serviceTo { get; set; }
            public bool notifyAboutBalanceChanges { get; set; }
            public int programType { get; set; }
            public bool isActive { get; set; }
            public string walletId { get; set; }
            public List<MarketingCampaign> marketingCampaigns { get; set; }
            public List<string> appliedOrganizations { get; set; }
            public int templateType { get; set; }
            public bool hasWelcomeBonus { get; set; }
            public double welcomeBonusSum { get; set; }
            public bool isExchangeRateEnabled { get; set; }
            public int refillType { get; set; }
        }

        public class LoyaltyProgamm
        {
            public List<Program> Programs { get; set; }
        }
        #endregion
        public static string Get()
        {
            var url = "https://api-ru.iiko.services/api/1/loyalty/iiko/program";
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "POST";
            httpRequest.Headers["Authorization"] = "Bearer " + TokenKey.bearertoken;
            httpRequest.ContentType = "application/json";
            var data = $"{{\"organizationId\":{JsonConvert.SerializeObject(Organizations.orgId[0].id)}, \"WithoutMarketingCampaigns\": \"false\"}}";
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
                return ex.Message + Environment.NewLine;
            }
        }
    }
}
