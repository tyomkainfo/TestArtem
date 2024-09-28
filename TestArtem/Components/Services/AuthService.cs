using System;

namespace TestArtem.Components.Services
{
    public class AuthService
    {
        private bool isAuthenticated;

        public bool IsAuthenticated => isAuthenticated;

        public void Login(string username, string password)
        {
            if (username == "test" && password == "test")
            {
                isAuthenticated = true;
            }
            else
            {
                throw new Exception("Invalid credentials");
            }
        }

        public void Logout()
        {
            isAuthenticated = false;
        }
    }
}