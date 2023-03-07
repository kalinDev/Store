namespace Core.DomainObjects.Interfaces;

public interface IUnitOfWork
{
    Task<bool> Commit();
}