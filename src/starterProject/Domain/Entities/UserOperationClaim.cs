namespace Domain.Entities;

public class UserOperationClaim : NArchitecture.Core.Security.Entities.UserOperationClaim<Guid, int>
{
    public virtual User User { get; set; } = default!;
    public virtual OperationClaim OperationClaim { get; set; } = default!;
}
