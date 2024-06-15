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