namespace Simbir.GO.Domain.Common.Entity;


/// <summary>
/// Represents the base class for entities
/// </summary>
public abstract class BaseEntity : IEquatable<BaseEntity>
{
    public long Id { get;}

    protected BaseEntity(long id)
    {
        Id = id;
    }
    
    public bool Equals(BaseEntity? other) =>
        Equals((object?)other);

    public override bool Equals(object? obj) =>
        obj is BaseEntity entity && Id.Equals(entity.Id);

    public override int GetHashCode() =>
        Id.GetHashCode();

    public static bool operator ==(BaseEntity left, BaseEntity right) =>
        Equals(left, right);
    
    public static bool operator !=(BaseEntity left, BaseEntity? right) =>
        !Equals(left, right);
        
#pragma warning disable CS8618
    protected BaseEntity() { }
#pragma warning disable CS8618
}