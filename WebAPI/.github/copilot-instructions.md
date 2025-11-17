# Copilot Instructions for ChatPedidos WebAPI

## Architecture Overview

This is a Domain-Driven Design (DDD) .NET WebAPI project for a food
delivery/order system integrated with Telegram.

- **Domain Layer**: Core business logic with aggregates `Customer` and `Order`.
- **Aggregates**:
  - `Customer`: Manages customer info, addresses, phone, Telegram ID.
  - `Order`: Handles order lifecycle from creation to completion/cancellation.
- **Key Abstractions**:
  - `BaseEntity<TId>`: Base for entities with domain events support.
  - `IAggregateRoot`: Marker for aggregate roots.
  - `IDomainEvent`: Marker for domain events.
  - `IValueObject`: Marker for value objects.

## Patterns and Conventions

- **Entities**: Inherit from `BaseEntity<Guid>`, use private setters, factory
  methods (e.g., `Customer.CreateFromTelegram`, `Order.Create`).
- **Value Objects**: Use records for immutability, implement `IValueObject`
  (e.g., `LocationPoint(float Latitude, float Longitude)`,
  `TelegramId(long Id)`).
- **Business Logic**: Encapsulated in aggregates with state guards (e.g.,
  `Order.CustomerConfirm()` throws `InvalidOperationException` if not
  `OrderStatus.CustomerPending`).
- **Domain Events**: Add via `entity.AddDomainEvent(event)`, clear after
  handling (infrastructure not yet implemented).
- **Status Transitions**: Strict enum-based flow in `Order` (see `OrderStatus`
  enum for valid transitions).

## Key Files

- Aggregates: `src/Domain/Aggregates/Customer/Customer.cs`,
  `src/Domain/Aggregates/Order/Order.cs`
- Abstractions: `src/Domain/Abstractions/BaseEntity.cs`
- Value Objects: `src/Domain/Aggregates/Common/LocationPoint.cs`
- Diagrams: `docs/CustomerAggregate.mermaid`, `docs/OrderAggregate.mermaid`

## Workflows

- Build: `dotnet build` (from solution root `src/`)
- Test: `dotnet test` (when test projects added)
- Run: `dotnet run` (when application project added)
- Debug: Use VS Code .NET debugger on main project.

## Integration Points

- Telegram integration via `TelegramId` value object in `Customer`.
- External dependencies: None yet, but expect payment (`PaymentId`), rider
  (`RiderId`) services.

Focus on aggregate boundaries; avoid cross-aggregate references. Use domain
events for side effects.
