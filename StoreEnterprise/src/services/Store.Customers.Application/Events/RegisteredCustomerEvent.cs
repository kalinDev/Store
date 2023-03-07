using Core.Messages;

namespace Store.Customers.Application.Events;

public class RegisteredCustomerEvent : Event
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Cpf { get; private set; }

    public RegisteredCustomerEvent(Guid id, string name, string email, string cpf)
    {
        AggrefateId = id;
        Id = id;
        Name = name;
        Email = email;
        Cpf = cpf;
    }
}