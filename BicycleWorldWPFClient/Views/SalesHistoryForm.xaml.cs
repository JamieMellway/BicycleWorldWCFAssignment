using System.Windows;
namespace BicycleWorldWPFClient
{
    /// <summary>
    /// Interaction logic for SalesHistoryForm.xaml
    /// </summary>
    public partial class SalesHistoryForm : Window
    {
        public SalesHistoryForm()
        {
            InitializeComponent();
            this.SizeChanged += new SizeChangedEventHandler(ShoppingCartForm_SizeChanged);
        }

        void ShoppingCartForm_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            mainGrid.Height = e.NewSize.Height - 90;
        }
    }
}
