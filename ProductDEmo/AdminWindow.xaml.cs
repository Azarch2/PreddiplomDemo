using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Функция возврата на предыдущее окно
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackClick(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow.mainWindow.Show();
        }
        /// <summary>
        /// Функция вывода окна редактирования продукта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeProduct(object sender, SelectionChangedEventArgs e)
        {
            ChangeProductWindow.currentProduct = AdminProductGrid.SelectedItem as Product;
            if (ChangeProductWindow.currentProduct == null)
            {
                return;
            }
            Product choosed = AdminProductGrid.SelectedItem as Product;
            MainWindow.changeProductWindow.ArticleTextBox.Text = choosed.ProductArticleNumber;
            MainWindow.changeProductWindow.NameTextBox.Text = choosed.ProductName;
            MainWindow.changeProductWindow.DescriptionTextBox.Text = choosed.ProductDescription;
            MainWindow.changeProductWindow.PriceTextBox.Text = choosed.ProductCost.ToString();
            MainWindow.changeProductWindow.MaxDiscountTextBox.Text = choosed.ProductMaxDiscountAmount.ToString();
            MainWindow.changeProductWindow.DiscountTextBox.Text = choosed.ProductDiscountAmount.ToString();
            MainWindow.changeProductWindow.QuantityTextBox.Text = choosed.ProductQuantityInStock.ToString();
            MainWindow.changeProductWindow.DescriptionTextBox.Text = choosed.ProductDescription;
            MessageBox.Show("Choosed: " + choosed.FullPath);
            try
            {
                MainWindow.changeProductWindow.ChoosedPhotoImage.Source = new BitmapImage(new Uri("C:\\PreddiplomDemo\\ProductDEmo\\" + choosed.FullPath));
            }
            catch
            {
                choosed.ProductPhoto = "C:\\PreddiplomDemo\\ProductDEmo\\Resources\\picture.png";
                MainWindow.changeProductWindow.ChoosedPhotoImage.Source = new BitmapImage(new Uri(choosed.ProductPhoto));
            }
            MainWindow.changeProductWindow.CategoryComboBox.ItemsSource = MainWindow.db.ProductCategory.ToList();
            MainWindow.changeProductWindow.ManufacturerComboBox.ItemsSource = MainWindow.db.ProductManufacturer.ToList();
            MainWindow.changeProductWindow.SupplierComboBox.ItemsSource = MainWindow.db.ProductSupplier.ToList();
            MainWindow.changeProductWindow.UnitTypeComboBox.ItemsSource = MainWindow.db.UnitType.ToList();
            MainWindow.changeProductWindow.CategoryComboBox.SelectedItem = choosed.ProductCategory;
            MainWindow.changeProductWindow.ManufacturerComboBox.SelectedItem = choosed.ProductManufacturer;
            MainWindow.changeProductWindow.SupplierComboBox.SelectedItem = choosed.ProductSupplier;
            MainWindow.changeProductWindow.UnitTypeComboBox.SelectedItem = choosed.UnitType;
            MainWindow.changeProductWindow.Show();
        }
        /// <summary>
        /// Функция показа окна добавления товара
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddClick(object sender, RoutedEventArgs e)
        {
            MainWindow.addProductWindow.CategoryComboBox.ItemsSource = MainWindow.db.ProductCategory.ToList();
            MainWindow.addProductWindow.ManufacturerComboBox.ItemsSource = MainWindow.db.ProductManufacturer.ToList();
            MainWindow.addProductWindow.SupplierComboBox.ItemsSource = MainWindow.db.ProductSupplier.ToList();
            MainWindow.addProductWindow.UnitTypeComboBox.ItemsSource = MainWindow.db.UnitType.ToList();
            MainWindow.addProductWindow.Show();
        }
        
    }
}
