using NArchitecture.Core.Application.Responses;

namespace Application.Features.UserOperationClaims.Commands.Delete;

public class DeletedUserOperationClaimResponse : IResponse
{
    public Guid Id { get; set; }
}
