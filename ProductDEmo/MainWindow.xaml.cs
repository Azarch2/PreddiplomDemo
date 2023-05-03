using System;
using System.Linq;
using System.Windows;

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
            PathInitialize();
        }
        public void GenerateCaptcha()
        {
            Random rnd = new Random();
            CaptchaNumbers = rnd.Next(10000, 99999);
        }
        public void PathInitialize()
        {
            string vivod = "";
            foreach (var Product in db.Product)
            {
                if (Product.ProductPhoto != null)
                {
                    Product.FullPath = "Resources/" + Product.ProductPhoto;
                }

                vivod += Product.FullPath + "\n";
            }
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
            productWindow.ProductListView.ItemsSource = db.Product.ToList();
            mainWindow.Hide();
            productWindow.Show();
            productWindow.InfoTextBox.Text = "Количество: " + MainWindow.db.Product.ToList().Count + "/" + MainWindow.db.Product.ToList().Count;
            productWindow.ChangeColorIfDiscountLargerThan15();
        }
    }
}
