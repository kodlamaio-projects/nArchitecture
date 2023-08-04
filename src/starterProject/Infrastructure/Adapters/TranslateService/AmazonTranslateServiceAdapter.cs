using Amazon;
using Amazon.Translate;
using Amazon.Translate.Model;
using Application.Services.TranslateService;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Adapters.TranslateService;
public class AmazonTranslateServiceAdapter : ITranslateService
{
    private readonly IConfiguration _configuration;
    private readonly AmazonTranslateClient _client;
    private readonly string _requestLang;

    public AmazonTranslateServiceAdapter(
        IConfiguration configuration,
        IHttpContextAccessor httpContextAccessor
        )
    {
        _requestLang = httpContextAccessor.HttpContext.Request.GetTypedHeaders()
            .AcceptLanguage
            ?.OrderByDescending(x => x.Quality ?? 1)
            .Select(x => x.Value.ToString())
            .ToArray().FirstOrDefault() ?? "en";

        _configuration = configuration;

        string accessKey = _configuration.GetSection("AmazonConfiguration").GetSection("AccessKey").Value ?? "";
        string secretKey = _configuration.GetSection("AmazonConfiguration").GetSection("SecretKey").Value ?? "";
        _client = new AmazonTranslateClient(accessKey, secretKey, RegionEndpoint.USEast1);
    }
    public async Task<string> TranslateAsync(string text, string? to = null, string from = "en")
    {
        if (to == null)
        {
            to = _requestLang;
        }

        var request = new TranslateTextRequest
        {
            SourceLanguageCode = from,
            TargetLanguageCode = to,
            Text = text,
        };

        var response = await _client.TranslateTextAsync(request);

        return response.TranslatedText;
    }
}
