namespace Simbir.GO.Domain.Common.Entity;


public class AggregateRoot : BaseEntity
{
    protected AggregateRoot(long id) : base(id) {}

    protected AggregateRoot() {}
}