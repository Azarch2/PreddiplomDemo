using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProductDEmo
{
    /// <summary>
    /// Логика взаимодействия для ChangeProductWindow.xaml
    /// </summary>
    public partial class ChangeProductWindow : Window
    {
        public static Product currentProduct;
        public ChangeProductWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Функция выбора фотографии
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChoosePhotoClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                ChoosedPhotoImage.Source = new BitmapImage(new Uri(dialog.FileName));
            }
        }
        /// <summary>
        /// Функция редактирования продукта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeProductClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CategoryComboBox.SelectedItem == null || ManufacturerComboBox.SelectedItem == null ||
                    PriceTextBox.Text == ""
                    || DescriptionTextBox.Text == "" || NameTextBox.Text == "" || SupplierComboBox.SelectedItem == null ||
                    QuantityTextBox.Text == "" || ArticleTextBox.Text == "" || UnitTypeComboBox.SelectedItem == null || MaxDiscountTextBox.Text == "" ||
                    DiscountTextBox.Text == "" || ChoosedPhotoImage.Source == null)
                {
                    MessageBox.Show("Вы заполнили не все поля");
                    return;
                }
                if (int.Parse(MaxDiscountTextBox.Text) < int.Parse(DiscountTextBox.Text))
                {
                    MessageBox.Show("Размер скидки не должен превышать максимальный размер скидки");
                    return;
                }
                if (double.Parse(PriceTextBox.Text) < 0)
                {
                    MessageBox.Show("Стоимость не должна быть отрицательной");
                    return;
                }
                if (int.Parse(QuantityTextBox.Text) < 0)
                {
                    MessageBox.Show("Количество не должно быть отрицательным");
                    return;
                }
                currentProduct.ProductManufacturer = ManufacturerComboBox.SelectedItem as ProductManufacturer;
                currentProduct.ProductName = NameTextBox.Text;
                currentProduct.ProductSupplier = SupplierComboBox.SelectedItem as ProductSupplier;
                currentProduct.ProductCategory = CategoryComboBox.SelectedItem as ProductCategory;
                currentProduct.ProductCost = decimal.Parse(PriceTextBox.Text);
                currentProduct.ProductDescription = DescriptionTextBox.Text;
                currentProduct.ProductMaxDiscountAmount = byte.Parse(MaxDiscountTextBox.Text);
                currentProduct.ProductDiscountAmount = byte.Parse(DiscountTextBox.Text);
                currentProduct.ProductQuantityInStock = int.Parse(QuantityTextBox.Text);
                if (ChoosedPhotoImage.Source != null)
                {
                    currentProduct.ProductPhoto = ChoosedPhotoImage.Source.ToString();
                    currentProduct.FullPath = ChoosedPhotoImage.Source.ToString();
                }

                MainWindow.db.SaveChanges();
                MessageBox.Show("Вы успешно отредактировали товар!");
                this.Hide();
                MainWindow.adminWindow.AdminProductGrid.ItemsSource = MainWindow.db.Product.ToList();
            }
            catch
            {
                MessageBox.Show("Вы неверно заполнили поля");
            }
        }
        /// <summary>
        /// Функция обработки события закрытий окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Close(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
        /// <summary>
        /// Функция удаления продукта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteProductClick(object sender, RoutedEventArgs e)
        {
            MainWindow.db.Product.Remove(currentProduct);
            MainWindow.db.SaveChanges();
            MessageBox.Show("Вы успешно удалили продукт");
            this.Hide();
            MainWindow.adminWindow.AdminProductGrid.ItemsSource = MainWindow.db.Product.ToList();
        }
    }
}
