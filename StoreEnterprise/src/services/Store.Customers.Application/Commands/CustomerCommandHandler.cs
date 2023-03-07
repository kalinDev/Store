using Core.Messages;
using FluentValidation.Results;
using MediatR;
using Store.Customers.Application.Events;
using Store.Customers.Domain.Entities;
using Store.Customers.Domain.Interfaces;

namespace Store.Customers.Application.Commands;

public class CustomerCommandHandler : CommandHandler, IRequestHandler<RegisterCustomerCommand, ValidationResult>
{

    private readonly ICustomerRepository _customerRepository;

    public CustomerCommandHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<ValidationResult> Handle(RegisterCustomerCommand request, CancellationToken cancellationToken)
    {
        if (!request.IsValid()) return request.ValidationResult;

        var customer = new Customer(request.Id, request.Name, request.Email, request.Cpf);

        var client = await _customerRepository.FindOneByCpf(request.Cpf);
        
        if (client is not null)
        {
            AddError("CPF is already in use");
            return ValidationResult;
        }
        
        _customerRepository.Add(customer); 
        
        customer.AddEvent(new RegisteredCustomerEvent(request.Id, request.Name, request.Email, request.Cpf));
        
        return await PersistData(_customerRepository.UnitOfWork);
    }
}