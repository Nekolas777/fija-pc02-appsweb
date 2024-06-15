using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using si730pc2u202217485.Sale.Domain.Services;
using si730pc2u202217485.Sale.Interfaces.REST.Resources;
using si730pc2u202217485.Sale.Interfaces.REST.Transform;

namespace si730pc2u202217485.Sale.Interfaces.REST;

[ApiController]
[Route("api/v1/purchase-orders")]
[Produces(MediaTypeNames.Application.Json)]
public class PurchaseOrderController(IPurchaseOrderCommandService purchaseOrderCommandService) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<IActionResult> CreatePurchaseOrderAsync(CreatePurchaseOrderResource resource)
    {
        var createPurchaseOrderCommand = CreatePurchaseOrderCommandFromResourceAssembler.ToCommandFromResource(resource);
        var purchaseOrder = await purchaseOrderCommandService.handle(createPurchaseOrderCommand);
        var purchaseOrderResource = PurchaseOrderResourceFromEntityAssembler.ToResourceFromEntity(purchaseOrder);

        return StatusCode(201, purchaseOrderResource);
    }    
}
