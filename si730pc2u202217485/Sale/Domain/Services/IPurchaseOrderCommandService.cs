using si730pc2u202217485.Sale.Domain.Model.Aggregates;
using si730pc2u202217485.Sale.Domain.Model.Commands;

namespace si730pc2u202217485.Sale.Domain.Services;

public interface IPurchaseOrderCommandService
{
    Task<PurchaseOrder> handle(CreatePurchaseOrderCommand command);
}