using Core.Application.Dtos;

namespace Application.Features.OperationClaims.Dtos;

public class DeletedOperationClaimDto : IDto
{
    public int Id { get; set; }
}