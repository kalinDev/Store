using System.Linq.Expressions;
using Core.DomainObjects.Interfaces;
using Store.Customers.Domain.Entities;

namespace Store.Customers.Domain.Interfaces;

public interface ICustomerRepository : IRepository<Customer>
{
    Task<IEnumerable<Customer>> SearchAsync(Expression<Func<Customer, bool>> predicate);
    Task<Customer> FindByIdAsync(Guid id);
    Task<Customer> FindOneByCpf(string cpf);
    Task<IEnumerable<Customer>> FindAsync();
    void Add(Customer entity);
}