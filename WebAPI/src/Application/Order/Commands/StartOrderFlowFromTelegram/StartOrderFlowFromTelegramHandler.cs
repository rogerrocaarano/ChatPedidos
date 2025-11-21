using Application.Customer.Commands.CreateCustomerFromTelegram;
using Application.Customer.Queries.GetCustomerByTelegramId;
using Application.Order.Commands.CreateOrder;
using LiteBus.Commands.Abstractions;
using LiteBus.Queries.Abstractions;

namespace Application.Order.Commands.StartOrderFlowFromTelegram;

public class StartOrderFlowFromTelegramHandler(ICommandMediator commands, IQueryMediator queries)
    : ICommandHandler<StartOrderFlowFromTelegramCommand, Guid>
{
    public async Task<Guid> HandleAsync(StartOrderFlowFromTelegramCommand cmd, CancellationToken ct)
    {
        // 1. Intentar obtener el cliente existente
        var customer = await queries.QueryAsync(
            new GetCustomerByTelegramIdQuery(cmd.TelegramId),
            ct
        );

        // 2. Resolver ID: Si el cliente existe usa su ID, si no, crea uno nuevo (Lógica "Get or Create")
        var customerId =
            customer?.Id
            ?? await commands.SendAsync(
                new CreateCustomerFromTelegramCommand(cmd.CustomerName, cmd.TelegramId.Id),
                ct
            );

        // 3. Crear y retornar la orden
        return await commands.SendAsync(new CreateOrderCommand(customerId), ct);
    }
}
