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

        public ConfirmDeleteWindow(string signedInUserName, string recipeName)
        {
            _signedInUserName = signedInUserName;
            _recipeName = recipeName;

            InitializeComponent();
        }

        private void btnConfirmDelete_Click(object sender, RoutedEventArgs e)
        {
            using (AppDbContext appContext = new())
            {
                UnitOfWork uow = new(appContext);
                uow.RecipeRepository.DeleteRecipe(_recipeName);
                uow.SaveChanges();
            }

            RecipeWindow recipeWindow = new(_signedInUserName);
            recipeWindow.Show();

            this.Close();
        }

        private void btnCancelDelete_Click(object sender, RoutedEventArgs e)
        {
            RecipeWindow recipeWindow = new(_signedInUserName);
            recipeWindow.Show();

            this.Close();
        }
    }
}
