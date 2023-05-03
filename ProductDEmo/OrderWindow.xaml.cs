using System.ComponentModel;
using System.Windows;

namespace ProductDEmo
{
    public partial class OrderWindow : Window
    {
        public OrderWindow()
        {
            InitializeComponent();
        }

        private void CheckOrder(object sender, RoutedEventArgs e)
        {
            
        }

        private void Close(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}