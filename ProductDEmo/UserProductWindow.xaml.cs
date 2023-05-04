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
            currentOrder.OrderGetCode = 555;
            MainWindow.db.Order.Add(currentOrder);
            MainWindow.db.SaveChanges();
            CurrentOrderExists = true;
            MessageBox.Show("Order added");
        }

        public void AddProductToOrderWithCounting(Product product)
        {
            Order order = MainWindow.db.Order.Where(ord=> ord.OrderID==currentOrder.OrderID).FirstOrDefault();
            OrderProduct copy = order.OrderProduct.Where(ord => ord.Product == product).FirstOrDefault();
            if (copy != null)
            {
                copy.Count++;
                MainWindow.db.SaveChanges();
                //MessageBox.Show("Copied: " + copy.Product.ProductName);
            }
            else
            {
               // MessageBox.Show("New product: " + product.ProductName);
                OrderProduct orderProduct = new OrderProduct();
                orderProduct.Product = product;
                orderProduct.Order = MainWindow.db.Order.Where(orderthis=> orderthis.OrderID==currentOrder.OrderID).FirstOrDefault();
                orderProduct.Count = 1;
                MainWindow.db.OrderProduct.Add(orderProduct);
                MainWindow.db.SaveChanges();
            }
            
        }

        public void UpdateOrderList()
        {
            decimal totalSum = 0;
            decimal totalDiscountSum = 0;
            foreach (var OrderProduct in MainWindow.db.Order.Where(order=> order.OrderID==currentOrder.OrderID).FirstOrDefault().OrderProduct.ToList())
            {
                totalSum += OrderProduct.Product.ProductCost * OrderProduct.Count;
                totalDiscountSum +=
                    (decimal)(OrderProduct.Product.ProductCost * OrderProduct.Product.ProductDiscountAmount / 100 * OrderProduct.Count);
            }

            MainWindow.orderWindow.TotalDiscountTextBox.Text = "Общая скидка: $" + (totalDiscountSum);
            MainWindow.orderWindow.TotalSumTextBox.Text = "Общая сумма: $" + (totalSum-totalDiscountSum);
            MainWindow.orderWindow.OrderListView.ItemsSource = MainWindow.db.Order.Where(order=> order.OrderID==currentOrder.OrderID).FirstOrDefault().OrderProduct.ToList();
        }
        private void AddToOrderClick(object sender, RoutedEventArgs e)
        {
            Product product;
            try
            {
                 product = (sender as MenuItem).DataContext as Product;
            }
            catch
            {
                 product = (sender as Button).DataContext as Product;
            }

            if (!CurrentOrderExists)
            {
                AddCurrentOrderToDatabase(); 
            }
            AddProductToOrderWithCounting(product);
            UpdateOrderList();
            MessageBox.Show("К заказу был добавлен продукт");
        }
    }
}