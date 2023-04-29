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
    /// Логика взаимодействия для ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        public static List<Product> productSortedList = MainWindow.db.Product.ToList();
        public ProductWindow()
        {
            InitializeComponent();
           // SortByDiscount.SelectedIndex = 0;
        }
        private void BackClick(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow.mainWindow.Show();
        }

        private void TextChange(object sender, TextChangedEventArgs e)
        {
            /* ProductListView.ItemsSource = MainWindow.db.Product.Where(prod => prod.ProductName.StartsWith(FindTextBox.Text)).ToList();
             if (SortComboBox.SelectedIndex == 0)
             {
                 List<Product> products = MainWindow.db.Product.Where(prod => prod.ProductName.StartsWith(FindTextBox.Text)).ToList();
                 ProductListView.ItemsSource = products.OrderByDescending(prod => prod.ProductCost);
             }
             if (SortComboBox.SelectedIndex == 1)
             {
                 List<Product> products = MainWindow.db.Product.Where(prod => prod.ProductName.StartsWith(FindTextBox.Text)).ToList();
                 ProductListView.ItemsSource = products.OrderBy(prod => prod.ProductCost);
             }*/
            AddAllFilters();
        }
        public void AddAllFilters()
        {
            //ADdPoisk
            productSortedList = productSortedList.Where(prod => prod.ProductName.StartsWith(FindTextBox.Text)).ToList();
          // MessageBox.Show("COunt: " + productSortedList.Count);
            //sort by first combobox
             if (SortComboBox.SelectedIndex == 0)
             {
                 productSortedList = productSortedList.OrderByDescending(prod => prod.ProductCost).ToList();
             }
             if (SortComboBox.SelectedIndex == 1)
             {
                 productSortedList = productSortedList.OrderBy(prod => prod.ProductCost).ToList();
             }
            //sortByDiscount
             if (SortByDiscount.SelectedIndex == 1)
             {
                 productSortedList = productSortedList.Where(prod => prod.ProductDiscountAmount>0 && prod.ProductDiscountAmount<10).ToList();
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
            productSortedList= MainWindow.db.Product.ToList();
        }
        private void ChangeSelection(object sender, SelectionChangedEventArgs e)
        {

           /* if(SortComboBox.SelectedIndex == 0)
            {
                List<Product> products = MainWindow.db.Product.Where(prod => prod.ProductName.StartsWith(FindTextBox.Text)).ToList();
                ProductListView.ItemsSource = products.OrderByDescending(prod => prod.ProductCost);
            }
            if (SortComboBox.SelectedIndex == 1)
            {
                List<Product> products = MainWindow.db.Product.Where(prod => prod.ProductName.StartsWith(FindTextBox.Text)).ToList();
                ProductListView.ItemsSource = products.OrderBy(prod => prod.ProductCost);
            }*/
           AddAllFilters();
        }

        private void ChangeSelectionOfDiscountComboBox(object sender, SelectionChangedEventArgs e)
        {
           AddAllFilters();
        }
    }
}
