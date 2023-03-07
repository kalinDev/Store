using Core.DomainObjects.Interfaces;
using FluentValidation.Results;

namespace Core.Messages;

public abstract class CommandHandler
{
    protected ValidationResult ValidationResult;

    protected CommandHandler()
    {
        ValidationResult = new ValidationResult();
    }

    protected void AddError(string message)
    {
        ValidationResult.Errors.Add(new ValidationFailure(string.Empty, message));
    }

    protected async Task<ValidationResult> PersistData(IUnitOfWork uow)
    {
        
        if(!await uow.Commit()) AddError("There was an error while persisting data to the database");
        
        return ValidationResult;
    }
}