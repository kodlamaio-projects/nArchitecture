using Core.Application.Dtos;

namespace Application.Features.OperationClaims.Queries.GetById;

public class GetByIdOperationClaimResponse : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}