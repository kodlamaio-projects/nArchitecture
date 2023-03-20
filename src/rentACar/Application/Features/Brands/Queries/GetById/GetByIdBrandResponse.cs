using Core.Application.Responses;

namespace Application.Features.Brands.Queries.GetById;

public class GetByIdBrandResponse : IResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
}
