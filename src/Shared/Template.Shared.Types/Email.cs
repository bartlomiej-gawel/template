using System.Text.RegularExpressions;

namespace Template.Shared.Types;

public sealed partial record Email
{
    [GeneratedRegex(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*@((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))\z", RegexOptions.IgnoreCase | RegexOptions.Compiled)]
    private static partial Regex EmailRegex();

    private static bool IsValid(string value) => EmailRegex().IsMatch(value);

    private Email()
    {
    }

    private Email(string value) => Value = value;

    public string Value { get; } = null!;

    public static Email Create(string value)
    {
        if (!IsValid(value))
            throw new ArgumentException("Provided email does not met the requirements.");

        return new Email(value);
    }

    public static implicit operator string(Email value) => value.Value;
    public static implicit operator Email(string value) => new(value);
}