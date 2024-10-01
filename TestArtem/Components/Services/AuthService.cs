using System;
using Microsoft.JSInterop;

namespace TestArtem.Components.Services
{
    public class AuthService
    {
        private bool isAuthenticated;
        private readonly IJSRuntime jsRuntime;

        public AuthService(IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
        }

        public bool IsAuthenticated => isAuthenticated;

        public async Task InitializeAuthenticationStateAsync()
        {
            var authState = await jsRuntime.InvokeAsync<string>("localStorage.getItem", "isAuthenticated");
            isAuthenticated = authState == "true";
        }

        public async Task Login(string username, string password)
        {
            // В реальном приложении проверяйте учетные данные с помощью базы данных или хранилища
            if (username == "test" && password == "test")
            {
                isAuthenticated = true;
                await jsRuntime.InvokeVoidAsync("localStorage.setItem", "isAuthenticated", "true");
            }
            else
            {
                throw new UnauthorizedAccessException("Invalid credentials");
            }
        }

        public async Task Logout()
        {
            isAuthenticated = false;
            await jsRuntime.InvokeVoidAsync("localStorage.removeItem", "isAuthenticated");
        }

        public bool IsUserLoggedIn()
        {
            return isAuthenticated;
        }
    }
}
