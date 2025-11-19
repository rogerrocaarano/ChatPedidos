using Application.Customer.Commands;
using FastEndpoints;
using LiteBus.Commands.Abstractions;

namespace Api.Customers;

public class RemoveCustomerAddressEndpoint(ICommandMediator commandMediator)
    : Endpoint<RemoveCustomerAddressCommand>
{
    private readonly ICommandMediator _commandMediator = commandMediator;

    public override void Configure()
    {
        Delete("/customers/{CustomerId}/addresses/{AddressId}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(RemoveCustomerAddressCommand req, CancellationToken ct)
    {
        await _commandMediator.SendAsync(req, ct);
        await Send.NoContentAsync(ct);
    }
}