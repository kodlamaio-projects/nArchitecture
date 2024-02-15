namespace Domain.Entities;

public class OtpAuthenticator : NArchitecture.Core.Security.Entities.OtpAuthenticator<Guid>
{
    public virtual User User { get; set; } = default!;
}
