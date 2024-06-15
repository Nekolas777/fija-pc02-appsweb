namespace si730pc2u202217485.Sale.Interfaces.REST.Resources;

public record PurchaseOrderResource(
    int Id,
    string Customer,
    int FabricId,
    string Country,
    string ResumeUrl,
    int Quantity
);