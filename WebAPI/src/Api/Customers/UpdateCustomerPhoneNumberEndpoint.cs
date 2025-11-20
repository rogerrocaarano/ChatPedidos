using Application.Customer.Commands.UpdateCustomerPhoneNumber;
using FastEndpoints;
using LiteBus.Commands.Abstractions;

namespace Api.Customers;

public class UpdateCustomerPhoneNumberEndpoint(ICommandMediator commandMediator)
    : Endpoint<UpdateCustomerPhoneNumberCommand>
{
    private readonly ICommandMediator _commandMediator = commandMediator;

    public override void Configure()
    {
        Put("/customers/{CustomerId}/phone");
        AllowAnonymous();
    }

    public override async Task HandleAsync(
        UpdateCustomerPhoneNumberCommand req,
        CancellationToken ct
    )
    {
        await _commandMediator.SendAsync(req, ct);
        await Send.NoContentAsync(ct);
    }
}
