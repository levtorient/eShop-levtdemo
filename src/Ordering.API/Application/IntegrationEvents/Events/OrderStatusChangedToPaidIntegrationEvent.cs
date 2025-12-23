namespace eShop.Ordering.API.Application.IntegrationEvents.Events;

public record OrderStatusChangedToPaidIntegrationEvent : IntegrationEvent
{
    public int OrderId { get; }
    public OrderStatus OrderStatus { get; }
    public string BuyerName { get; }
    public string BuyerIdentityGuid { get; }
    public IEnumerable<OrderStockItem> OrderStockItems { get; }
    public string Street { get; }
    public string City { get; }
    public string ZipCode { get; }
    public string State { get; }
    public string Country { get; }

    public OrderStatusChangedToPaidIntegrationEvent(int orderId,
        OrderStatus orderStatus, string buyerName, string buyerIdentityGuid,
        IEnumerable<OrderStockItem> orderStockItems,
        string street, string city, string zipCode, string state, string country)
    {
        OrderId = orderId;
        OrderStockItems = orderStockItems;
        OrderStatus = orderStatus;
        BuyerName = buyerName;
        BuyerIdentityGuid = buyerIdentityGuid;
        Street = street;
        City = city;
        ZipCode = zipCode;
        State = state;
        Country = country;
    }
}

