using System.Collections.Generic;

namespace APIcloud
{
    #region CreateOrder
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

    public class CreateOrderSettings
    {
        public int transportToFrontTimeout { get; set; }
    }

    public class Customer
    {
        public string id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string comment { get; set; }
        public string birthdate { get; set; }
        public string email { get; set; }
        public bool shouldReceivePromoActionsInfo { get; set; }
        public bool shouldReceiveOrderStatusNotifications { get; set; }
        public string gender { get; set; }
        public string type { get; set; }
    }

    public class Discount
    {
        public string type { get; set; }
    }

    public class DiscountsInfo
    {
        public Card card { get; set; }
        public List<Discount> discounts { get; set; }
    }

    public class Guests
    {
        public int count { get; set; }
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
        public int amount { get; set; }
        public string productSizeId { get; set; }
        public ComboInformation comboInformation { get; set; }
        public string comment { get; set; }
        public double price { get; set; }

        public class Order
        {
            public string id { get; set; }
            public string externalNumber { get; set; }
            public List<string> tableIds { get; set; }
            public Customer customer { get; set; }
            public string phone { get; set; }
            public int guestCount { get; set; }
            public Guests guests { get; set; }
            public string tabName { get; set; }
            public List<Item> items { get; set; }
            public List<Combo> combos { get; set; }
            public List<Payment> payments { get; set; }
            public List<Tip> tips { get; set; }
            public string sourceKey { get; set; }
            public DiscountsInfo discountsInfo { get; set; }
            public IikoCard5Info iikoCard5Info { get; set; }
            public string orderTypeId { get; set; }
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

        public class CreateOrder
        {
            public string organizationId { get; set; }
            public string terminalGroupId { get; set; }
            public Order order { get; set; }
            public CreateOrderSettings createOrderSettings { get; set; }
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
       
    }
    #endregion
}

