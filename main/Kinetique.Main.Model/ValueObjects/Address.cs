namespace Kinetique.Main.Model.ValueObjects;

public sealed class Address
{
    public string Value { get; }

    public Address(string value)
    {
        // validation later
        Value = value;
    }
    
    public static implicit operator string(Address address) => $"{address.Value}";
    public static implicit operator Address(string address) => new(address);
    public override string ToString() => Value;
}