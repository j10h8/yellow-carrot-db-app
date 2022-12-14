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

        private void btnAddIngredient_Click(object sender, RoutedEventArgs e)
        {
            if (txtIngredient.Text.Trim().Length > 0 && txtQuantity.Text.Trim().Length > 0)
            {
                Ingredient ingredient = new()
                {
                    Name = txtIngredient.Text.Trim(),
                    Quantity = txtQuantity.Text.Trim()
                };

                _ingredients.Add(ingredient);

                txtIngredient.Clear();
                txtQuantity.Clear();

                UpdateUi();
            }
            else
            {
                MessageBox.Show("Please specify ingredient name and quantity to add ingredient to recipe!", "Error!");
            }
        }

        private void UpdateUi()
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

        private void btnTag_Click(object sender, RoutedEventArgs e)
        {
            if (txtTag.Text.Trim().Length > 0)
            {
                Tag tag = new()
                {
                    Description = txtTag.Text.Trim()
                };

                _tags.Add(tag);

                txtTag.Clear();

                UpdateUi();
            }
            else
            {
                MessageBox.Show("Please specify tag description to add tag to recipe!", "Error!");
            }
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
            // Add delete ingredient logic 
        }

        private void btnDeleteTagEnabled_Click(object sender, RoutedEventArgs e)
        {
            // Add delete Tag logic 
        }
    }
}
