using Domain.Abstractions;
using Domain.Aggregates.Common;

namespace Domain.Aggregates.Customer;

public class Customer : BaseEntity<Guid>, IAggregateRoot
{
    public string? Name { get; private set; }
    public PhoneNumber? PhoneNumber { get; private set; }
    public TelegramId? TelegramId { get; private set; }
    public IReadOnlyCollection<Address> Addresses => _addresses.AsReadOnly();

    private readonly List<Address> _addresses = new();

    private Customer(
        Guid id,
        string? name,
        PhoneNumber? phoneNumber,
        TelegramId? telegramId,
        List<Address> addresses
    )
        : base(id)
    {
        Name = name;
        PhoneNumber = phoneNumber;
        TelegramId = telegramId;
        _addresses = addresses;
    }

    public static Customer CreateFromTelegram(TelegramId telegramId)
    {
        return new Customer(
            id: Guid.NewGuid(),
            name: null,
            phoneNumber: null,
            telegramId: telegramId,
            addresses: []
        );
    }

    public void UpdateName(string name)
    {
        Name = name;
    }

    public void UpdatePhoneNumber(PhoneNumber phoneNumber)
    {
        PhoneNumber = phoneNumber;
    }

    public void UpdateTelegramId(TelegramId telegramId)
    {
        TelegramId = telegramId;
    }

    public void AddAddress(LocationPoint location, string name)
    {
        var address = Address.Create(location, name);
        _addresses.Add(address);
    }

    public void UpdateAddress(Address address)
    {
        var existing = _addresses.FirstOrDefault(a => a.Id == address.Id);
        if (existing != null)
        {
            existing.UpdateName(address.Name);
            existing.UpdateLocation(address.Location);
        }
    }

    public void RemoveAddress(Guid addressId)
    {
        _addresses.RemoveAll(a => a.Id == addressId);
    }
}
