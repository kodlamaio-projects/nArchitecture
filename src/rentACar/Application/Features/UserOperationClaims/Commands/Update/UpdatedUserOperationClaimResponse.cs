using Core.Application.Dtos;

namespace Application.Features.UserOperationClaims.Commands.Update;

public class UpdatedUserOperationClaimResponse : IDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int OperationClaimId { get; set; }
}
