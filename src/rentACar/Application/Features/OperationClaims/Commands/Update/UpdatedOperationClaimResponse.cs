using Core.Application.Dtos;

namespace Application.Features.OperationClaims.Commands.Update;

public class UpdatedOperationClaimResponse : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}