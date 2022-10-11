using System.Globalization;
using Application.Features.IndividualCustomers.Dtos;
using Application.Services.PersonService;
using Infrastructure.KPSPublic;

namespace Infrastructure.Adapters.PersonService;

public class PersonServiceAdapter : IPersonService
{
    public async Task<bool> VerifyNationalId(CitizenDto citizenDto)
    {
        return await Verify(citizenDto);
    }

    private static async Task<bool> Verify(CitizenDto citizenDto)
    {
        var locale = new CultureInfo("tr-TR", false);
        var kps = new KPSPublicSoapClient(KPSPublicSoapClient.EndpointConfiguration.KPSPublicSoap);
        
        {
            var response = await kps.TCKimlikNoDogrulaAsync(
                Convert.ToInt64(citizenDto.NationalIdentity),
                citizenDto.FirstName.ToUpper(locale),
                citizenDto.LastName.ToUpper(locale),
                citizenDto.BirthDay.Year);
            
            return response.Body.TCKimlikNoDogrulaResult;
        }
    }
}