using Core.Application.Dtos;

namespace Application.Features.Brands.Dtos;

public class BrandListDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}