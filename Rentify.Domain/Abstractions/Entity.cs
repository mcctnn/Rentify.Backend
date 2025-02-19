namespace Rentify.Domain.Abstractions;
public abstract class Entity
{
    public Guid Id { get; set; }
    public Entity()
    {
        Id = Guid.CreateVersion7();
    }
}
