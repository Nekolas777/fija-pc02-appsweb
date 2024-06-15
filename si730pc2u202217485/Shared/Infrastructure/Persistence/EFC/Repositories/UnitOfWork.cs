using si730pc2u202217485.Shared.Domain.Model.Repositories;
using si730pc2u202217485.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace si730pc2u202217485.Shared.Infrastructure.Persistence.EFC.Repositories;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public async Task CompleteAsync() => await context.SaveChangesAsync();
}