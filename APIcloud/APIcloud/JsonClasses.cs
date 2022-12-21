using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace APIcloud
{
    internal static class JsonClasses
    {
        public class Token
        {
            public Guid correlationId { get; set; }
            public string token { get; set; }
        }
        public class Organizations_
        {
            public Guid correlationId { get; set; }

            [JsonProperty("organizations")]
            public List<OrganizationsArray> organizations { get; set; }
        }
        public class OrganizationsArray
        {
            [JsonProperty("responseType")]
            public string responseType { get; set; }
            [JsonProperty("restaurantAddress")]
            public string restaurantAddress { get; set; }
            [JsonProperty("latitude")]
            public string latitude { get; set; }
            [JsonProperty("longitude")]
            public string longitude { get; set; }
            [JsonProperty("useUaeAddressingSystem")]
            public string useUaeAddressingSystem { get; set; }
            [JsonProperty("version")]
            public string version { get; set; }
            [JsonProperty("currencyIsoName")]
            public string currencyIsoName { get; set; }
            [JsonProperty("inn")]
            public string inn { get; set; }
            [JsonProperty("isCloud")]
            public bool isCloud { get; set; }
            [JsonProperty("id")]
            public Guid id { get; set; }
            [JsonProperty("name")]
            public string name { get; set; }

        }
        public class TerminaGroups_
        {
            public Guid correlationId { get; set; }

            [JsonProperty("terminalGroups")]
            public List<TerminalGroups> terminalGroups { get; set; }
        }

        public class TerminalGroups
        {
            [JsonProperty("items")]
            public List<ItemsArray> items { get; set; }
        }
        public class ItemsArray
        {

            [JsonProperty("address")]
            public string address { get; set; }
            [JsonProperty("name")]
            public string name { get; set; }
            [JsonProperty("timeZone")]
            public string timeZone { get; set; }
            [JsonProperty("id")]
            public Guid id { get; set; }
            [JsonProperty("organizationId")]
            public Guid organizationId { get; set; }

        }
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class ChildModifier
        {
            public string id { get; set; }
            public int defaultAmount { get; set; }
            public int minAmount { get; set; }
            public int maxAmount { get; set; }
            public bool required { get; set; }
            public bool hideIfDefaultAmount { get; set; }
            public bool splittable { get; set; }
            public int freeOfChargeAmount { get; set; }
        }

        public class Group
        {
            public List<string> imageLinks { get; set; }
            public string parentGroup { get; set; }
            public int order { get; set; }
            public bool isIncludedInMenu { get; set; }
            public bool isGroupModifier { get; set; }
            public string id { get; set; }
            public string code { get; set; }
            public string name { get; set; }
            public string description { get; set; }
            public string additionalInfo { get; set; }
            public List<string> tags { get; set; }
            public bool isDeleted { get; set; }
            public string seoDescription { get; set; }
            public string seoText { get; set; }
            public string seoKeywords { get; set; }
            public string seoTitle { get; set; }
        }

        public class GroupModifier
        {
            public string id { get; set; }
            public int minAmount { get; set; }
            public int maxAmount { get; set; }
            public bool required { get; set; }
            public bool childModifiersHaveMinMaxRestrictions { get; set; }
            public List<ChildModifier> childModifiers { get; set; }
            public bool hideIfDefaultAmount { get; set; }
            public int defaultAmount { get; set; }
            public bool splittable { get; set; }
            public int freeOfChargeAmount { get; set; }
        }

        public class Modifier
        {
            public string id { get; set; }
            public int defaultAmount { get; set; }
            public int minAmount { get; set; }
            public int maxAmount { get; set; }
            public bool required { get; set; }
            public bool hideIfDefaultAmount { get; set; }
            public bool splittable { get; set; }
            public int freeOfChargeAmount { get; set; }
        }

        public class Price
        {
            public object currentPrice { get; set; }
            public bool isIncludedInMenu { get; set; }
            public object nextPrice { get; set; }
            public bool nextIncludedInMenu { get; set; }
            public string nextDatePrice { get; set; }
        }

        public class Product
        {
            public double fatAmount { get; set; }
            public double proteinsAmount { get; set; }
            public double carbohydratesAmount { get; set; }
            public double energyAmount { get; set; }
            public double fatFullAmount { get; set; }
            public double proteinsFullAmount { get; set; }
            public double carbohydratesFullAmount { get; set; }
            public double energyFullAmount { get; set; }
            public double weight { get; set; }
            public string groupId { get; set; }
            public string productCategoryId { get; set; }
            public string type { get; set; }
            public string orderItemType { get; set; }
            public string modifierSchemaId { get; set; }
            public string modifierSchemaName { get; set; }
            public bool splittable { get; set; }
            public string measureUnit { get; set; }
            public List<SizePrice> sizePrices { get; set; }
            public List<Modifier> modifiers { get; set; }
            public List<GroupModifier> groupModifiers { get; set; }
            public List<string> imageLinks { get; set; }
            public bool doNotPrintInCheque { get; set; }
            public string parentGroup { get; set; }
            public int order { get; set; }
            public string fullNameEnglish { get; set; }
            public bool useBalanceForSell { get; set; }
            public bool canSetOpenPrice { get; set; }
            public string id { get; set; }
            public string code { get; set; }
            public string name { get; set; }
            public string description { get; set; }
            public string additionalInfo { get; set; }
            public List<string> tags { get; set; }
            public bool isDeleted { get; set; }
            public string seoDescription { get; set; }
            public string seoText { get; set; }
            public string seoKeywords { get; set; }
            public string seoTitle { get; set; }
        }

        public class ProductCategory
        {
            public string id { get; set; }
            public string name { get; set; }
            public bool isDeleted { get; set; }
        }

        public class Root
        {
            public string correlationId { get; set; }
            public List<Group> groups { get; set; }
            public List<ProductCategory> productCategories { get; set; }
            public List<Product> products { get; set; }
            public List<Size> sizes { get; set; }
            public Int64 revision { get; set; }
        }

        public class Size
        {
            public string id { get; set; }
            public string name { get; set; }
            public int priority { get; set; }
            public bool isDefault { get; set; }
        }

        public class SizePrice
        {
            public string sizeId { get; set; }
            public Price price { get; set; }
        }


    }
}
