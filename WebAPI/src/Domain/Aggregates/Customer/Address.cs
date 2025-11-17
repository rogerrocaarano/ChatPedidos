using Domain.Abstractions;
using Domain.Aggregates.Common;

namespace Domain.Aggregates.Customer;

public class Address : BaseEntity<Guid>
{
    public LocationPoint Location { get; private set; }
    public string Name { get; private set; }

    private Address(Guid id, LocationPoint location, string name)
        : base(id)
    {
        Location = location;
        Name = name;
    }

    public static Address Create(LocationPoint location, string name)
    {
        return new Address(Guid.NewGuid(), location, name);
    }

    public void UpdateName(string name)
    {
        Name = name;
    }

    public void UpdateLocation(LocationPoint location)
    {
        Location = location;
    }
}
