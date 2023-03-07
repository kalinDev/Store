using AutoMapper;
using Core.DomainObjects.Interfaces;
using Core.Service;
using Store.Catalog.Application.Validations;
using Store.Catalog.Domain.Entities;
using Store.Catalog.Domain.Interfaces;
using Store.Catalog.Shared.Core.RequestModels;
using Store.Catalog.Shared.Core.ResponseModels;

namespace Store.Catalog.Application.Services;

public class ProductService : BaseService, IProductService
{

    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    
    public ProductService(IProductRepository productRepository, IMapper mapper, INotifier notifier) : base(notifier)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }
    
    public async Task<bool> AnyAsync(Guid id) =>
        await _productRepository.AnyAsync(id);

    public async Task RemoveByIdAsync(Guid id)
    {
        _productRepository.Remove(id);
        if(!await _productRepository.UnitOfWork.Commit()) Notify("Error when removing entity"); 
    }

    public async Task<ProductResponse> FindByIdAsync(Guid id) =>
        _mapper.Map<ProductResponse>(await _productRepository.FindByIdAsync(id));

    public async Task<IEnumerable<ProductResponse>> FindAsync()
    {
        var products = await _productRepository.FindAsync();
        return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductResponse>>(products);
    }
    
    public async Task AddAsync(ProductRequest productRequest)
    {
        var product = _mapper.Map<ProductRequest, Product>(productRequest);
        if (!RunValidation(new ProductValidator(), product)) return;

        _productRepository.Add(product);
        if(!await _productRepository.UnitOfWork.Commit()) Notify("Error when adding entity"); 
    }
    
    public async Task UpdateAsync(ProductRequest productRequest)
    {
        var product = _mapper.Map<ProductRequest, Product>(productRequest);
        if (!RunValidation(new ProductValidator(), product)) return;

        _productRepository.Update(product);
        if(!await _productRepository.UnitOfWork.Commit()) Notify("Error when updating entity"); 
    }

}