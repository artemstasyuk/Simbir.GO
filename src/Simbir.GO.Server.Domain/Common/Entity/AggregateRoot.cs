namespace Simbir.GO.Server.Domain.Common.Entity;


public class AggregateRoot : BaseEntity
{
    protected AggregateRoot(long id) : base(id) {}

    protected AggregateRoot() {}
}