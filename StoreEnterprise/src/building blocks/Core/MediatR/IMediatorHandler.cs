using Core.Messages;
using FluentValidation.Results;

namespace Core.MediatR;

public interface IMediatorHandler
{
    Task PublishEvent<T>(T eventInstance) where T : Event;
    Task<ValidationResult> SendCommand<T>(T command) where T : Command;
}