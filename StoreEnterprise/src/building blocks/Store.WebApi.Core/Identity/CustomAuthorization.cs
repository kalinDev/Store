﻿using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Store.WebApi.Core.Identity;

public  class CustomAuthorization
{
    public static bool ValidateUserClaims(HttpContext context, string claimName, string claimValue)
    {
        return context.User.Identity?.IsAuthenticated == true &&
               context.User.Claims.Any(c => c.Type == claimName && c.Value.Contains(claimValue));
    }
    
}
public class ClaimsAuthorizeAttribute : TypeFilterAttribute
{

    public ClaimsAuthorizeAttribute(string claimName, string claimValue) : base(typeof(ClaimFilter))
    {
        Arguments = new object[] { new Claim(claimName, claimValue) };
    }
}
    
public class ClaimFilter : IFilterMetadata
{
    private readonly Claim _claim;

    public ClaimFilter(Claim claim)
    {
        _claim = claim;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.User.Identity?.IsAuthenticated == true)
        {
            context.Result = new StatusCodeResult(401);
            return;
        }
        if (!CustomAuthorization.ValidateUserClaims(context.HttpContext, _claim.Type,_claim.Value))
        {
            context.Result = new StatusCodeResult(403);
        }
    }
}