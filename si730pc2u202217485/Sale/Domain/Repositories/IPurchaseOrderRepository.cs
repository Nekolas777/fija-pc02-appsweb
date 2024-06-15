using si730pc2u202217485.Sale.Domain.Model.Aggregates;
using si730pc2u202217485.Shared.Domain.Model.Repositories;

namespace si730pc2u202217485.Sale.Domain.Repositories;

public interface IPurchaseOrderRepository: IBaseRepository<PurchaseOrder>
{
    Task<bool> ExistsByCustomerAndFabricId(string customer, int fabricId);
}