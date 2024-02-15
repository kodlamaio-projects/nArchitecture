namespace Domain.Entities;

public class EmailAuthenticator : NArchitecture.Core.Security.Entities.EmailAuthenticator<Guid>
{
    public virtual User User { get; set; } = default!;
}
