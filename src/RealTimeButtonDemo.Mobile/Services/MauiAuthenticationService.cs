using System.Text.Json;
using RealTimeButtonDemo.Shared.DTOs;

namespace RealTimeButtonDemo.Mobile.Services;

public class MauiAuthenticationService
{
    private readonly HttpClient _httpClient;
    private const string TokenKey = "auth_token";
    private const string UsernameKey = "username";
    private const string ExpirationKey = "token_expiration";

    public MauiAuthenticationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> IsAuthenticatedAsync()
    {
        try
        {
            var token = await SecureStorage.GetAsync(TokenKey);
            if (string.IsNullOrEmpty(token))
                return false;

            var expirationString = await SecureStorage.GetAsync(ExpirationKey);
            if (DateTime.TryParse(expirationString, out var expiration))
            {
                if (DateTime.UtcNow >= expiration)
                {
                    await LogoutAsync();
                    return false;
                }
            }

            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<string?> GetTokenAsync()
    {
        try
        {
            var token = await SecureStorage.GetAsync(TokenKey);
            
            if (!string.IsNullOrEmpty(token))
            {
                var expirationString = await SecureStorage.GetAsync(ExpirationKey);
                if (DateTime.TryParse(expirationString, out var expiration))
                {
                    if (DateTime.UtcNow >= expiration)
                    {
                        await LogoutAsync();
                        return null;
                    }
                }
            }
            
            return token;
        }
        catch
        {
            return null;
        }
    }

    public async Task<string?> GetUsernameAsync()
    {
        try
        {
            return await SecureStorage.GetAsync(UsernameKey);
        }
        catch
        {
            return null;
        }
    }

    public async Task<LoginResult> LoginAsync(string username, string password, string apiBaseUrl)
    {
        try
        {
            var loginRequest = new LoginRequest
            {
                Username = username,
                Password = password
            };

            var json = JsonSerializer.Serialize(loginRequest);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{apiBaseUrl}/api/auth/login", content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var loginResponse = JsonSerializer.Deserialize<LoginResponse>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (loginResponse != null)
                {
                    await SecureStorage.SetAsync(TokenKey, loginResponse.Token);
                    await SecureStorage.SetAsync(UsernameKey, loginResponse.Username);
                    await SecureStorage.SetAsync(ExpirationKey, loginResponse.ExpiresAt.ToString());

                    return new LoginResult { Success = true, Token = loginResponse.Token, Username = loginResponse.Username };
                }
            }

            return new LoginResult { Success = false, Error = "Invalid credentials" };
        }
        catch (Exception ex)
        {
            return new LoginResult { Success = false, Error = ex.Message };
        }
    }

    public async Task LogoutAsync()
    {
        try
        {
            SecureStorage.Remove(TokenKey);
            SecureStorage.Remove(UsernameKey);
            SecureStorage.Remove(ExpirationKey);
        }
        catch
        {
            // Ignore errors during logout
        }
    }
}

public class LoginResult
{
    public bool Success { get; set; }
    public string? Token { get; set; }
    public string? Username { get; set; }
    public string? Error { get; set; }
}