using System;
using System.ServiceModel;
using System.Windows;
using BicycleWorldServiceLibrary;

namespace ProductsServiceHost
{
    /// <summary>
    /// Interaction logic for HostController.xaml
    /// </summary>
    public partial class HostController : Window
    {
        private ServiceHost productsServiceHost1;
        private ServiceHost productsServiceHost2;

        public HostController()
        {
            InitializeComponent();
            start1_Click(null, null);
            start2_Click(null, null);
        }

        private void handleException(Exception ex)
        {
            MessageBox.Show(ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void start1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                productsServiceHost1 = new ServiceHost(typeof(BicycleWorldService));
                productsServiceHost1.Open();
                stop1.IsEnabled = true;
                start1.IsEnabled = false;
                status1.Text = "Service Running";
            }
            catch (Exception ex)
            {
                handleException(ex);
            }
        }

        private void start2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                productsServiceHost2 = new ServiceHost(typeof(BicycleWorldSalesService));
                productsServiceHost2.Open();
                stop2.IsEnabled = true;
                start2.IsEnabled = false;
                status2.Text = "Service Running";
            }
            catch (Exception ex)
            {
                handleException(ex);
            }
        }


        private void stop1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                productsServiceHost1.Close();
                stop1.IsEnabled = false;
                start1.IsEnabled = true;
                status1.Text = "Service Stopped";
            }
            catch (Exception ex)
            {
                handleException(ex);
            }
        }
        private void stop2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                productsServiceHost2.Close();
                stop2.IsEnabled = false;
                start2.IsEnabled = true;
                status2.Text = "Service Stopped";
            }
            catch (Exception ex)
            {
                handleException(ex);
            }
        }
    }
}
