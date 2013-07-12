using System;
using System.Windows;
using BicycleWorldClientSession;

namespace BicycleWorldWPFClient
{
    /// <summary>
    /// Interaction logic for ShoppingCartForm.xaml
    /// </summary>
    public partial class ShoppingCartForm : Window
    {
        public ShoppingCartForm()
        {
            InitializeComponent();
            this.Closed += new EventHandler(ShoppingCartForm_Closed);
        }

        void ShoppingCartForm_Closed(object sender, EventArgs e)
        {
            UserSession.Current.CloseSession();            
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Authenticated = false;
            this.Close();
        }

        public bool? Authenticated { get; set; }

        SalesHistoryForm salesHistoryForm;
        private void SalesHistory_Click(object sender, RoutedEventArgs e)
        {
            salesHistoryForm = new SalesHistoryForm();
            salesHistoryForm.Show();
        }

        AdminForm adminForm;
        private void Admin_Click(object sender, RoutedEventArgs e)
        {
            adminForm = new AdminForm();
            adminForm.Show();
        }
    }
}
