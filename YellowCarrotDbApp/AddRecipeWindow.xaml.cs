using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using YellowCarrotDbApp.Data;
using YellowCarrotDbApp.Models;
using YellowCarrotDbApp.Services;

namespace YellowCarrotDbApp
{
    /// <summary>
    /// Interaction logic for AddRecipeWindow.xaml
    /// </summary>
    public partial class AddRecipeWindow : Window
    {
        private string _signedInUserName;
        private List<Ingredient> _ingredients = new();
        private List<Tag> _tags = new();

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

        private void btnAddIngredientEnabled_Click(object sender, RoutedEventArgs e)
        {
            Ingredient ingredient = new()
            {
                Name = txtIngredient.Text.Trim(),
                Quantity = txtQuantity.Text.Trim()
            };

            _ingredients.Add(ingredient);

            txtIngredient.Clear();
            txtQuantity.Clear();

            UpdateListViews();
        }

        private void UpdateListViews()
        {
            lvIngredients.Items.Clear();
            lvTags.Items.Clear();

            foreach (Ingredient ingredient in _ingredients)
            {
                ListViewItem ingredientItem = new();
                ingredientItem.Content = $"{ingredient.Quantity} {ingredient.Name}";
                ingredientItem.Tag = ingredient;
                lvIngredients.Items.Add(ingredientItem);
            }

            foreach (Tag tag in _tags)
            {
                ListViewItem tagItem = new();
                tagItem.Content = tag.Description;
                tagItem.Tag = tag;
                lvTags.Items.Add(tagItem);
            }
        }

        private void btnAddTagEnabled_Click(object sender, RoutedEventArgs e)
        {
            Tag tag = new()
            {
                Description = txtTag.Text.Trim()
            };

            _tags.Add(tag);

            txtTag.Clear();

            UpdateListViews();
        }

        private void btnAddRecipe_Click(object sender, RoutedEventArgs e)
        {
            if (txtRecipeName.Text.Trim().Length > 0 && _ingredients.Count > 1 && _tags.Count > 0)
            {
                using (AppDbContext appContext = new())
                {
                    UnitOfWork uow = new(appContext);

                    if (uow.RecipeRepository.IsUsed(txtRecipeName.Text.Trim()))
                    {
                        MessageBox.Show("Recipe name is already used. Please choose another one!", "Error!");
                    }
                    else
                    {
                        uow.RecipeRepository.AddRecipe(txtRecipeName.Text.Trim(), _signedInUserName, _ingredients, _tags);

                        uow.SaveChanges();

                        txtRecipeName.Clear();
                        txtIngredient.Clear();
                        txtQuantity.Clear();
                        txtTag.Clear();
                        _ingredients.Clear();
                        _tags.Clear();

                        UpdateListViews();

                        MessageBox.Show("Recipe has been added!", "Success!");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please specify recipe name, and add at least two ingredients and one tag to the recipe!", "Error!");
            }
        }

        private void btnDeleteIngredientEnabled_Click(object sender, RoutedEventArgs e)
        {
            ListViewItem ingredientItem = (ListViewItem)lvIngredients.SelectedItem;
            Ingredient ingredient = (Ingredient)ingredientItem.Tag;
            _ingredients.Remove(ingredient);

            UpdateListViews();
        }

        private void btnDeleteTagEnabled_Click(object sender, RoutedEventArgs e)
        {
            ListViewItem tagItem = (ListViewItem)lvTags.SelectedItem;
            Tag tag = (Tag)tagItem.Tag;
            _tags.Remove(tag);

            UpdateListViews();
        }

        private void txtIngredient_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtIngredient.Text.Trim().Length > 0 && txtQuantity.Text.Trim().Length > 0)
            {
                btnAddIngredientDisabled.Visibility = Visibility.Hidden;
                btnAddIngredientEnabled.Visibility = Visibility.Visible;
            }
            else
            {
                btnAddIngredientDisabled.Visibility = Visibility.Visible;
                btnAddIngredientEnabled.Visibility = Visibility.Hidden;
            }
        }

        private void txtTag_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtTag.Text.Trim().Length > 0)
            {
                btnAddTagDisabled.Visibility = Visibility.Hidden;
                btnAddTagEnabled.Visibility = Visibility.Visible;
            }
            else
            {
                btnAddTagDisabled.Visibility = Visibility.Visible;
                btnAddTagEnabled.Visibility = Visibility.Hidden;
            }
        }
    }
}
