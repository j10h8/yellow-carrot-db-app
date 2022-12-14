using System.Windows;

namespace YellowCarrotDbApp
{
    /// <summary>
    /// Interaction logic for AddRecipeWindow.xaml
    /// </summary>
    public partial class AddRecipeWindow : Window
    {
        private string _signedInUserName;

        public AddRecipeWindow(string signedInUserName)
        {
            _signedInUserName = signedInUserName;

            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            RecipeWindow recipeWindow = new(_signedInUserName);
            recipeWindow.Show();

            this.Close();
        }

        private void lvIngredients_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (lvIngredients.SelectedItems.Count > 0)
            {
                btnDeleteIngredientDisabled.Visibility = Visibility.Hidden;
                btnDeleteIngredientEnabled.Visibility = Visibility.Visible;
            }
            else
            {
                btnDeleteIngredientDisabled.Visibility = Visibility.Visible;
                btnDeleteIngredientEnabled.Visibility = Visibility.Hidden;
            }
        }

        private void lvTags_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (lvTags.SelectedItems.Count > 0)
            {
                btnDeleteTagDisabled.Visibility = Visibility.Hidden;
                btnDeleteTagEnabled.Visibility = Visibility.Visible;
            }
            else
            {
                btnDeleteTagDisabled.Visibility = Visibility.Visible;
                btnDeleteTagEnabled.Visibility = Visibility.Hidden;
            }
        }

        // Recipe name must be unique 
    }
}
