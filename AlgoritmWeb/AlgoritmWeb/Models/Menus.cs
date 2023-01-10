using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlgoritmWeb.Models
{
    public class Menus
    {
        public class ChildModifier
        {
            [Key] public Guid ChildModifierNumber { get; set; }
            public string? id { get; set; }
            public int defaultAmount { get; set; }
            public int minAmount { get; set; }
            public int maxAmount { get; set; }
            public bool required { get; set; }
            public bool hideIfDefaultAmount { get; set; }
            public bool splittable { get; set; }
            public int freeOfChargeAmount { get; set; }
        }
        public class Tags
        {
            [Key] public Guid tagNumber { get; set; }
            public string? tags { get; set; }

        }
        public class Group
        {
            public string? id { get; set; }
            public string? code { get; set; }
            public string? name { get; set; }
            public bool isDeleted { get; set; }
            [Key] public Guid groupNumber { get; set; }
            public string? parentGroup { get; set; }
            public bool isIncludedInMenu { get; set; }
            public bool isGroupModifier { get; set; }
            public string? organizationId { get; set; }

        }
        public class GroupModifier
        {
            [Key] public Guid groupModifierNumber { get; set; }
            public string? id { get; set; }
            public int minAmount { get; set; }
            public int maxAmount { get; set; }
            public bool required { get; set; }
            public bool childModifiersHaveMinMaxRestrictions { get; set; }
            public List<ChildModifier>? childModifiers { get; set; }
            public bool hideIfDefaultAmount { get; set; }
            public int defaultAmount { get; set; }
            public bool splittable { get; set; }
            public int freeOfChargeAmount { get; set; }
        }
        public class Modifier
        {
            [Key] public Guid modifierNumber { get; set; }
            public string? id { get; set; }
            public int defaultAmount { get; set; }
            public int minAmount { get; set; }
            public int maxAmount { get; set; }
            public bool required { get; set; }
            public bool hideIfDefaultAmount { get; set; }
            public bool splittable { get; set; }
            public int freeOfChargeAmount { get; set; }
        }

        public class SizePrice
        {
            [Key] public Guid sizePriceNumber { get; set; }
            public Price? price { get; set; }
        }
        public class Price
        {
            [Key] public Guid? priceNumber { get; set; }
            public double currentPrice { get; set; }
            public bool isIncludedInMenu { get; set; }
        }

        public class Product
        {
            [Key] public Guid productNumber { get; set; }
            public string? id { get; set; }
            public string? code { get; set; }
            public string? name { get; set; }
            public double? price { get; set; }
            public string? description { get; set; }
            public bool isDeleted { get; set; }
            public string? groupId { get; set; }
            public string? productCategoryId { get; set; }
            public string type { get; set; }
            public string? orderItemType { get; set; }
            public string? measureUnit { get; set; }
            public List<SizePrice>? sizePrices { get; set; }
            public string? parentGroup { get; set; }
            public bool canSetOpenPrice { get; set; }
            public string? organizationId { get; set; }
        }
        public class ProductCategory
        {
            [Key] public Guid productCategoryNumber { get; set; }
            public string? id { get; set; }
            public string? name { get; set; }
            public bool isDeleted { get; set; }
        }

        public class Root
        {
            public Guid id { get; set; } = new Guid();
            public string? correlationId { get; set; }
            public List<Group>? groups { get; set; }
            public List<ProductCategory>? productCategories { get; set; }
            public List<Product>? products { get; set; }
            public long revision { get; set; }
        }

        public class ItemOrder
        {
            [Key] public Guid itemOrderNumber { get; set; }
            public string? id { get; set; }
            public string? name { get; set; }
            public string? orderServiceType { get; set; }
            public bool isDeleted { get; set; }
            public int externalRevision { get; set; }
        }
        public class OrderType
        {
            [Key] public Guid orderTypeNumber { get; set; }
            public string? organizationId { get; set; }
            public List<ItemOrder>? items { get; set; }
        }
        public class RootOrderTypes
        {
            [Key] public Guid rootOrderTypeNumber { get; set; }
            public string? correlationId { get; set; }
            public List<OrderType>? orderTypes { get; set; }
        }
    }
}
