using System;
using System.Threading;
using System.Threading.Tasks;
using Core.MediatR;
using Core.Messages.IntegrationEvent;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Store.Customers.Application.Commands;
using Store.MessageBus;

namespace Store.Customers.Service.Services
{
    public class RegisterCustomerIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _bus;
        private readonly IServiceProvider _serviceProvider;
        
        public RegisterCustomerIntegrationHandler(IServiceProvider serviceProvider, IMessageBus bus)
        {
            _serviceProvider = serviceProvider;
            _bus = bus;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetResponder();
            return Task.CompletedTask;
        }
        
        private void SetResponder()
        {
            _bus.RespondAsync<UserRegisteredIntegrationEvent, ResponseMessage>(async request
                => await RegisterCustomer(request));

            _bus.AdvancedBus.Connected += OnConnect;
        }

        private void OnConnect(object s, EventArgs e)
        {
            SetResponder();
        }
        
        private async Task<ResponseMessage> RegisterCustomer(UserRegisteredIntegrationEvent message)
        {
            var customerCommand = new RegisterCustomerCommand(message.Id, message.Name, message.Email, message.Cpf);

            using var scope = _serviceProvider.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
            var success = await mediator.SendCommand(customerCommand);

            return new ResponseMessage(success);
        }
    }
}