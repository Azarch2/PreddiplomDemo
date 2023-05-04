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
            PickupPointComboBox.ItemsSource = MainWindow.db.PickupPoint.ToList();
        }
        /// <summary>
        /// Функция закрытия окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Close(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
        /// <summary>
        /// Функция удаления товара из корзины пользователя
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Minus(object sender, RoutedEventArgs e)
        {
            OrderProduct orderProductToRemove = (sender as Button).DataContext as OrderProduct;
            List<OrderProduct> orderProductCollection = MainWindow.db.Order
                .Where(order => order.OrderID == UserProductWindow.currentOrder.OrderID).FirstOrDefault().OrderProduct
                .ToList();
                if (orderProductToRemove.Count - 1 == 0)
                {
                    MainWindow.db.OrderProduct.Remove(orderProductToRemove);
                    MainWindow.db.SaveChanges();
                }
                else
                {
                    orderProductToRemove.Count -= 1;
                    MainWindow.db.SaveChanges();
                }
                MainWindow.userProductWindow.UpdateOrderList();
            MessageBox.Show("Продукт был удалён");
 
        }
        /// <summary>
        /// Метод получения продуктов заказа как строки
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string GetProductsInOrderString(string str)
        {
            decimal totalSum = 0;
            decimal totalDiscountSum = 0;
            foreach (var orderProduct in MainWindow.db.Order.Where(order=> order.OrderID==UserProductWindow.currentOrder.OrderID).FirstOrDefault().OrderProduct)
            {
                totalSum += orderProduct.Product.ProductCost * orderProduct.Count;
                totalDiscountSum +=
                    (decimal)(orderProduct.Product.ProductCost * orderProduct.Product.ProductDiscountAmount / 100 * orderProduct.Count);
                str += "Артикул: " + orderProduct.Product.ProductArticleNumber + "\n";
                str += "Наименование: " + orderProduct.Product.ProductName + "\n";
                str += "Единица измерения: " + orderProduct.Product.UnitType.UnitTypeName + "\n";
                str += "Цена: $" + (orderProduct.Product.ProductCost * orderProduct.Count) + "\n";
                str += "Поставщик: " + orderProduct.Product.ProductSupplier.ProductSupplierName + "\n";
                str += "Категория: " + orderProduct.Product.ProductCategory.ProductCategoryName + "\n";
                str += "Скидка: " + orderProduct.Product.ProductDiscountAmount + "%\n";
                str += "Описание: " + orderProduct.Product.ProductDescription + "\n";
                str += "\n";
            }

            TotalSumOfOrder = totalSum;
            TotalSumOfOrderWithDiscounts = totalDiscountSum;

            return str;
        }
        /// <summary>
        /// Метод назначения итоговох свойств заказа
        /// </summary>
        public void FinalOrder()
        {
            Random rnd = new Random();
            UserProductWindow.currentOrder.OrderGetCode = rnd.Next(100, 1000);
            UserProductWindow.currentOrder.OrderCreateDate = DateTime.Now;
            UserProductWindow.currentOrder.OrderDeliveryDate = DateTime.Now + TimeSpan.FromHours(72);
            UserProductWindow.currentOrder.PickupPoint = PickupPointComboBox.SelectedItem as PickupPoint;

        }
        /// <summary>
        /// Метод финального оформления заказа с формированием pdf документа
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetOrder(object sender, RoutedEventArgs e)
        {
            if (PickupPointComboBox.SelectedItem == null)
            {
                MessageBox.Show("Вы не выбрали пункт назначения!");
                return;
            }
            string str = "";
            FinalOrder();
            Application app = new Application();
            Document doc = app.Documents.Add();
            doc.Content.Text += "Дата заказа: " + UserProductWindow.currentOrder.OrderCreateDate+ "\n" +
                               "Номер заказа: " + UserProductWindow.currentOrder.OrderID + "\n" +
                               "Состав заказа: " + "\n" + GetProductsInOrderString(str) + "\n" +
                               "Сумма заказа: " + TotalSumOfOrder + "\n" +
                               "Сумма скидки: " + TotalSumOfOrderWithDiscounts + "\n" +
                               "Пункт выдачи: " + UserProductWindow.currentOrder.PickupPoint.Address + "\n" +
                               "Код получения: " + UserProductWindow.currentOrder.OrderGetCode + "\n";
            doc.SaveAs2(@"C:\Users\zzz\Documents\check.pdf", WdSaveFormat.wdFormatPDF);
            Process.Start(@"C:\Users\zzz\Documents\check.pdf");
            doc.Close();
            app.Quit();
        }
    }
}