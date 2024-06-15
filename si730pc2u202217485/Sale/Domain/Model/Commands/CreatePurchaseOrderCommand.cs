namespace si730pc2u202217485.Sale.Domain.Model.Commands;

public record CreatePurchaseOrderCommand(
    string Customer,
    int FabricId,
    string Country,
    string ResumeUrl,
    int Quantity
);