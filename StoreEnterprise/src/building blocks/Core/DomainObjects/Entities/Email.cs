using System.Text.RegularExpressions;

namespace Core.DomainObjects.Entities;

public class Email
{
    public const int EmailMaxLength = 256;
    public const int EmailMinLength = 5;
    public string Address { get; private set; }
    
    protected Email() {}

    public Email(string address)
    {
        if(!Validate(address)) throw new DomainException("Invalid Email");
        Address = address;
    }

    public static bool Validate(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return false;
        }

        if (email.Length is EmailMaxLength or EmailMinLength)
        {
            return false;
        }
        
        // Email regular expression pattern
        string pattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";

        // Check if the email matches the pattern
        Regex regex = new Regex(pattern);
        Match match = regex.Match(email);
        return match.Success;
    }    
}