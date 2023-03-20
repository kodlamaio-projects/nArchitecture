using Core.Application.Responses;

namespace Application.Features.Colors.Commands.Update;

public class UpdatedColorResponse : IResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
}
