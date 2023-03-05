using Core.Application.Dtos;

namespace Application.Features.OperationClaims.Commands.Create;

public class CreatedOperationClaimResponse : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}
