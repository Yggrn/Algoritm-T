namespace AlgoritmWeb.Models
{
    public class Organizations
    {
        static public List<OrganizationsArray>? orgId;
   
        public class Organizations_
        {
            public Guid correlationId { get; set; }
            public List<OrganizationsArray>? organizations { get; set; }
        }
        public class OrganizationsArray
        {
            public string? id { get; set; }
            public string? responseType { get; set; }

            public string? restaurantAddress { get; set; }

            public string? latitude { get; set; }

            public string? longitude { get; set; }

            public string? useUaeAddressingSystem { get; set; }

            public string? version { get; set; }

            public string? currencyIsoName { get; set; }

            public string    inn { get; set; }

            public bool isCloud { get; set; }
           
            public string? name { get; set; }
            public string? apiToken { get; set; }

        }
    }
}
