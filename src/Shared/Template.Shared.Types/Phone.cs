using PhoneNumbers;

namespace Template.Shared.Types;

public sealed record Phone
{
    private Phone()
    {
    }

    private Phone(string prefix, string number)
    {
        Prefix = prefix;
        Number = number;
    }

    public string Prefix { get; } = null!;
    public string Number { get; } = null!;

    public static Phone Create(string prefix, string number)
    {
        var phoneNumberUtil = PhoneNumberUtil.GetInstance();

        if (string.IsNullOrWhiteSpace(prefix))
            throw new ArgumentException("Prefix for phone number cannot be null or whitespace.");

        if (string.IsNullOrWhiteSpace(number))
            throw new ArgumentException("Phone number cannot be null or whitespace.");

        var cleanPrefix = prefix.TrimStart('+');
        var fullNumber = $"+{cleanPrefix}{number}";
        var phoneNumber = phoneNumberUtil.Parse(fullNumber, null);
        var countryCode = phoneNumber.CountryCode;

        if (!phoneNumberUtil.IsValidNumber(phoneNumber))
            throw new ArgumentException("Provided phone number is not valid.");

        if (!cleanPrefix.Equals(countryCode.ToString()))
            throw new ArgumentException("Provided phone number does not match the expected country code.");

        return new Phone(prefix, number);
    }
}