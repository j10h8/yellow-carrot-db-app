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
        private string _signedInUserName;

        public SignInWindow()
        {
            InitializeComponent();
        }

        private void btnCloseApp_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            if (txtUserName.Text.Trim().Length > 0 && pbPassword.Password.Trim().Length > 0)
            {
                using (UserDbContext context = new())
                {
                    UserRepository userRepository = new(context);

                    if (userRepository.IsRegistered(txtUserName.Text.Trim(), pbPassword.Password.Trim()))
                    {
                        _signedInUserName = txtUserName.Text.Trim();

                        RecipeWindow recipeWindow = new(_signedInUserName);
                        recipeWindow.Show();

                        Close();
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

        }
    }
}
