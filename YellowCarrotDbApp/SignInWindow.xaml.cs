using System.Windows;
using YellowCarrotDbApp.Data;
using YellowCarrotDbApp.Services;

namespace YellowCarrotDbApp
{
    /// <summary>
    /// Interaction logic for SignInWindow.xaml
    /// </summary>
    public partial class SignInWindow : Window
    {
        public SignInWindow()
        {
            InitializeComponent();
        }

        private void btnCloseApp_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            if (txtUserName.Text.Trim().Length > 0 && pbPassword.Password.Trim().Length > 0)
            {
                using (UserDbContext userContext = new())
                {
                    UserRepository userRepository = new(userContext);

                    if (userRepository.IsRegistered(txtUserName.Text.Trim(), pbPassword.Password.Trim()))
                    {
                        RecipeWindow recipeWindow = new(txtUserName.Text.Trim());
                        recipeWindow.Show();

                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Sign in credentials don't match or don't exist. Please try again!", "Error!");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please provide user name and password to sign in!", "Error!");
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new();
            registerWindow.Show();

            this.Close();
        }
    }
}
