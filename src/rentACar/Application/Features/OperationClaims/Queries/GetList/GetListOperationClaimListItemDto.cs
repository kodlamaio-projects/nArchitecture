using Core.Application.Dtos;

namespace Application.Features.OperationClaims.Queries.GetList;

public class GetListOperationClaimListItemDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}
