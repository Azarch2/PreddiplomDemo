using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Office.Interop.Word;
using Application = Microsoft.Office.Interop.Word.Application;
using Window = System.Windows.Window;

namespace ProductDEmo
{
    public partial class OrderWindow : Window
    {
        public static decimal TotalSumOfOrder = 0;
        public static decimal TotalSumOfOrderWithDiscounts = 0;
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

        public string GetProductsInOrderString(string str)
        {
            decimal totalSum = 0;
            decimal totalDiscountSum = 0;
            foreach (var orderProduct in MainWindow.db.Order.Where(order=> order.OrderID==UserProductWindow.currentOrder.OrderID).FirstOrDefault().OrderProduct)
            {
                totalSum = 0;
                totalDiscountSum = 0;
                totalSum += orderProduct.Product.ProductCost * orderProduct.Count;
                totalDiscountSum +=
                    (decimal)(orderProduct.Product.ProductCost * orderProduct.Product.ProductDiscountAmount / 100 * orderProduct.Count);
                str += "Артикул: " + orderProduct.Product.ProductArticleNumber + "\n";
                str += "Наименование: " + orderProduct.Product.ProductName + "\n";
                str += "Единица измерения: " + orderProduct.Product.UnitType.UnitTypeName + "\n";
                str += "Цена: $" + (orderProduct.Product.ProductCost * orderProduct.Count) + "\n";
                str += "Поставщик: " + orderProduct.Product.ProductSupplier.ProductSupplierName + "\n";
                str += "Категория: " + orderProduct.Product.ProductCategory.ProductCategoryName + "\n";
                str += "Скидка: " + orderProduct.Product.ProductDiscountAmount + "\n";
                str += "Описание: " + orderProduct.Product.ProductDescription + "\n";
                str += "\n";
            }

            TotalSumOfOrder = totalSum;
            TotalSumOfOrderWithDiscounts = totalSum - totalDiscountSum;

            return str;
        }
        public void FinalOrder()
        {
            Random rnd = new Random();
            UserProductWindow.currentOrder.OrderGetCode = rnd.Next(100, 1000);
            UserProductWindow.currentOrder.OrderCreateDate = DateTime.Now;
            UserProductWindow.currentOrder.OrderDeliveryDate = DateTime.Now + TimeSpan.FromHours(72);
            
        }
        private void GetOrder(object sender, RoutedEventArgs e)
        {
            string str = "";
            FinalOrder();
            Application app = new Application();
            Document doc = app.Documents.Add();
            doc.Content.Text = "ФИО: " + MainWindow.CurrentUser.UserSurname + " " + MainWindow.CurrentUser.UserName + " " + MainWindow
                .CurrentUser.UserPatronymic;
            doc.Content.Text += "Дата заказа: " + UserProductWindow.currentOrder.OrderCreateDate+ "\n" +
                               "Номер заказа: " + UserProductWindow.currentOrder.OrderID + "\n" +
                               "Состав заказа: " + GetProductsInOrderString(str) + "\n" +
                               "Сумма заказа: " + TotalSumOfOrder + "\n" +
                               "Сумма скидки: " + TotalSumOfOrderWithDiscounts + "\n" +
                               "Пункт выдачи: " + UserProductWindow.currentOrder.PickupPoint.Address + "\n" +
                               "Код получения: " + UserProductWindow.currentOrder.OrderGetCode + "\n";
            doc.SaveAs2(@"C:\Users\Azarch\Documents\check.pdf", WdSaveFormat.wdFormatPDF);
            Process.Start(@"C:\Users\Azarch\Documents\check.pdf");
            doc.Close();
            app.Quit();
        }
    }
}