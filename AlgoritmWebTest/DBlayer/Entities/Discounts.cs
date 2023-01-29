namespace DBlayer.Entities

{
    public class Discounts
    {
        public class Discount
        {
            public string organizationId { get; set; }
            public List<Item> items { get; set; }
        }

        public class Item
        {
            public Guid id { get; set; }
            public string name { get; set; }
            public double percent { get; set; }
            public bool isCategorisedDiscount { get; set; }
         //   public List<ProductCategoryDiscount> productCategoryDiscounts { get; set; }
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
            public string OID { get; set; }
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
    }
}
