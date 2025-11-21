using Application.Customer.Commands.CreateCustomerFromTelegram;
using FastEndpoints;
using LiteBus.Commands.Abstractions;

namespace Api.Customers;

public class CreateCustomerFromTelegramEndpoint(ICommandMediator commandMediator)
    : Endpoint<CreateCustomerFromTelegramCommand, Guid>
{
    private readonly ICommandMediator _commandMediator = commandMediator;

    public override void Configure()
    {
        Post("/customers/from-telegram");
        AllowAnonymous();
    }

    public override async Task HandleAsync(
        CreateCustomerFromTelegramCommand req,
        CancellationToken ct
    )
    {
        var customerId = await _commandMediator.SendAsync(req, ct);
        await Send.OkAsync(customerId, ct);
    }
}
