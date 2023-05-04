namespace WebAPI.Controllers.Dtos;

public class UpdateByAuthFromServiceRequestDto
{
    public string IdentityNumber { get; set; }

    public UpdateByAuthFromServiceRequestDto()
    {
        IdentityNumber = string.Empty;
    }

    public UpdateByAuthFromServiceRequestDto(string identityNumber)
    {
        IdentityNumber = identityNumber;
    }
}
