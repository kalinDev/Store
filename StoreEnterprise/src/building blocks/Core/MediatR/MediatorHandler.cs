using Core.Messages;
using FluentValidation.Results;
using MediatR;

namespace Core.MediatR;

public class MediatorHandler : IMediatorHandler
{
    private readonly IMediator _mediator;

    public MediatorHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<ValidationResult> SendCommand<T>(T command) where T : Command
    {
        return await _mediator.Send(command);
    }

    public async Task PublishEvent<T>(T eventInstance) where T : Event
    {
        await _mediator.Publish(eventInstance);
    }

}