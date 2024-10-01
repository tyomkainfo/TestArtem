using System;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace TestArtem.Components.Services
{
    public class AuthService
    {
        private bool isAuthenticated;
        private readonly IJSRuntime _jsRuntime;

        public bool IsAuthenticated => isAuthenticated;

        public AuthService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task LoadAuthenticationStateAsync()
        {
            // Получаем значение из localStorage только на клиенте
            try
            {
                var value = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "isAuthenticated");
                isAuthenticated = !string.IsNullOrEmpty(value) && bool.Parse(value);
            }
            catch (Exception)
            {
                isAuthenticated = false; // В случае ошибки считаем пользователя неавторизованным
            }
        }

        public void Login(string username, string password)
        {
            if (username == "test" && password == "test")
            {
                isAuthenticated = true;
                SaveAuthenticationState();
            }
            else
            {
                throw new UnauthorizedAccessException("Invalid credentials");
            }
        }

        public void Logout()
        {
            isAuthenticated = false;
            ClearAuthenticationState();
        }

        private void SaveAuthenticationState()
        {
            _jsRuntime.InvokeVoidAsync("localStorage.setItem", "isAuthenticated", true);
        }

        private void ClearAuthenticationState()
        {
            _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "isAuthenticated");
        }
    }
}
