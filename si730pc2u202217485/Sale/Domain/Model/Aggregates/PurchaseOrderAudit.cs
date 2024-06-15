using EntityFrameworkCore.CreatedUpdatedDate.Contracts;

namespace si730pc2u202217485.Sale.Domain.Model.Aggregates;

public partial class PurchaseOrderAudit : IEntityWithCreatedUpdatedDate
{
    public DateTimeOffset? CreatedDate { get; set; }
    public DateTimeOffset? UpdatedDate { get; set; }
}