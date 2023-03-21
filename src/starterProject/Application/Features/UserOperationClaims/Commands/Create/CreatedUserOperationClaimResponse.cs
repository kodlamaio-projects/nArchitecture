using Core.Application.Responses;

namespace Application.Features.UserOperationClaims.Commands.Create;

public class CreatedUserOperationClaimResponse : IResponse
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int OperationClaimId { get; set; }
}
