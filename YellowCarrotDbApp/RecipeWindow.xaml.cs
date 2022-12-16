using System.Linq;
using System.Windows;
using System.Windows.Controls;
using YellowCarrotDbApp.Data;
using YellowCarrotDbApp.Models;
using YellowCarrotDbApp.Services;

namespace YellowCarrotDbApp
{
    /// <summary>
    /// Interaction logic for RecipeWindow.xaml
    /// </summary>
    public partial class RecipeWindow : Window
    {
        private string _signedInUserName;

        // Class constructor. Calls InitializeComponent and UpdateUi methods. Takes string argument and sets field variable.
        public RecipeWindow(string signedInUserName)
        {
            _signedInUserName = signedInUserName;

            InitializeComponent();

            UpdateUi();
        }

        // Updates RecipeWindow UI 
        private void UpdateUi()
        {
            lvRecipes.Items.Clear();

            using (AppDbContext appContext = new())
            {
                UnitOfWork uow = new(appContext);

                // GetRecipes gets and returns a list of all Recipes in YellowCarrotDb
                foreach (Recipe recipe in uow.RecipeRepository.GetRecipes().OrderBy(n => n.Name).ToList())
                {
                    ListViewItem item = new();
                    item.Content = recipe.Name;
                    item.Tag = recipe;
                    lvRecipes.Items.Add(item);
                }

                // SaveChanges saves all changes made to YellowCarrotDb
                uow.SaveChanges();
            }
        }

        // Creates and opens a RecipeWindow and closes this window 
        private void btnAddRecipie_Click(object sender, RoutedEventArgs e)
        {
            AddRecipeWindow addRecipeWindow = new(_signedInUserName);
            addRecipeWindow.Show();

            this.Close();
        }

        // Creates and opens a ConfirmDeleteWindow if the signed in users username = the recipe users username. Closes this window. 
        private void btnDeleteEnabled_Click(object sender, RoutedEventArgs e)
        {
            ListViewItem item = (ListViewItem)lvRecipes.SelectedItem;
            Recipe recipe = (Recipe)item.Tag;

            if (recipe.User.Username == _signedInUserName)
            {
                ConfirmDeleteWindow confirmDeleteWindow = new(_signedInUserName, recipe.Name);
                confirmDeleteWindow.Show();

                this.Close();
            }
            else
            {
                MessageBox.Show("Deleting another users recipe is not allowed!", "Error!");
            }
        }

        // Creates and opens a DetailsWindow and closes this window 
        private void btnDetailsEnabled_Click(object sender, RoutedEventArgs e)
        {
            ListViewItem recipeItem = (ListViewItem)lvRecipes.SelectedItem;
            Recipe recipe = (Recipe)recipeItem.Tag;

            DetailsWindow detailsWindow = new(_signedInUserName, recipe.Name);
            detailsWindow.Show();

            this.Close();
        }

        // Updates button visibility depending on listview selection 
        private void lvRecipes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvRecipes.SelectedItems.Count > 0)
            {
                btnDetailsDisabled.Visibility = Visibility.Hidden;
                btnDetailsEnabled.Visibility = Visibility.Visible;
                btnDeleteDisabled.Visibility = Visibility.Hidden;
                btnDeleteEnabled.Visibility = Visibility.Visible;
            }
            else
            {
                btnDetailsDisabled.Visibility = Visibility.Visible;
                btnDetailsEnabled.Visibility = Visibility.Hidden;
                btnDeleteDisabled.Visibility = Visibility.Visible;
                btnDeleteEnabled.Visibility = Visibility.Hidden;
            }
        }

        // Creates and opens a SignInWindow and closes this window 
        private void btnSignOut_Click(object sender, RoutedEventArgs e)
        {
            SignInWindow signInWindow = new();
            signInWindow.Show();

            this.Close();
        }
    }
}
