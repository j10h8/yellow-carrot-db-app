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
        // Class constructor. Calls InitializeComponent method.
        public SignInWindow()
        {
            InitializeComponent();
        }

        // Checks sign in credentials and shows error message if wrong. Opens a RecipeWindow if right. 
        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            if (txtUserName.Text.Trim().Length > 0 && pbPassword.Password.Trim().Length > 0)
            {
                using (UserDbContext userContext = new())
                {
                    UserRepository userRepository = new(userContext);

                    // IsRegistered checks if provided username and password exist in YellowCarrotUserDb 
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

        // Creates and opens a Registerwindow and closes this window 
        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new();
            registerWindow.Show();

            this.Close();
        }

        // Closes SignInWindow/YellowCarrotDbApp 
        private void btnCloseApp_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
