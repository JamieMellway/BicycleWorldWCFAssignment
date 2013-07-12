using System.Windows;
using BicycleWorldWPFClient.Views;

namespace BicycleWorldWPFClient
{
    /// <summary>
    /// Interaction logic for AdminForm.xaml
    /// </summary>
    public partial class AdminForm : Window
    {
        public AdminForm()
        {
            InitializeComponent();
        }

        void ShoppingCartForm_SizeChanged(object sender, SizeChangedEventArgs e)
        {
        }

        private void Categories_Click(object sender, RoutedEventArgs e)
        {
            contentControl.Content = new CatagoriesAdmin();
        }

        private void Products_Click(object sender, RoutedEventArgs e)
        {
            contentControl.Content = new ProductsAdmin();
        }
    }
}
