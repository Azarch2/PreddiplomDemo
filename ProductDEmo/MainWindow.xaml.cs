using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProductDEmo
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static DataBaseEntities db = new DataBaseEntities();
        public static MainWindow mainWindow;
        public static ProductWindow productWindow = new ProductWindow();
        public static CaptchaWindow captchaWindow = new CaptchaWindow();
        public static int NumbersOfIncorrectAuthorizations = 0;
        public static int CaptchaNumbers = 00000;
        public MainWindow()
        {
            InitializeComponent();
            mainWindow = this;
            PathInitializa();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        public void GenerateCaptcha()
        {
            Random rnd = new Random();
            CaptchaNumbers = rnd.Next(10000, 99999);
        }
        public void PathInitializa()
        {
            string vivod = "";
            foreach(var Product in db.Product)
            {
                if (Product.ProductPhoto != null)
                {
                    Product.FullPath = "Resources/" + Product.ProductPhoto;
                }

                vivod += Product.FullPath + "\n";
            }
          //  MessageBox.Show(vivod);
        }

        private void ExitClick(object sender, RoutedEventArgs e)
        {
           Application.Current.Shutdown();
        }

        private void AuthorizationClick(object sender, RoutedEventArgs e)
        {
            if (NumbersOfIncorrectAuthorizations < 5)
            {
                try
                {
                    User user = db.User.ToList().Where(us => us.UserLogin == LoginTextBox.Text && us.UserPassword == PasswordTextBox.Text).FirstOrDefault();
                    if (user != null)
                    {
                        MessageBox.Show(user.UserLogin);
                        MessageBox.Show("Вы успешно авторизовались");
                        MessageBox.Show("Ваша роль: " + user.Role.RoleName);
                    }
                    else
                    {
                        MessageBox.Show("Неверные данные для входа");
                        NumbersOfIncorrectAuthorizations++;

                        TextBoxClear();
                        if (NumbersOfIncorrectAuthorizations >= 5)
                        {
                            captchaWindow.Show();
                            GenerateCaptcha();
                            captchaWindow.CaptchaTextBlock.Text = CaptchaNumbers.ToString();
                            MessageBox.Show("Вы потратили на авторизацию слишком много попыток");
                        }
                    }
                    // MessageBox.Show("Успешная авторизация");
                }
                catch
                {
                    MessageBox.Show("Возникла непредвиденная ошибка");
                }
            }
            else
            {
                captchaWindow.Show();
                GenerateCaptcha();
                captchaWindow.CaptchaTextBlock.Text = CaptchaNumbers.ToString();
                MessageBox.Show("Сначала пройдите капчу!");
            }
            }

            public void TextBoxClear()
        {
            PasswordTextBox.Text = "";
            LoginTextBox.Text = "";
        }

        private void ProductsClick(object sender, RoutedEventArgs e)
        {
            mainWindow.Hide();
            productWindow.Show();
            productWindow.ProductListView.ItemsSource = db.Product.ToList();
        }
    }
}
