using Domain.Abstractions;
using Domain.Aggregates.Common;

namespace Domain.Aggregates.Order;

public class Order : BaseEntity<Guid>, IAggregateRoot
{
    public Guid CustomerId { get; private set; }
    public List<OrderItem> Items { get; private set; }
    public LocationPoint? ClientLocation { get; private set; }
    public OrderStatus Status { get; private set; }
    public Guid? PaymentId { get; private set; }
    public Guid? RiderId { get; private set; }

    private Order(
        Guid id,
        Guid customerId,
        List<OrderItem> items,
        LocationPoint? clientLocation,
        OrderStatus status,
        Guid? paymentId,
        Guid? riderId
    )
        : base(id)
    {
        CustomerId = customerId;
        Items = items;
        ClientLocation = clientLocation;
        Status = status;
        PaymentId = paymentId;
        RiderId = riderId;
    }

    public static Order Create(Guid customerId)
    {
        return new(
            id: Guid.NewGuid(),
            customerId: customerId,
            items: [],
            clientLocation: null,
            status: OrderStatus.CustomerPending,
            paymentId: null,
            riderId: null
        );
    }

    public void AddItem(Guid product, int quantity, float unitPrice)
    {
        var existingItem = Items.Find(i => i.ProductId == product);
        if (existingItem != null)
        {
            existingItem.UpdateQuantity(existingItem.Quantity + quantity);
        }
        else
        {
            var newItem = OrderItem.Create(product, quantity, unitPrice);
            Items.Add(newItem);
        }
    }

    public void RemoveItem(Guid itemId)
    {
        Items.RemoveAll(i => i.Id == itemId);
    }

    public void UpdateItemQuantity(Guid itemId, int quantity)
    {
        var item = Items.Find(i => i.Id == itemId);
        if (item != null)
        {
            item.UpdateQuantity(quantity);
        }
    }

    public void SetClientLocation(LocationPoint location)
    {
        ClientLocation = location;
    }

    public void CustomerConfirm()
    {
        if (Status != OrderStatus.CustomerPending)
            throw new InvalidOperationException();
        Status = OrderStatus.CustomerConfirmed;
    }

    public void MarkAsPaid(Guid paymentId)
    {
        if (Status != OrderStatus.CustomerConfirmed)
            throw new InvalidOperationException();
        Status = OrderStatus.CustomerPaid;
        PaymentId = paymentId;
    }

    public float TotalAmount() => Items.Sum(item => item.TotalAmount());

    public void MarkAsReadyForPickup()
    {
        if (Status != OrderStatus.InPreparation)
            throw new InvalidOperationException();
        Status = OrderStatus.ReadyForPickup;
    }

    public void AssignRider(Guid riderId)
    {
        if (Status != OrderStatus.InPreparation)
            throw new InvalidOperationException();
        RiderId = riderId;
        Status = OrderStatus.WaitingForRider;
    }

    public void MarkOnTheWay()
    {
        if (Status != OrderStatus.WaitingForRider)
            throw new InvalidOperationException();
        Status = OrderStatus.OnTheWay;
    }

    public void MarkAtCustomerLocation()
    {
        if (Status != OrderStatus.OnTheWay)
            throw new InvalidOperationException();
        Status = OrderStatus.AtCustomerLocation;
    }

    public void CompleteOrder()
    {
        if (Status != OrderStatus.AtCustomerLocation)
            throw new InvalidOperationException();
        Status = OrderStatus.Completed;
    }

    public void CancelOrder()
    {
        if (Status == OrderStatus.Completed || Status == OrderStatus.Cancelled)
            throw new InvalidOperationException();
        Status = OrderStatus.Cancelled;
    }
}
