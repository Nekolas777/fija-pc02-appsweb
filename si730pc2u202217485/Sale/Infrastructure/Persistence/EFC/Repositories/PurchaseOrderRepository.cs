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
