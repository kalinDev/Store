using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Store.Authentication.API.Controllers;

[ApiController]
public abstract class ApiController : ControllerBase
{
    protected ICollection<string> Errors = new List<string>();

    protected ActionResult CustomResponse(object result = null)
    {
        if (IsOperationValid())
        {
            return Ok(result);
        }

        return BadRequest(new
        {
            sucess = false,
            errors = Errors
        }); 
    }

    protected ActionResult CustomResponse(ModelStateDictionary modelState)
    {
        var errors = modelState.Values.SelectMany(e => e.Errors);
        foreach (var error in errors)
        {
            AddError(error.ErrorMessage);
        }

        return CustomResponse();
    }

    protected bool IsOperationValid()
        => !Errors.Any();

    protected void AddError(string error)
        => Errors.Add(error);

}