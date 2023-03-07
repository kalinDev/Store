namespace Store.Authentication.Shared.Core.ResponseModels;

public class UserTokenResponse
{
    public string Id { get; set; }
    public string Email { get; set; }
    public IEnumerable<UserClaimResponse> Claims { get; set; }
}