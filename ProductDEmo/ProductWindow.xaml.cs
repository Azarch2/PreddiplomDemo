﻿using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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
        /// Функция редактирования текста поисковой строки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextChange(object sender, TextChangedEventArgs e)
        {
            AddAllFilters();
        }
        /// <summary>
        /// Функция добавления всех фильтров
        /// </summary>
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
        /// <summary>
        /// Функция реагирования на событие редактирования товара
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeSelection(object sender, SelectionChangedEventArgs e)
        {
            AddAllFilters();
        }
        /// <summary>
        /// Событие изменения выбора сортировки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeSelectionOfDiscountComboBox(object sender, SelectionChangedEventArgs e)
        {
            AddAllFilters();
        }
        /// <summary>
        /// Функция применения цвета для элемента если скидка больше определённого значения
        /// </summary>
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
 
    }
}
