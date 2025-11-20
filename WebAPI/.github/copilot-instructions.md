# Copilot Instructions for ChatPedidos WebAPI

## Architecture Overview

This is a Domain-Driven Design (DDD) .NET WebAPI project for a food
delivery/order system with Telegram bot integration.

### Core Architecture

- **Domain Layer**: Core business logic with aggregates `Customer`, `Order`, and
  `Product`
- **Application Layer**: CQRS with commands/queries organized by aggregate
  (e.g., `Application/Customer/Commands/`)
- **Infrastructure Layer**: EF Core with PostgreSQL, repositories, LiteBus for
  CQRS
- **API Layer**: FastEndpoints for REST API + separate TelegramBot application
- **TelegramBot Layer**: Dedicated app for Telegram webhook handling with
  interaction components

### Aggregates

- **Customer**: Manages customer info, addresses, phone, Telegram ID
- **Order**: Handles order lifecycle with strict status transitions (e.g.,
  `CustomerConfirm()` throws `InvalidOperationException` if not
  `OrderStatus.CustomerPending`)
- **Product**: Manages product details, availability, and images

### Key Abstractions

- `BaseEntity<TId>`: Base for entities with domain events + `CreatedAt`
  timestamp (set in constructor)
- `IAggregateRoot`: Marker for aggregate roots
- `IDomainEvent`: Marker for domain events
- `IValueObject`: Marker for value objects (records for immutability)
- `IChatProviderPort<TId>`: Port interface for chat providers (Telegram
  implementation)

## Patterns and Conventions

### Domain Patterns

- **Entities**: Inherit from `BaseEntity<Guid>`, use private setters, factory
  methods (e.g., `Customer.CreateFromTelegram`)
- **Value Objects**: Use records for immutability, implement `IValueObject`
  (e.g., `LocationPoint(float Latitude, float Longitude)`,
  `TelegramId(long Id)`)
- **Business Logic**: Encapsulated in aggregates with state guards and domain
  events
- **Aggregate Boundaries**: Avoid cross-aggregate references; use domain events
  for side effects

### Application Patterns

- **Commands**: Records implementing `ICommand<TResult>`, handlers inject
  `IRepository<T>`
- **Queries**: Records implementing `IQuery<TResult>`, handlers return DTOs from
  `Application/{Aggregate}/Queries/DTOs`
- **Organization**: Commands/queries organized by aggregate (e.g.,
  `Application/Customer/Commands/CreateCustomerFromTelegramCommand.cs`)

### Infrastructure Patterns

- **Entity Configurations**: `IEntityTypeConfiguration<T>` for EF mappings
  (e.g., `CustomerConfiguration` owns many `Addresses` with owned
  `LocationPoint`)
- **Persistence**: `AppDbContext` applies configurations from assembly, uses
  Npgsql
- **Migrations**: Run from `src/` with
  `--project Infrastructure/Persistence --startup-project Infrastructure/Migrator`

### API Patterns

- **FastEndpoints**: Inject `ICommandMediator`/`IQueryMediator`; use
  command/query records as request types
- **TelegramBot**: Separate application with FastEndpoints, custom JSON
  serialization (snake_case), RestSharp for Telegram API

### Telegram Integration Patterns

- **Interaction Components**: Fluent APIs for `KeyboardButton.WithText()`,
  `ReplyKeyboardMarkup.Create()` with `.WithPersistent()`,
  `.WithResizeKeyboard()`
- **DTOs**: Classes with private constructors + static factory methods (e.g.,
  `SendMessageRequest.Create()`)
- **Serialization**: `JsonIgnoreCondition.WhenWritingNull` to exclude null
  properties from API payloads
- **Adapters**: `TelegramProvider` implements `IChatProviderPort<TelegramId>`
  for chat operations

## Key Files

### Domain

- Aggregates: `src/Domain/Aggregates/{Customer,Order,Product}/{Aggregate}.cs`
- Abstractions: `src/Domain/Abstractions/BaseEntity.cs`
- Value Objects: `src/Domain/Aggregates/Common/LocationPoint.cs`,
  `src/Domain/Aggregates/Customer/TelegramId.cs`

### Application

- Commands: `src/Application/{Aggregate}/Commands/{Command}.cs`
- Queries: `src/Application/{Aggregate}/Queries/{Query}.cs`
- DTOs: `src/Application/{Aggregate}/Queries/DTOs/{Dto}.cs`

### Infrastructure

- Configurations:
  `src/Infrastructure/Persistence/Configurations/{Aggregate}Configuration.cs`
- Context: `src/Infrastructure/Persistence/AppDbContext.cs`

### API Applications

- **WebAPI**: `src/Apps/Api/` - REST endpoints with Swagger
- **TelegramBot**: `src/Apps/TelegramBot/` - Webhook handling with interaction
  components

### TelegramBot Components

- Interaction:
  `src/Apps/TelegramBot/Interaction/{KeyboardButton,ReplyKeyboardMarkup}.cs`
- DTOs: `src/Apps/TelegramBot/DTOs/Requests/SendMessageRequest.cs`
- Adapter: `src/Apps/TelegramBot/Adapters/TelegramProvider.cs`
- Configuration:
  `src/Apps/TelegramBot/Configurations/{DependencyInjection,RestClientConfiguration}.cs`

## Workflows

### Development

- **Build**: `dotnet build` (from `src/` directory)
- **Run API**: `dotnet run --project src/Apps/Api/Api.csproj`
- **Run TelegramBot**:
  `dotnet run --project src/Apps/TelegramBot/TelegramBot.csproj`
- **Debug**: VS Code launch configs for both applications (TelegramBot includes
  Cloudflare tunnel)

### Database

- **Add Migration**:
  `dotnet ef migrations add <Name> --project Infrastructure/Persistence --startup-project Infrastructure/Migrator --output-dir Migrations`
- **Update Database**:
  `dotnet ef database update --project Infrastructure/Persistence --startup-project Infrastructure/Migrator`

### Telegram Development

- **Local Testing**: Use Cloudflare tunnel for webhook testing
  (`Start Cloudflare Tunnel` task)
- **API Integration**: RestSharp with custom JSON options (snake_case, ignore
  nulls)
- **Interactive Messages**: Use fluent APIs for keyboard creation and message
  composition

## Integration Points

- **Telegram API**: Via `TelegramId` value object, RestSharp client with custom
  serialization
- **Database**: PostgreSQL with EF Core, connection string in user secrets
- **CQRS**: LiteBus for command/query mediation
- **API Framework**: FastEndpoints for both REST and webhook endpoints
- **External Services**: Future payment (`PaymentId`), rider (`RiderId`)
  services

## Code Generation Patterns

### Adding a New Aggregate

1. Create aggregate in `src/Domain/Aggregates/{Name}/`
2. Add to `AppDbContext` with `DbSet<{Name}>`
3. Create `IEntityTypeConfiguration<{Name}>` in Persistence/Configurations/
4. Add parameterless constructor for EF Core: `private {Name}() { }`
5. Generate migration and update database

### Adding Telegram Features

1. Define DTOs in `DTOs/Requests/` or `DTOs/Types/` with records for API types
2. Use classes with private constructors + static `Create()` for requests
3. Add fluent methods for optional properties (e.g., `WithReplyMarkup()`)
4. Implement in `TelegramProvider` following `IChatProviderPort<TelegramId>`
5. Configure JSON serialization options in `RestClientConfiguration`

### Adding Commands/Queries

1. Create record in `Application/{Aggregate}/Commands/` implementing
   `ICommand<TResult>`
2. Create handler in `Handlers/` subdirectory injecting required repositories
3. Add endpoint in appropriate API application using `ICommandMediator`
