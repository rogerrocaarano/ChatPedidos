using Domain.Aggregates.Common;

namespace Domain.Aggregates.Customer;

public class Customer : BaseEntity<Guid>, IAggregateRoot
{
    public string Name { get; private set; }
    public PhoneNumber? PhoneNumber { get; private set; }
    public TelegramId? TelegramId { get; private set; }
    public IReadOnlyCollection<Address> Addresses => _addresses.AsReadOnly();

    private readonly List<Address> _addresses = [];

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private Customer() { } // For ORMs
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    private Customer(
        Guid id,
        string name,
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

    public static Customer Create(string name)
    {
        return new Customer(
            id: Guid.NewGuid(),
            name: name,
            phoneNumber: null,
            telegramId: null,
            addresses: []
        );
    }

    public Customer WithPhoneNumber(PhoneNumber phoneNumber)
    {
        UpdatePhoneNumber(phoneNumber);
        return this;
    }

    public Customer WithTelegramId(TelegramId telegramId)
    {
        UpdateTelegramId(telegramId);
        return this;
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
