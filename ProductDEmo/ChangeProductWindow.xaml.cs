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

        private void ChoosePhotoClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.ShowDialog();
            ChoosedPhotoImage.Source = new BitmapImage(new Uri(dialog.FileName));
        }

        private void ChangeProductClick(object sender, RoutedEventArgs e)
        {
            currentProduct.ProductManufacturer = ManufacturerComboBox.SelectedItem as ProductManufacturer;
            currentProduct.ProductName = NameTextBox.Text;
            currentProduct.ProductSupplier = SupplierComboBox.SelectedItem as ProductSupplier;
            currentProduct.ProductCategory = CategoryComboBox.SelectedItem as ProductCategory;
            currentProduct.ProductCost = decimal.Parse(PriceTextBox.Text);
            currentProduct.ProductDescription= DescriptionTextBox.Text;
            currentProduct.ProductMaxDiscountAmount = byte.Parse( MaxDiscountTextBox.Text);
            currentProduct.ProductDiscountAmount = byte.Parse(DiscountTextBox.Text);
            currentProduct.ProductQuantityInStock = int.Parse(QuantityTextBox.Text);
            currentProduct.ProductPhoto = ChoosedPhotoImage.Source.ToString();
            currentProduct.FullPath = ChoosedPhotoImage.Source.ToString();
            MainWindow.db.SaveChanges();
            MessageBox.Show("Вы успешно отредактировали товар!");
            this.Hide();
            MainWindow.adminWindow.AdminProductGrid.ItemsSource = MainWindow.db.Product.ToList();
        }

        private void Close(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

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
