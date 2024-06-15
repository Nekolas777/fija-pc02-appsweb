# Creating this kind of Projects

## DDD Structure to use

![Descripción de la imagen](https://i.postimg.cc/KYYbpr8V/ddd-csharp.jpg)


## Loading the Shared Services and the Project Configurations

Be sure that you are creating the project as

- Web
  - Web Api Following what the test asks you to create

### Using the Shared

In these cases when we implement the shared we MUST change the namespace to the current namespace we will be using.

Like in these example:
```csharp
namespace si730pc2u202217485.API.Shared.Infrastructure.Persistence.EFC.Repositories;
```

Keep in mind that we also should change the namespace of the `AppDbContext` and the `Entity` to the current namespace we are using.
And all the other classes that are in the shared.

`REMEMBER` the changes to the `AppDBContext` will be made once you have finished 
the solution since then you will have the Aggregates and such for the AppDbContext.

### Using the Configurations

`appsettings.json` use database = to whatever the schema is called
```csharp

{
  "ConnectionStrings": {
    "DefaultConnection": "server=localhost,3306;user=root;password=password;database=cottonknit;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}

```


`Startup.cs` use the following code to configure the database and REMEMBER TO ADD THE SERVICES

COMMAND SERVICE AND REPOSITORIES

ALSO, DON`T FORGET TO MAKE THE SWAGGER DOCUMENTATION

```csharp
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using si730pc2u202217485.Sale.Application.Internal.CommandServices;
using si730pc2u202217485.Sale.Domain.Repositories;
using si730pc2u202217485.Sale.Domain.Services;
using si730pc2u202217485.Sale.Infrastructure.Persistence.EFC.Repositories;
using si730pc2u202217485.Shared.Domain.Model.Repositories;
using si730pc2u202217485.Shared.Infrastructure.Interfaces.ASP.Configuration;
using si730pc2u202217485.Shared.Infrastructure.Interfaces.Middleware;
using si730pc2u202217485.Shared.Infrastructure.Persistence.EFC.Configuration;
using si730pc2u202217485.Shared.Infrastructure.Persistence.EFC.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers( options => options.Conventions.Add(new KebabCaseRouteNamingConvention()));

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Configure Database Context and Logging Levels

builder.Services.AddDbContext<AppDbContext>(
    options =>
    {
        if (connectionString != null)
            if (builder.Environment.IsDevelopment())
                options.UseMySQL(connectionString)
                    .LogTo(Console.WriteLine, LogLevel.Information)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors();
            else if (builder.Environment.IsProduction())
                options.UseMySQL(connectionString)
                    .LogTo(Console.WriteLine, LogLevel.Error)
                    .EnableDetailedErrors();    
    });

// Configure Lowercase URLs
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    c =>
    {
        c.SwaggerDoc("v1",
            new OpenApiInfo()
            {
                Title = "Purchase Orders API",
                Version = "v1",
                Description = "Purchase Orders Platform API",
                TermsOfService = new Uri("https://reservation.com/tos"),
                Contact = new OpenApiContact
                {
                    Name = "u2022217485 Studios",
                    Email = "nicolas@neko.com"
                },
                License = new OpenApiLicense
                {
                    Name = "Apache 2.0",
                    Url = new Uri("https://www.apache.org/licenses/LICENSE-2.0.html")
                }
            });
        c.EnableAnnotations();
    });

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Configure Dependency Injection

// Shared Bounded Context Injection Configuration
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Reservation Bounded Context Injection Configuration
builder.Services.AddScoped<IPurchaseOrderRepository, PurchaseOrderRepository>();
builder.Services.AddScoped<IPurchaseOrderCommandService, PurchaseOrderCommandServiceImpl>();


var app = builder.Build();

// Verify Database Objects are created
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


// Configure Lowercase URLs
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Configure Dependency Injection

// Shared Bounded Context Injection Configuration
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
//hr Bounded Context Injection Configuration
builder.Services.AddScoped<IPurchaseOrderCommandService, PurchaseOrderCommandServiceImpl>();
builder.Services.AddScoped<IPurchaseOrderRepository, PurchaseOrderRepository>();




var app = builder.Build();

// Verify Database Objects are created
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
```

### Configuration for Dependencies

Do not forget this will be found in the `csproj` file under the File System View.

```csharp
<Project Sdk="Microsoft.NET.Sdk.Web">

    <PropertyGroup>
        <TargetFramework>net8.0</TargetFramework>
        <Nullable>enable</Nullable>
        <ImplicitUsings>enable</ImplicitUsings>
    </PropertyGroup>

    <ItemGroup>
        <PackageReference Include="EntityFrameworkCore.CreatedUpdatedDate" Version="8.0.0" />
        <PackageReference Include="Humanizer" Version="2.14.1" />
        <PackageReference Include="Microsoft.AspNetCore.OpenApi" Version="8.0.5"/>
        <PackageReference Include="Microsoft.EntityFrameworkCore" Version="8.0.5" />
        <PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="8.0.5">
          <PrivateAssets>all</PrivateAssets>
          <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
        </PackageReference>
        <PackageReference Include="MySql.EntityFrameworkCore" Version="8.0.2" />
        <PackageReference Include="Swashbuckle.AspNetCore" Version="6.4.0"/>
        <PackageReference Include="Swashbuckle.AspNetCore.Annotations" Version="6.5.0" />
    </ItemGroup>

</Project>
```

## DbContext configuration for entities

```csharp

using Microsoft.EntityFrameworkCore;
using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using sexo730u202217485.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using si730pc2u202217485.Sale.Domain.Model.Aggregates;
using si730pc2u202217485.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;

namespace si730pc2u202217485.Shared.Infrastructure.Persistence.EFC.Configuration;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        base.OnConfiguring(builder);
        // Enable Audit Fields Interceptors
        builder.AddCreatedUpdatedInterceptor();
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        // Set the primary key and auto increment
        builder.Entity<PurchaseOrder>().ToTable("purchase_orders");
        builder.Entity<PurchaseOrder>().HasKey(po => po.Id);
        builder.Entity<PurchaseOrder>().Property(po => po.Id).ValueGeneratedOnAdd();
        
        // Place here your entities configurations
        builder.Entity<PurchaseOrder>().Property(po => po.Customer).IsRequired().HasMaxLength(100);
        builder.Entity<PurchaseOrder>().Property(po => po.FabricId).IsRequired();
        builder.Entity<PurchaseOrder>().Property(po => po.Country).IsRequired();
        builder.Entity<PurchaseOrder>().Property(po => po.ResumeUrl).IsRequired();
        builder.Entity<PurchaseOrder>().Property(po => po.Quantity).IsRequired();

        
        // Use Snake Case with Pluralized Table Naming Convention
        builder.UseSnakeCaseWithPluralizedTableNamingConvention();
    }
    
}

```

## Bounded Context Domain/Models

### Aggregates

```csharp
using System.ComponentModel.DataAnnotations;
using EntityFrameworkCore.CreatedUpdatedDate.Contracts;
using si730pc2u202217485.Sale.Domain.Model.Commands;
using si730pc2u202217485.Sale.Domain.Model.ValueObjects;

namespace si730pc2u202217485.Sale.Domain.Model.Aggregates;

public partial class PurchaseOrder : IEntityWithCreatedUpdatedDate
{
    public int Id { get; set; }
    
    [Required]
    public string Customer { get; set; }
    
    [Required]
    public EFabric FabricId { get; set; }
    
    [Required]
    public string Country { get; set; }
    
    [Required]
    public string ResumeUrl { get; set; }
    
    [Required]
    public int Quantity { get; set; }
    
    public DateTimeOffset? CreatedDate { get; set; }
    public DateTimeOffset? UpdatedDate { get; set; }
    
    public PurchaseOrder()
    {
        Customer = string.Empty;
        FabricId = 0;
        Country = string.Empty;
        ResumeUrl = string.Empty;
        Quantity = 0;
    }
    
    // initial constructor
    public PurchaseOrder(string customer, int fabricId, string country, string resumeUrl, int quantity)
    {
        this.Customer = customer;
        this.FabricId = (EFabric)fabricId;
        this.Country = country;
        this.ResumeUrl = resumeUrl;
        this.Quantity = quantity;
    }

    public PurchaseOrder(CreatePurchaseOrderCommand command)
    {
        this.Customer = command.Customer;
        this.FabricId = (EFabric)command.FabricId;
        this.Country = command.Country;
        this.ResumeUrl = command.ResumeUrl;
        this.Quantity = command.Quantity;
    }
    
}

```

#### Audit Creation and Update Date

```csharp

using EntityFrameworkCore.CreatedUpdatedDate.Contracts;

namespace si730pc2u202217485.Sale.Domain.Model.Aggregates;

public partial class PurchaseOrderAudit : IEntityWithCreatedUpdatedDate
{
    public DateTimeOffset? CreatedDate { get; set; }
    public DateTimeOffset? UpdatedDate { get; set; }
}

```
### Value Objects

```csharp

namespace si730pc2u202217485.Sale.Domain.Model.ValueObjects;

public enum EFabric
{
    Algodon = 1,
    Modal = 2,
    Elastano = 3,
    Poliester = 4,
    Nailon = 5,
    Acrylico = 6,
    Rayon = 7,
    Lyocell = 8,
}
```

### Commands

```csharp
namespace si730pc2u202217485.Sale.Domain.Model.Commands;

public record CreatePurchaseOrderCommand(
    string Customer,
    int FabricId,
    string Country,
    string ResumeUrl,
    int Quantity
);
```

## Domain/Repositories

### Repository Interface

```csharp
using si730pc2u202217485.Sale.Domain.Model.Aggregates;
using si730pc2u202217485.Shared.Domain.Model.Repositories;

namespace si730pc2u202217485.Sale.Domain.Repositories;

public interface IPurchaseOrderRepository: IBaseRepository<PurchaseOrder>
{
    Task<bool> ExistsByCustomerAndFabricId(string customer, int fabricId);
}
```

### Command Service Interface

```csharp
using si730pc2u202217485.Sale.Domain.Model.Aggregates;
using si730pc2u202217485.Sale.Domain.Model.Commands;

namespace si730pc2u202217485.Sale.Domain.Services;

public interface IPurchaseOrderCommandService
{
    Task<PurchaseOrder> handle(CreatePurchaseOrderCommand command);
}
```

## Bounded Context Infrastructure/Persistence/EFC/Repositories

### Repository Implementation

```csharp
using Microsoft.EntityFrameworkCore;
using si730pc2u202217485.Sale.Domain.Model.Aggregates;
using si730pc2u202217485.Sale.Domain.Repositories;
using si730pc2u202217485.Shared.Infrastructure.Persistence.EFC.Configuration;
using si730pc2u202217485.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace si730pc2u202217485.Sale.Infrastructure.Persistence.EFC.Repositories;

public class PurchaseOrderRepository(AppDbContext context) : BaseRepository<PurchaseOrder>(context), IPurchaseOrderRepository
{

    public async Task<bool> ExistsByCustomerAndFabricId(string customer, int fabricId)
    {
        return await context.Set<PurchaseOrder>().AnyAsync(po => po.Customer == customer && (int)po.FabricId == fabricId);
    }
}

```
## Bounded Context Application/Internal/CommandService

### Command Service Implementation

```csharp
using si730pc2u202217485.Sale.Domain.Model.Aggregates;
using si730pc2u202217485.Sale.Domain.Model.Commands;
using si730pc2u202217485.Sale.Domain.Model.ValueObjects;
using si730pc2u202217485.Sale.Domain.Repositories;
using si730pc2u202217485.Sale.Domain.Services;
using si730pc2u202217485.Shared.Domain.Model.Repositories;

namespace si730pc2u202217485.Sale.Application.Internal.CommandServices;

public class PurchaseOrderCommandServiceImpl(IPurchaseOrderRepository purchaseOrderRepository, IUnitOfWork unitOfWork)
    : IPurchaseOrderCommandService
{
    
    public async Task<PurchaseOrder> handle(CreatePurchaseOrderCommand command)
    {
        if (await purchaseOrderRepository.ExistsByCustomerAndFabricId(command.Customer, command.FabricId))
        {
            throw new Exception("Purchase order with the same customer and fabric id already exists.");
        }
        
        if (!Enum.IsDefined(typeof(EFabric), command.FabricId))
        {
            throw new Exception("Invalid fabric id.");
        }
        
        var purchaseOrder = new PurchaseOrder(command);
        
        await purchaseOrderRepository.AddAsync(purchaseOrder);
        await unitOfWork.CompleteAsync();
        
        return purchaseOrder;
    }
    
}
```
## Bounded Context Interfaces/REST

### Resources

#### Create Resource

```csharp
namespace si730pc2u202217485.Sale.Interfaces.REST.Resources;

public record CreatePurchaseOrderResource(
    string Customer,
    int FabricId,
    string Country,
    string ResumeUrl,
    int Quantity
);
```

#### Resource 

Remember that this is what will be shown after the creation of the object

```csharp

namespace si730pc2u202217485.Sale.Interfaces.REST.Resources;

public record PurchaseOrderResource(
    int Id,
    string Customer,
    int FabricId,
    string Country,
    string ResumeUrl,
    int Quantity
);
```

### Transforms

#### Create_CommandFromResourceAssembler
```csharp
using si730pc2u202217485.Sale.Domain.Model.Commands;
using si730pc2u202217485.Sale.Interfaces.REST.Resources;

namespace si730pc2u202217485.Sale.Interfaces.REST.Transform;

public class CreatePurchaseOrderCommandFromResourceAssembler
{
    public static CreatePurchaseOrderCommand ToCommandFromResource(CreatePurchaseOrderResource resource)
        => new CreatePurchaseOrderCommand(
            resource.Customer,
            resource.FabricId,
            resource.Country,
            resource.ResumeUrl,
            resource.Quantity
        );
}
```

#### _ResourceFromEntityAssembler

```csharp
using si730pc2u202217485.Sale.Domain.Model.Aggregates;
using si730pc2u202217485.Sale.Interfaces.REST.Resources;

namespace si730pc2u202217485.Sale.Interfaces.REST.Transform;

public class PurchaseOrderResourceFromEntityAssembler
{
    public static PurchaseOrderResource ToResourceFromEntity(PurchaseOrder entity)
        => new PurchaseOrderResource(
            entity.Id,
            entity.Customer,
            (int)entity.FabricId,
            entity.Country,
            entity.ResumeUrl,
            entity.Quantity
        );
}
```

### Controllers

```csharp
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using si730pc2u202217485.Sale.Domain.Services;
using si730pc2u202217485.Sale.Interfaces.REST.Resources;
using si730pc2u202217485.Sale.Interfaces.REST.Transform;

namespace si730pc2u202217485.Sale.Interfaces.REST;

[ApiController]
[Route("api/v1/purchase-orders")]
[Produces(MediaTypeNames.Application.Json)]
public class PurchaseOrderController(IPurchaseOrderCommandService purchaseOrderCommandService) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<IActionResult> CreatePurchaseOrderAsync(CreatePurchaseOrderResource resource)
    {
        var createPurchaseOrderCommand = CreatePurchaseOrderCommandFromResourceAssembler.ToCommandFromResource(resource);
        var purchaseOrder = await purchaseOrderCommandService.handle(createPurchaseOrderCommand);
        var purchaseOrderResource = PurchaseOrderResourceFromEntityAssembler.ToResourceFromEntity(purchaseOrder);

        return StatusCode(201, purchaseOrderResource);
    }    
}

```

### Final Observations

Don't Forget that `AppDbContext` is to be changed along with the `Program.cs` once you already have made all the configurations relating to the Bounded Context.