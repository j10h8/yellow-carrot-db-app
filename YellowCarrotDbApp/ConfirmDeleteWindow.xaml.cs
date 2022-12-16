using System.Windows;
using YellowCarrotDbApp.Data;
using YellowCarrotDbApp.Services;

namespace YellowCarrotDbApp
{
    /// <summary>
    /// Interaction logic for ConfirmDeleteWindow.xaml
    /// </summary>
    public partial class ConfirmDeleteWindow : Window
    {
        private string _signedInUserName;
        private string _recipeName;

        // Class constructor. Calls InitializeComponent method. Takes two string arguments and sets field variables.
        public ConfirmDeleteWindow(string signedInUserName, string recipeName)
        {
            _signedInUserName = signedInUserName;
            _recipeName = recipeName;

            InitializeComponent();

            tblQuestion.Text = $"Are you sure you want to delete the selected recipe ({recipeName})?";
        }

        // Deletes recipe from YellowCarrotDb 
        private void btnConfirmDelete_Click(object sender, RoutedEventArgs e)
        {
            using (AppDbContext appContext = new())
            {
                UnitOfWork uow = new(appContext);

                // DeleteRecipe deletes recipe from YellowCarrotDb 
                uow.RecipeRepository.DeleteRecipe(_recipeName);

                // SaveChanges saves all changes made to YellowCarrotDb
                uow.SaveChanges();
            }

            RecipeWindow recipeWindow = new(_signedInUserName);
            recipeWindow.Show();

            this.Close();
        }

        // Creates and opens a RecipeWindow and closes this window 
        private void btnCancelDelete_Click(object sender, RoutedEventArgs e)
        {
            RecipeWindow recipeWindow = new(_signedInUserName);
            recipeWindow.Show();

            this.Close();
        }
    }
}
