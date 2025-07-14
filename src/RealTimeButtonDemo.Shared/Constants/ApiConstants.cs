namespace RealTimeButtonDemo.Shared.Constants;

public static class ApiConstants
{
    public const string DefaultBaseUrl = "http://localhost:5293";
    public const string DefaultHubUrl = "http://localhost:5293/buttonhub";
    
    // Auth endpoints
    public const string AuthLoginEndpoint = "/api/auth/login";
    
    // Button endpoints
    public const string ButtonStateEndpoint = "/api/button/state";
    
    // SignalR Hub path
    public const string HubPath = "/buttonhub";
    
    // Build hub URL from base URL
    public static string GetHubUrl(string baseUrl) => $"{baseUrl.TrimEnd('/')}{HubPath}";
}