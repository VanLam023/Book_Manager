using BookManager_BLL.Services;
using BookManager_DAL.Entities;
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

namespace BookManagerGui
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text;
            string password = PasswordBox.Password;
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                System.Windows.MessageBox.Show("please enter both email and password.", "input error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            UserAccountService service = new();
            UserAccount? acc = service.CheckLogin(email, password);
            if (acc != null)
            {
                if (acc.Role == 3)
                {
                    MessageBox.Show("You have no permission to access this function!", "Access denied", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                else
                {
                    MainWindow bookmanagerwindow = new MainWindow();
                    bookmanagerwindow.currentAccount = acc;
                    bookmanagerwindow.Show();
                    this.Hide(); // ẩn chính mình là cửa sổ login 
                }

            }else
            {
                System.Windows.MessageBox.Show("login failed. check the email and password again", "wrong credentials", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            var answer = MessageBox.Show("Do you really want to Quit?", "Quit?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (answer == MessageBoxResult.Yes)
                Application.Current.Shutdown();
        }
    }
}
