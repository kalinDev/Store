using Core.DomainObjects.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Catalog.Domain.Interfaces;
using Store.Catalog.Shared.Core.RequestModels;
using Store.Catalog.Shared.Core.ResponseModels;
using Store.WebApi.Core.Identity;

namespace Store.Catalog.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CatalogController : ApiController
{
    private readonly IProductService _productServiceService;
    public CatalogController(INotifier notifier, IProductService productServiceService) : base(notifier)
    {
        _productServiceService = productServiceService;
    }
    
    [HttpGet("Products/{guid:int}")]
    [AllowAnonymous]
    public async Task<ActionResult<ProductResponse>> FindOneAsync(Guid id)
    {
        var productResponse = await _productServiceService.FindByIdAsync(id);
        return CustomResponse(productResponse);
    }

    [HttpGet("Products")]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<ProductResponse>>> FindAsync()
    {
        var productResponses = await _productServiceService.FindAsync();
        return CustomResponse(productResponses);
    }

    [HttpPost("Products")]
    [ClaimsAuthorize("Catalog", "Add")]
    public async Task<ActionResult> PostAsync([FromBody] ProductRequest productRequest)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        await _productServiceService.AddAsync(productRequest);
        
        return CustomResponse();
    }
}