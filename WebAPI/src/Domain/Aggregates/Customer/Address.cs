using Domain.Aggregates.Common;

namespace Domain.Aggregates.Customer;

public class Address : BaseEntity<Guid>
{
    public LocationPoint Location { get; private set; }
    public string Name { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private Address() { } // For ORMs
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

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
