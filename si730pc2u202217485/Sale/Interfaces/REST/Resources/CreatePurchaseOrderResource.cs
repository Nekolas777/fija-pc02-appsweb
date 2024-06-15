namespace si730pc2u202217485.Sale.Interfaces.REST.Resources;

public record CreatePurchaseOrderResource(
    string Customer,
    int FabricId,
    string Country,
    string ResumeUrl,
    int Quantity
);