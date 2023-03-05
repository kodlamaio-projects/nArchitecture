using Application.Services.FindeksService;

namespace Infrastructure.Adapters.FakeFindeksService;

public class FakeFindeksServiceAdapter : IFindeksService
{
    public short GetScore(string identityNumber)
    {
        Random random = new();
        short score = Convert.ToInt16(random.Next(1900));
        return score;
    }
}
