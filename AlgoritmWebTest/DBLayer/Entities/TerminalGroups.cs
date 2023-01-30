namespace DBLayer.Entities
{
    
        public class TerminaGroups
        {
            public Guid correlationId { get; set; }
            public List<Terminals>? terminalGroups { get; set; }
        }
        public class Terminals
        {
            public List<ItemsArray>? items { get; set; }
        }
        public class ItemsArray
        {
        public string address { get; set; } = string.Empty;
        public string name { get; set; } = string.Empty;
        public string timeZone { get; set; } = string.Empty;
        public string id { get; set; } = string.Empty;
        public string organizationId { get; set; } = string.Empty;
        }
   
}
