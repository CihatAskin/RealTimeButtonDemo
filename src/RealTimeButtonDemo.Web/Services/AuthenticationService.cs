using Microsoft.JSInterop;

namespace RealTimeButtonDemo.Web.Services;

public class AuthenticationService
{
    private readonly IJSRuntime _jsRuntime;

    public AuthenticationService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task<bool> IsAuthenticatedAsync()
    {
        try
        {
            var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "auth_token");
            return !string.IsNullOrEmpty(token);
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
            return await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "auth_token");
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
            return await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "username");
        }
        catch
        {
            return null;
        }
    }

    public async Task LogoutAsync()
    {
        try
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "auth_token");
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "username");
        }
        catch
        {
            // Ignore errors
        }
    }
}