using System.Linq.Expressions;
using Core.DomainObjects.Interfaces;
using Microsoft.EntityFrameworkCore;
using Store.Catalog.Domain.Entities;
using Store.Catalog.Domain.Interfaces;

namespace Store.Catalog.Data.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly CatalogContext _context;

    public ProductRepository(CatalogContext context)
    {
        _context = context;
    }

    public IUnitOfWork UnitOfWork => _context;
    public async Task<bool> AnyAsync(Guid id)
        => await _context.Products.AnyAsync();


    public async Task<Product> FindByIdAsync(Guid id)
        => await _context.Products.FindAsync(id);

    public async Task<IEnumerable<Product>> FindAsync()
        => await _context.Products.AsNoTracking().ToListAsync();
    
    public async Task<IEnumerable<Product>> SearchAsync(Expression<Func<Product, bool>> predicate)
        => await _context.Products.AsNoTracking().Where(predicate).ToListAsync();

    public void Add(Product entity)
        => _context.Products.Add(entity);
    
    public void Update(Product entity)
        => _context.Products.Update(entity); 

    public void Remove(Guid id)
        => _context.Products.Remove(new Product { Id = id });
    
    public void Dispose()
        => _context?.Dispose();
}