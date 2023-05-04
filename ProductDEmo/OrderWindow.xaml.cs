using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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

        private void Minus(object sender, RoutedEventArgs e)
        {
            OrderProduct orderProductToRemove = (sender as Button).DataContext as OrderProduct;
            List<OrderProduct> orderProductCollection = MainWindow.db.Order
                .Where(order => order.OrderID == UserProductWindow.currentOrder.OrderID).FirstOrDefault().OrderProduct
                .ToList();
    
                MessageBox.Show("FindedThisOrder");
                if (orderProductToRemove.Count - 1 == 0)
                {
                    MessageBox.Show("LastCount");
                    MainWindow.db.OrderProduct.Remove(orderProductToRemove);
                    MainWindow.db.SaveChanges();
                }
                else
                {
                    MessageBox.Show("PrevCount");
                    orderProductToRemove.Count -= 1;
                    MainWindow.db.SaveChanges();
                }
                MainWindow.userProductWindow.UpdateOrderList();
            MessageBox.Show("Продукт был удалён");
 
        }
    }
}