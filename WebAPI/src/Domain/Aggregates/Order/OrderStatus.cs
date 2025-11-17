namespace Domain.Aggregates.Order;

public enum OrderStatus
{
    CustomerPending,
    CustomerConfirmed,
    CustomerPaid,
    InPreparation,
    ReadyForPickup,
    WaitingForRider,
    OnTheWay,
    AtCustomerLocation,
    Completed,
    Cancelled,
}
