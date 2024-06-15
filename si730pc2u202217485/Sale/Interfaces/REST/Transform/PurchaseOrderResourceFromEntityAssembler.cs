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