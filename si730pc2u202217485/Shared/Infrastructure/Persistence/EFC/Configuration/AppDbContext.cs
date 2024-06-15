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