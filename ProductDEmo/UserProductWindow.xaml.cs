using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ProductDEmo
{
    public partial class UserProductWindow : Window
    {
        public static bool CurrentOrderExists = false;
        public static Order currentOrder = new Order();
        public static List<Product> productSortedList = MainWindow.db.Product.ToList();
        public UserProductWindow()
        {
            InitializeComponent();
        }
         private void BackClick(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow.mainWindow.Show();
        }

         public void ShowOrderButton()
         {
           
         }

        private void TextChange(object sender, TextChangedEventArgs e)
        {
            AddAllFilters();
        }
        public void AddAllFilters()
        {
            productSortedList = productSortedList.Where(prod => prod.ProductName.StartsWith(FindTextBox.Text)).ToList();

            if (SortComboBox.SelectedIndex == 0)
            {
                productSortedList = productSortedList.OrderByDescending(prod => prod.ProductCost).ToList();
            }
            if (SortComboBox.SelectedIndex == 1)
            {
                productSortedList = productSortedList.OrderBy(prod => prod.ProductCost).ToList();
            }

            if (SortByDiscount.SelectedIndex == 1)
            {
                productSortedList = productSortedList.Where(prod => prod.ProductDiscountAmount > 0 && prod.ProductDiscountAmount < 10).ToList();
            }
            if (SortByDiscount.SelectedIndex == 2)
            {
                productSortedList = productSortedList.Where(prod => prod.ProductDiscountAmount > 9.99 && prod.ProductDiscountAmount < 15).ToList();
            }
            if (SortByDiscount.SelectedIndex == 3)
            {
                productSortedList = productSortedList.Where(prod => prod.ProductDiscountAmount > 14.99).ToList();
            }
            ProductListView.ItemsSource = productSortedList;
            InfoTextBox.Text = "Количество: " + productSortedList.Count + "/" + MainWindow.db.Product.ToList().Count;
            productSortedList = MainWindow.db.Product.ToList();
            ChangeColorIfDiscountLargerThan15();
        }
        private void ChangeSelection(object sender, SelectionChangedEventArgs e)
        {
            AddAllFilters();
        }

        private void ChangeSelectionOfDiscountComboBox(object sender, SelectionChangedEventArgs e)
        {
            AddAllFilters();
        }
        public void ChangeColorIfDiscountLargerThan15()
        {
            for (int i = 0; i < ProductListView.Items.Count; i++)
            {
                if ((ProductListView.Items[i] as Product).ProductDiscountAmount > 15)
                {
                    var container = ProductListView.ItemContainerGenerator.ContainerFromIndex(i) as ListViewItem;
                    if (container != null)
                    {
                        int b = VisualTreeHelper.GetChildrenCount(container);
                        for (int j = 0; j < VisualTreeHelper.GetChildrenCount(container); j++)
                        {
                            Grid grid = (Grid)VisualTreeHelper.GetChild(((VisualTreeHelper.GetChild(container, j) as Border).Child as ContentPresenter), 0);
                            grid.Background = new SolidColorBrush(Color.FromRgb(127, 255, 0));
                        }
                    }
                }
            }
        }
        private void ClickDOubleCheck(object sender, MouseButtonEventArgs e)
        {
            ChangeColorIfDiscountLargerThan15();
        }

        private void CheckOrder(object sender, RoutedEventArgs e)
        {
            MainWindow.orderWindow.Show();
            //MainWindow.orderWindow.OrderListView.ItemsSource = MainWindow.db.Order.Where(order=> order.OrderID==currentOrder.OrderID).FirstOrDefault().OrderProduct;
        }

        public void AddCurrentOrderToDatabase()
        {
            currentOrder.OrderStatusID = 1;
            currentOrder.PickupPointID = 1;
            currentOrder.OrderCreateDate=DateTime.Now;
            currentOrder.OrderDeliveryDate=DateTime.Now;
            currentOrder.User = MainWindow.CurrentUser;
            currentOrder.OrderGetCode = 5555;
            MainWindow.db.Order.Add(currentOrder);
            MainWindow.db.SaveChanges();
            CurrentOrderExists = true;
            MessageBox.Show("Order added");
        }
        private void AddToOrderClick(object sender, RoutedEventArgs e)
        {
            if (!CurrentOrderExists)
            {
                AddCurrentOrderToDatabase();
            }
            Product product = (sender as MenuItem).DataContext as Product;
           // MessageBox.Show(product.ProductName);
            OrderProduct orderProduct = new OrderProduct();
            orderProduct.Product = product;
            orderProduct.Order = MainWindow.db.Order.Where(order=> order.OrderID==currentOrder.OrderID).FirstOrDefault();
            orderProduct.Count = 1;
            MainWindow.db.OrderProduct.Add(orderProduct);
            MainWindow.db.SaveChanges();
            MainWindow.orderWindow.OrderListView.ItemsSource = MainWindow.db.Order.Where(order=> order.OrderID==currentOrder.OrderID).FirstOrDefault().OrderProduct.ToList();
           // MainWindow.orderWindow.OrderListView.ItemsSource = MainWindow.db.Order.ToList();
            MessageBox.Show("CountOfProducts: " + MainWindow.db.Order
                .Where(order => order.OrderID == currentOrder.OrderID).FirstOrDefault().OrderProduct.Count);
            MessageBox.Show("ItemCountINlist: " + MainWindow.orderWindow.OrderListView.Items.Count);
            if (MainWindow.db.Order
                    .Where(order => order.OrderID == currentOrder.OrderID).FirstOrDefault().OrderProduct.Count > 1)
            {
                MessageBox.Show("Второй айтем: " +
                                (MainWindow.orderWindow.OrderListView.Items[1] as OrderProduct).Product.ProductName);
            }
            MessageBox.Show("К заказу был добавлен продукт");
            // order.Product = 
        }
    }
}