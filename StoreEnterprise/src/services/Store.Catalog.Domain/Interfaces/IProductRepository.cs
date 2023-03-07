using System.Linq.Expressions;
using Core.DomainObjects.Interfaces;
using Store.Catalog.Domain.Entities;

namespace Store.Catalog.Domain.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> SearchAsync(Expression<Func<Product, bool>> predicate);
    Task<Product> FindByIdAsync(Guid id);
    Task<IEnumerable<Product>> FindAsync();
    void Add(Product entity);
    void Update(Product entity);
    void Remove(Guid id);
    Task<bool> AnyAsync(Guid id);
}