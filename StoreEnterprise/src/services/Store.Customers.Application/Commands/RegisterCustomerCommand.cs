using Core.Messages;
using FluentValidation;

namespace Store.Customers.Application.Commands;

public class RegisterCustomerCommand : Command
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Cpf { get; private set; }

    public RegisterCustomerCommand(Guid id, string name, string email, string cpf)
    {
        AggrefateId = id;
        Id = id;
        Name = name;
        Email = email;
        Cpf = cpf;
    }

    public override bool IsValid()
    {
        ValidationResult = new RegisterCustomerValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

public class RegisterCustomerValidation : AbstractValidator<RegisterCustomerCommand>
{
    public RegisterCustomerValidation()
    {
        RuleFor(c => c.Id)
            .NotEqual(Guid.Empty)
            .WithMessage("The id is not valid");

        RuleFor(c => c.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("The name was not informed");
        
        RuleFor(c => c.Cpf)
            .Must(HaveValidCpf)
            .WithMessage("The provided CPF is not valid");
        
        RuleFor(c => c.Email)
            .Must(HaveValidEmail)
            .WithMessage("The provided CPF is not valid");
    }

    protected static bool HaveValidCpf(string cpf)
        => Core.DomainObjects.Entities.Cpf.Validate(cpf);
    
    protected static bool HaveValidEmail(string email)
        => Core.DomainObjects.Entities.Email.Validate(email);
}