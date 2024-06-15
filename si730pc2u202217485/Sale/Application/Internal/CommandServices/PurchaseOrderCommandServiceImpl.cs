using si730pc2u202217485.Sale.Domain.Model.Aggregates;
using si730pc2u202217485.Sale.Domain.Model.Commands;
using si730pc2u202217485.Sale.Domain.Model.ValueObjects;
using si730pc2u202217485.Sale.Domain.Repositories;
using si730pc2u202217485.Sale.Domain.Services;
using si730pc2u202217485.Shared.Domain.Model.Repositories;

namespace si730pc2u202217485.Sale.Application.Internal.CommandServices;

public class PurchaseOrderCommandServiceImpl(IPurchaseOrderRepository purchaseOrderRepository, IUnitOfWork unitOfWork)
    : IPurchaseOrderCommandService
{
    
    public async Task<PurchaseOrder> handle(CreatePurchaseOrderCommand command)
    {
        if (await purchaseOrderRepository.ExistsByCustomerAndFabricId(command.Customer, command.FabricId))
        {
            throw new Exception("Purchase order with the same customer and fabric id already exists.");
        }
        
        if (!Enum.IsDefined(typeof(EFabric), command.FabricId))
        {
            throw new Exception("Invalid fabric id.");
        }
        
        var purchaseOrder = new PurchaseOrder(command);
        
        await purchaseOrderRepository.AddAsync(purchaseOrder);
        await unitOfWork.CompleteAsync();
        
        return purchaseOrder;
    }
    
}