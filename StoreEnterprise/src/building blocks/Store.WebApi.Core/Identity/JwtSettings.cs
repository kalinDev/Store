namespace Store.WebApi.Core.Identity;

public class JwtSettings
{
    public string Secret { get; set; }
    public int ExpirationInHours { get; set; }
    public string Emitter { get; set; }
    public string ValidOn { get; set; }
}