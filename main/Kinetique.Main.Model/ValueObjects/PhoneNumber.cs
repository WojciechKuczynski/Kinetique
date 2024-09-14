using System.Text.RegularExpressions;

namespace Kinetique.Main.Model.ValueObjects;

public sealed class PhoneNumber
{
    private const string PhoneRegex = @"[\+][1-9][0-9]{1,4}[0-9]{9}";
    
    public string NumberDirection { get; private set; }
    public string Number { get; private set; }

    public PhoneNumber(string number)
    {
        ValidatePhoneNumber(number);
    }

    private void ValidatePhoneNumber(string number)
    {
        var numberWithoutWhiteCharacters = number.Trim();

        if (!Regex.IsMatch(numberWithoutWhiteCharacters, PhoneRegex))
        {
            throw new Exception("Incorrect number!");
        }

        NumberDirection = numberWithoutWhiteCharacters[..^9];
        Number = numberWithoutWhiteCharacters[NumberDirection.Length..];
    }
    
    public static implicit operator string(PhoneNumber number) => $"{number.NumberDirection}{number.Number}";
    public static implicit operator PhoneNumber(string number) => new(number);
    public override string ToString() => $"{NumberDirection} {Number}";
}