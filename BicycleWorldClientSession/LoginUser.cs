
namespace BicycleWorldClientSession
{
    public sealed class LoginUser
    {
        private static readonly LoginUser current = new LoginUser();
        static LoginUser() { }
        private LoginUser() { }
        public static LoginUser Current { get { return current; } }

        public void Reset()
        {
            Username = null;
            Password = null;
        }

        public void Set(string username, string password, bool isAdmin)
        {
            Username = username;
            Password = password;
            IsUserAdmin = isAdmin;
        }

        public string Username { get; set; }
        public string Password { get; internal set; }
        public bool IsUserAdmin{get; set;}
    }

}
