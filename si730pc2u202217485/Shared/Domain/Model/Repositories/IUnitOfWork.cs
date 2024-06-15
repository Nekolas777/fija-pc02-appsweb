namespace si730pc2u202217485.Shared.Domain.Model.Repositories;

public interface IUnitOfWork
{
    Task CompleteAsync();
}