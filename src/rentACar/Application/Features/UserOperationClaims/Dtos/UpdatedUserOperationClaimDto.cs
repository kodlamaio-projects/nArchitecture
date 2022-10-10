namespace Application.Features.UserOperationClaims.Dtos;

public class UpdatedUserOperationClaimDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int OperationClaimId { get; set; }
}