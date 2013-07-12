using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace BicycleWorldWPFClient.Views
{
    /// <summary>
    /// Interaction logic for ProductsAdmin.xaml
    /// </summary>
    public partial class ProductsAdmin : UserControl
    {
        public ProductsAdmin()
        {
            InitializeComponent();
        }

        private void ListPrice_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.-]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Quantity_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
