using Application.Customer.Commands;
using FastEndpoints;
using LiteBus.Commands.Abstractions;

namespace Api.Customers;

public class UpdateCustomerAddressEndpoint(ICommandMediator commandMediator)
    : Endpoint<UpdateCustomerAddressCommand>
{
    private readonly ICommandMediator _commandMediator = commandMediator;

    public override void Configure()
    {
        Put("/customers/{CustomerId}/addresses/{AddressId}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateCustomerAddressCommand req, CancellationToken ct)
    {
        await _commandMediator.SendAsync(req, ct);
        await Send.NoContentAsync(ct);
    }
}
