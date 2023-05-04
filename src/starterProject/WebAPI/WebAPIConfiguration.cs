namespace WebAPI;

public class WebApiConfiguration
{
    public string ApiDomain { get; set; }
    public string[] AllowedOrigins { get; set; }

    public WebApiConfiguration()
    {
        ApiDomain = string.Empty;
        AllowedOrigins = Array.Empty<string>();
    }

    public WebApiConfiguration(string apiDomain, string[] allowedOrigins)
    {
        ApiDomain = apiDomain;
        AllowedOrigins = allowedOrigins;
    }
}
