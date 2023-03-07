using Store.Catalog.Shared.Core.RequestModels;
using Store.Catalog.Shared.Core.ResponseModels;

namespace Store.Catalog.Domain.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductResponse>> FindAsync();
    Task<ProductResponse> FindByIdAsync(Guid id);
    Task AddAsync(ProductRequest productRequest);
    Task UpdateAsync(ProductRequest productRequest);
    Task<bool> AnyAsync(Guid id);
    Task RemoveByIdAsync(Guid id);
}