using Core.Application.Responses;

namespace Application.Features.OperationClaims.Commands.Update;

public class UpdatedOperationClaimResponse : IResponse
{
    public int Id { get; set; }
    public string Name { get; set; }

    public UpdatedOperationClaimResponse()
    {
        Name = string.Empty;
    }

    public UpdatedOperationClaimResponse(int id, string name)
    {
        Id = id;
        Name = name;
    }
}
