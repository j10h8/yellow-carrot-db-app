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

        public RecipeWindow(string signedInUserName)
        {
            _signedInUserName = signedInUserName;

            InitializeComponent();

            UpdateUi();
        }

        private void UpdateUi()
        {
            lvRecipies.Items.Clear();

            using (AppDbContext appContext = new())
            {
                UnitOfWork uow = new(appContext);

                foreach (Recipe recipe in uow.RecipeRepository.GetRecipies().OrderBy(n => n.Name).ToList())
                {
                    ListViewItem item = new();
                    item.Content = recipe.Name;
                    item.Tag = recipe;
                    lvRecipies.Items.Add(item);
                }

                uow.SaveChanges();
            }
        }

        private void btnSignOut_Click(object sender, RoutedEventArgs e)
        {
            SignInWindow signInWindow = new();
            signInWindow.Show();

            this.Close();
        }

        private void btnDetailsEnabled_Click(object sender, RoutedEventArgs e)
        {
            ListViewItem recipeItem = (ListViewItem)lvRecipies.SelectedItem;
            Recipe recipe = (Recipe)recipeItem.Tag;

            DetailsWindow detailsWindow = new(_signedInUserName, recipe.Name);
            detailsWindow.Show();

            this.Close();
        }

        private void btnAddRecipie_Click(object sender, RoutedEventArgs e)
        {
            AddRecipeWindow addRecipeWindow = new(_signedInUserName);
            addRecipeWindow.Show();

            this.Close();
        }

        private void btnDeleteEnabled_Click(object sender, RoutedEventArgs e)
        {
            ListViewItem item = (ListViewItem)lvRecipies.SelectedItem;
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

        private void lvRecipies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvRecipies.SelectedItems.Count > 0)
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
    }
}
