namespace Domain.Aggregates.Order;

public class OrderItem : BaseEntity<Guid>
{
    public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }
    public float UnitPrice { get; private set; }

    private OrderItem() { } // For ORMs

    private OrderItem(Guid id, Guid productId, int quantity, float unitPrice)
        : base(id)
    {
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }

    public static OrderItem Create(Guid productId, int quantity, float unitPrice) =>
        new OrderItem(Guid.NewGuid(), productId, quantity, unitPrice);

    public void UpdateQuantity(int quantity) => Quantity = quantity;

    public float TotalAmount() => Quantity * UnitPrice;
}
