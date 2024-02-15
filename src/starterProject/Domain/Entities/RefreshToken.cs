namespace Domain.Entities;

public class RefreshToken : NArchitecture.Core.Security.Entities.RefreshToken<Guid>
{
    public virtual User User { get; set; } = default!;
}
