using Ardalis.Specification.EntityFrameworkCore;
using Simbir.GO.Server.ApplicationCore.Interfaces;

namespace Simbir.GO.Server.Infrastructure.Persistence;

public class EfRepository<T>: RepositoryBase<T>, IRepository<T> where T : class
{
    private readonly AppDbContext _dbContext;

    public EfRepository(AppDbContext dbContext) : base(dbContext)
        => _dbContext = dbContext;

    // Not required to implement anything. Add additional functionalities if required.
}