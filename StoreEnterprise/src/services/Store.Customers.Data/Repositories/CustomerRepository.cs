using System.Linq.Expressions;
using Core.DomainObjects.Interfaces;
using Microsoft.EntityFrameworkCore;
using Store.Customers.Domain.Entities;
using Store.Customers.Domain.Interfaces;

namespace Store.Customers.Data.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly CustomersContext _context;

    public CustomerRepository(CustomersContext context)
    {
        _context = context;
    }

    public IUnitOfWork UnitOfWork => _context;
    
    public async Task<bool> AnyAsync(Guid id)
        => await _context.Customers.AnyAsync();


    public Task<Customer> FindOneByCpf(string cpf)
        => _context.Customers.FirstOrDefaultAsync(c => c.Cpf.Number == cpf);
    public async Task<IEnumerable<Customer>> SearchAsync(Expression<Func<Customer, bool>> predicate)
        => await _context.Customers.AsNoTracking().Where(predicate).ToListAsync();

    public async Task<Customer> FindByIdAsync(Guid id)
        => await _context.Customers.FindAsync(id);

    public async Task<IEnumerable<Customer>> FindAsync()
        => await _context.Customers.AsNoTracking().ToListAsync();

    public void Add(Customer entity)
        => _context.Customers.Add(entity);
    
    public void Dispose()
        => _context?.Dispose();
}