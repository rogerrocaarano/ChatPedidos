# Copilot Instructions for ChatPedidos WebAPI

## Architecture Overview

This is a Domain-Driven Design (DDD) .NET WebAPI project for a food
delivery/order system integrated with Telegram.

- **Domain Layer**: Core business logic with aggregates `Customer`, `Order`, and
  `Product`.
- **Aggregates**:
  - `Customer`: Manages customer info, addresses, phone, Telegram ID.
  - `Order`: Handles order lifecycle from creation to completion/cancellation
    with strict status transitions (e.g., `CustomerConfirm()` throws
    `InvalidOperationException` if not `OrderStatus.CustomerPending`).
  - `Product`: Manages product details, availability, and images.
- **Key Abstractions**:
  - `BaseEntity<TId>`: Base for entities with domain events support (add via
    `entity.AddDomainEvent(event)`, clear after handling).
  - `IAggregateRoot`: Marker for aggregate roots.
  - `IDomainEvent`: Marker for domain events.
  - `IValueObject`: Marker for value objects (e.g.,
    `LocationPoint(float Latitude, float Longitude)`, `TelegramId(long Id)`).
- **Entities**: Inherit from `BaseEntity<Guid>`, use private setters, factory
  methods (e.g., `Customer.CreateFromTelegram`, `Order.Create`,
  `Product.Create`).
- **Value Objects**: Use records for immutability, implement `IValueObject`.
- **Business Logic**: Encapsulated in aggregates with state guards.
- **Infrastructure**: EF Core with PostgreSQL, repositories implement
  `IRepository<T>`, LiteBus for commands.
- **API**: Uses FastEndpoints for endpoint definitions with request/response
  records.

## Patterns and Conventions

- **Aggregate Boundaries**: Avoid cross-aggregate references; use domain events
  for side effects.
- **Status Transitions**: Strict enum-based flow in `Order` (see `OrderStatus`
  enum for valid transitions).
- **Entity Configurations**: Use `IEntityTypeConfiguration` for EF mappings
  (e.g., `CustomerConfiguration` owns many `Addresses` with owned
  `LocationPoint`).
- **Commands**: Use LiteBus with records implementing `ICommand<TResult>`,
  handlers inject `IRepository<T>`.
- **Persistence**: `AppDbContext` applies configurations from assembly, uses
  Npgsql.
- **API Endpoints**: FastEndpoints inject `ICommandMediator` to send commands.

## Key Files

- Aggregates: `src/Domain/Aggregates/Customer/Customer.cs`,
  `src/Domain/Aggregates/Order/Order.cs`,
  `src/Domain/Aggregates/Product/Product.cs`
- Abstractions: `src/Domain/Abstractions/BaseEntity.cs`
- Value Objects: `src/Domain/Aggregates/Common/LocationPoint.cs`
- Configurations:
  `src/Infrastructure/Persistence/Configurations/CustomerConfiguration.cs`
- Commands: `src/Application/Commands/CreateCustomerFromTelegramCommand.cs`
- Handlers: `src/Application/Handlers/CreateCustomerFromTelegramHandler.cs`
- Endpoints: `src/Apps/Api/Endpoints/Products/CreateProductEndpoint.cs`
- Diagrams: `docs/CustomerAggregate.mermaid`, `docs/OrderAggregate.mermaid`

## Workflows

- Build: `dotnet build` (from solution root `src/`)
- Test: `dotnet test` (when test projects added)
- Run: `dotnet run --project src/Apps/Api/Api.csproj`
- Debug: Use VS Code .NET debugger on `src/Apps/Api/Api.csproj` (launch profiles
  in `Properties/launchSettings.json`)
- Migrations:
  `dotnet dotnet-ef migrations add <Name> --project Infrastructure/Persistence --startup-project Infrastructure/Migrator --output-dir Migrations`

### Adding a New Aggregate

To add a new aggregate (e.g., `Order`):

1. **Add to AppDbContext**: In `src/Infrastructure/Persistence/AppDbContext.cs`,
   add `public DbSet<Order> Orders { get; set; }` and import the namespace.
2. **Add Parameterless Constructor**: In the aggregate root (e.g.,
   `src/Domain/Aggregates/Order/Order.cs`), add
   `private Order() { } // For ORMs` if not present (required for EF Core).
3. **Create Configuration**: Add
   `src/Infrastructure/Persistence/Configurations/OrderConfiguration.cs`
   implementing `IEntityTypeConfiguration<Order>`, configure keys, properties,
   and owned entities (see `CustomerConfiguration.cs` for examples).
4. **Generate Migration**: From `src/`, run
   `dotnet dotnet-ef migrations add AddOrderAggregate --project Infrastructure/Persistence --startup-project Infrastructure/Migrator --output-dir Migrations`.
5. **Update Database**: Run
   `dotnet dotnet-ef database update --project Infrastructure/Persistence --startup-project Infrastructure/Migrator`.

## Integration Points

- Telegram integration via `TelegramId` value object in `Customer`.
- External dependencies: PostgreSQL (connection string in user secrets), LiteBus
  for commands, FastEndpoints for API.
- Future: Payment (`PaymentId`), rider (`RiderId`) services.
