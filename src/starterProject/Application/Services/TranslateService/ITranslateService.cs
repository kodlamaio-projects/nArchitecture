namespace Application.Services.TranslateService;
public interface ITranslateService
{
    public Task<string> TranslateAsync(string text, string? to = null, string from = "en");
}
