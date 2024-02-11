using NArchitecture.Core.Application.Responses;

namespace Application.Features.UserOperationClaims.Commands.Update;

public class UpdatedUserOperationClaimResponse : IResponse
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int OperationClaimId { get; set; }
}
