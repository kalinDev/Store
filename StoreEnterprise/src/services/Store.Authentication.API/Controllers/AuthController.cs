using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.Messages.IntegrationEvent;
using EasyNetQ;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Store.Authentication.Shared.Core.RequestModels;
using Store.Authentication.Shared.Core.ResponseModels;
using Store.MessageBus;
using Store.WebApi.Core.Identity;

namespace Store.Authentication.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ApiController
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly JwtSettings _jwtSettings;
    private IMessageBus _bus;
    
    public AuthController(SignInManager<IdentityUser>  signInManager, 
                          UserManager<IdentityUser> userManager,
                          IOptions<JwtSettings> jwtSettings,
                          IMessageBus bus)
    {
        _signInManager = signInManager;
       _userManager = userManager;
       _bus = bus;
       _jwtSettings = jwtSettings.Value;
    }
    
    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterRequest model)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);
        
        var user = new IdentityUser
        {
            UserName = model.Email,
            Email = model.Email
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            var customerResult = await RegisterCustomer(model);
            if (!customerResult.ValidationResult.IsValid)
            {
                await _userManager.DeleteAsync(user);
            
                return CustomResponse(customerResult.ValidationResult);
            }
            
            await _signInManager.SignInAsync(user, false);
            return CustomResponse(await GenerateJwt(model.Email));
        }

        foreach (var error in result.Errors)
        {
            AddError(error.Description);
        }

        return CustomResponse();
    }

    [HttpPost("Authenticate")]
    public async Task<IActionResult> Login(UserLoginRequest userLoginRequest)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var result = await _signInManager.PasswordSignInAsync(userLoginRequest.Email, userLoginRequest.Password, false, false);
        if (result.Succeeded)
        {
            return CustomResponse(await GenerateJwt(userLoginRequest.Email));
        }

        if (result.IsLockedOut)
        {
            AddError("User temporarily blocked by invalid login attempts.");
            return CustomResponse();
        }

        AddError("Invalid login attempt.");
        return CustomResponse();
    }

    private async Task<UserLoginResponse> GenerateJwt(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        var claims = await GetUserClaims(user);
        var encodedToken = GetEncodedJwt(claims);
        var response = CreateUserLoginResponse(encodedToken, user, claims);
        return response;
    }

    private async Task<List<Claim>> GetUserClaims(IdentityUser user)
    {
        var claims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);
        claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
        claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Iat,
        new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64));
        foreach (var role in roles)
        {
            claims.Add(new Claim("role", role));
        }
        return (List<Claim>)claims;
    }

    private string GetEncodedJwt(IEnumerable<Claim> claims)
    {
        var identityClaims = new ClaimsIdentity();
        identityClaims.AddClaims(claims);
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Issuer = _jwtSettings.Emitter,
            Audience = _jwtSettings.ValidOn,
            Subject = identityClaims,
            Expires = DateTime.UtcNow.AddHours(_jwtSettings.ExpirationInHours),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256Signature)
        });
        return tokenHandler.WriteToken(token);
    }

    private UserLoginResponse CreateUserLoginResponse(string encodedToken, IdentityUser user, IEnumerable<Claim> claims)
    {
        var response = new UserLoginResponse
        {
            Token = encodedToken,
            ExpiresIn = TimeSpan.FromHours(_jwtSettings.ExpirationInHours).TotalSeconds,
            UserToken = new UserTokenResponse
            {
                Id = user.Id,
                Email = user.Email,
                Claims = claims.Select(c => new UserClaimResponse { Type = c.Type, Value = c.Value })
            }
        };
        return response;
    }

    private async Task<ResponseMessage> RegisterCustomer(UserRegisterRequest userRegisterRequest)
    {
        var user = await _userManager.FindByEmailAsync(userRegisterRequest.Email);
        var userRegistered = new UserRegisteredIntegrationEvent(
            Guid.Parse(user.Id), userRegisterRequest.Name, userRegisterRequest.Email, userRegisterRequest.Cpf);

        try
        {
            return await _bus.RequestAsync<UserRegisteredIntegrationEvent, ResponseMessage>(userRegistered);
        }
        catch
        {
            await _userManager.DeleteAsync(user);
            throw;
        }
    }
}