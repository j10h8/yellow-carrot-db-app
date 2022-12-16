using System.Windows;
using YellowCarrotDbApp.Data;
using YellowCarrotDbApp.Services;

namespace YellowCarrotDbApp
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        // Class constructor. Calls InitializeComponent method.
        public RegisterWindow()
        {
            InitializeComponent();
        }

        // Saves user in YellowCarrotDb and YellowCarrotUserDb if data has been provided correctly and provided username is not already registered 
        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            if (txtUsername.Text.Trim().Length < 1 || txtUsername.Text.Trim().Length < 2 || pbPassword.Password.Length < 1 || pbPassword.Password.Length < 8 || pbPassword.Password.Contains(' ') || pbConfirmPassword.Password.Contains(' ') || pbPassword.Password != pbConfirmPassword.Password)
            {
                MessageBox.Show("Please choose a password with at least 8 characters that does not contain spaces, and a user name with at least 2 characters, and make sure that 'Password' and 'Confirm password' matches!", "Error!");
            }
            else
            {
                using (UserDbContext userContext = new())
                {
                    // RegisterNewUser creates and saves a User in YellowCarrotUserDb if provided username does not exist in YellowCarrotUserDb
                    if (new UserRepository(userContext).RegisterNewUser(txtUsername.Text.Trim(), pbPassword.Password))
                    {
                        // SaveChanges saves all changes made to YellowCarrotUserDb
                        userContext.SaveChanges();

                        using (AppDbContext appContext = new())
                        {
                            UnitOfWork uow = new(appContext);

                            // AddUser creates and saves a AppUser to YellowCarrotDb
                            uow.AppUserRepository.AddUser(txtUsername.Text);

                            // SaveChanges saves all changes made to YellowCarrotDb
                            uow.SaveChanges();
                        }

                        MessageBox.Show($"{txtUsername.Text} has been registered!", "Success!");

                        SignInWindow signInWindow = new();
                        signInWindow.Show();

                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show($"User name ({txtUsername.Text}) is not available. Please choose another one.", "Error!");
                    }
                }
            }
        }

        // Creates and opens a SignInWindow and closes this window 
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            SignInWindow signInWindow = new();
            signInWindow.Show();

            this.Close();
        }
    }
}
