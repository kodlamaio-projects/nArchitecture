using NArchitecture.Core.Application.Responses;

namespace Application.Features.Users.Commands.Delete;

public class DeletedUserResponse : IResponse
{
    public int Id { get; set; }
}
