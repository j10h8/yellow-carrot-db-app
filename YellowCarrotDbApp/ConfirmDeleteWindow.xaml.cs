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

            tblQuestion.Text = $"Are you sure you want to delete the selected recipe ({recipeName})?";
        }

        private void btnConfirmDelete_Click(object sender, RoutedEventArgs e)
        {   // TODO: Implement cascade delete behaviour for Tag (since it's many to many relation with Recipe)
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
