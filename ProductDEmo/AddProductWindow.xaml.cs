using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

namespace ProductDEmo
{
    public partial class AddProductWindow : Window
    {
        public AddProductWindow()
        {
            InitializeComponent();
        }

        private void AddProductClick(object sender, RoutedEventArgs e)
        {
            if (CategoryComboBox.SelectedItem == null || ManufacturerComboBox.SelectedItem == null ||
                PriceTextBox.Text == ""
                || DescriptionTextBox.Text == "" || NameTextBox.Text == "" || SupplierComboBox.SelectedItem == null ||
                QuantityTextBox.Text == "" || ArticleTextBox.Text == "" || UnitTypeComboBox.SelectedItem == null || MaxDiscountTextBox.Text=="" ||
                DiscountTextBox.Text=="" || ChoosedPhotoImage.Source == null)
            {
                MessageBox.Show("Вы заполнили не все поля");
                return;
            }
            if (int.Parse(MaxDiscountTextBox.Text) < int.Parse(DiscountTextBox.Text))
            {
                MessageBox.Show("Размер скидки не должен превышать максимальный размер скидки");
                return;
            }
            
            Product product = new Product();
            product.ProductCategory = CategoryComboBox.SelectedItem as ProductCategory;
            product.ProductManufacturer = ManufacturerComboBox.SelectedItem as ProductManufacturer;
            product.ProductCost = decimal.Parse(PriceTextBox.Text);
            product.ProductDescription = DescriptionTextBox.Text;
            product.ProductName = NameTextBox.Text;
            product.ProductSupplier = SupplierComboBox.SelectedItem as ProductSupplier;
            product.ProductQuantityInStock = int.Parse(QuantityTextBox.Text);
            product.ProductArticleNumber = ArticleTextBox.Text;
            product.UnitType = UnitTypeComboBox.SelectedItem as UnitType;
            product.ProductMaxDiscountAmount = byte.Parse(MaxDiscountTextBox.Text);
            product.ProductDiscountAmount = byte.Parse(DiscountTextBox.Text);
            product.ProductPhoto = ChoosedPhotoImage.Source.ToString();
            product.FullPath = ChoosedPhotoImage.Source.ToString();
            MainWindow.db.Product.Add(product);
            MainWindow.db.SaveChanges();
            MainWindow.adminWindow.AdminProductGrid.ItemsSource = MainWindow.db.Product.ToList();
            this.Hide();
            MessageBox.Show("Вы успешно добавили новый продукт");
        }

        private void ChoosePhotoClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.ShowDialog();
            ChoosedPhotoImage.Source = new BitmapImage(new Uri(dialog.FileName));
        }

        private void Close(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}