using Ardalis.Specification;

namespace Simbir.GO.Server.ApplicationCore.Interfaces.Persistence;

public interface IRepository<T> : IRepositoryBase<T> where T : class
{
}
 
/// <inheritdoc/>
/// Repository for queries to read from db, it's also caching in memory
public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class
{
}