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