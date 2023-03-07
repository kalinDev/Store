namespace Store.Authentication.Shared.Core.ResponseModels;

public class UserLoginResponse
{
    public string Token { get; set; }
    public double ExpiresIn { get; set; }
    public UserTokenResponse UserToken { get; set; }
}