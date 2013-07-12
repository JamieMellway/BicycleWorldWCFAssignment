using System;
using System.ServiceModel.Security;
using System.Windows;
using BicycleWorldClientSession;
using BicycleWorldServiceModelClient.BicycleWorldService;


namespace BicycleWorldWPFClient
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoginUser.Current.Reset();
            string username = txtUsername.Text;
            string password = txtPassword.Password;

            PermissiveCertificatePolicy.Enact("CN=HTTPS-Server");
            BicycleWorldSalesServiceClient proxy = new BicycleWorldSalesServiceClient("wsHttpBinding_BicycleWorldSalesService");
            proxy.ClientCredentials.UserName.UserName = username;
            proxy.ClientCredentials.UserName.Password = password;
            try
            {
                proxy.Login();
                bool isAdmin = proxy.IsUserAdmin();
                LoginUser.Current.Set(username, password, isAdmin);
                DialogResult = true;
                Authenicated = true;
                this.Close();
            }
            catch (MessageSecurityException ex)
            {
                MessageBox.Show((ex.InnerException != null) ? ex.InnerException.Message : "Login failed.");
                DialogResult = true;
                Authenicated = false;
            }
            catch (Exception)
            {
                MessageBox.Show("Login failed.");
                DialogResult = true;
                Authenicated = false;
            }
        }

        public bool Authenicated { get; set; }
    }
}
