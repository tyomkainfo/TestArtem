using System;

namespace TestArtem.Components.Services
{
    public class AuthService
    {
        private bool isAuthenticated;

        public bool IsAuthenticated => isAuthenticated;

        public void Login(string username, string password)
        {
            // В реальном приложении проверяйте учетные данные с помощью базы данных или хранилища
            if (username == "test" && password == "test")
            {
                isAuthenticated = true;
            }
            else
            {
                throw new UnauthorizedAccessException("Invalid credentials"); // Более специфичное исключение
            }
        }

        public void Logout()
        {
            isAuthenticated = false;
        }

        public bool IsUserLoggedIn()
        {
            return isAuthenticated;
        }
    }
}
