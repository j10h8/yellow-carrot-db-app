using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using YellowCarrotDbApp.Data;
using YellowCarrotDbApp.Models;
using YellowCarrotDbApp.Services;

namespace YellowCarrotDbApp
{
    /// <summary>
    /// Interaction logic for DetailsWindow.xaml
    /// </summary>
    public partial class DetailsWindow : Window
    {
        private string _signedInUserName;
        private string _oldRecipeName;
        private Recipe _recipe = new();
        private List<Ingredient> _ingredients = new();
        private List<Tag> _tags = new();

        // Class constructor. Calls InitializeComponent, SetRecipe, and UpdateListViews methods. Takes two string arguments and sets field variables.
        public DetailsWindow(string signedInUserName, string oldRecipeName)
        {
            _signedInUserName = signedInUserName;
            _oldRecipeName = oldRecipeName;

            InitializeComponent();

            // SetRecipe populates recipe name textbox and sets field variables related to the recipe 
            SetRecipe();

            UpdateListViews();
        }

        // Populates recipe name textbox and sets field variables related to the recipe 
        private void SetRecipe()
        {
            using (AppDbContext appContext = new())
            {
                UnitOfWork uow = new(appContext);

                // GetRecipe gets and returns a recipe from YellowCarrotDb 
                _recipe = (Recipe)uow.RecipeRepository.GetRecipe(_oldRecipeName);
                _ingredients = _recipe.Ingredients;
                _tags = _recipe.Tags;
                txtRecipeName.Text = _recipe.Name;

                // SaveChanges saves all changes made to YellowCarrotDb
                uow.SaveChanges();
            }
        }

        // Updates listviews 
        private void UpdateListViews()
        {
            lvIngredients.Items.Clear();
            lvTags.Items.Clear();

            foreach (Ingredient ingredient in _ingredients.OrderBy(i => i.Name).ToList())
            {
                ListViewItem ingredientItem = new();
                ingredientItem.Content = $"{ingredient.Name} ({ingredient.Quantity})";
                ingredientItem.Tag = ingredient;
                lvIngredients.Items.Add(ingredientItem);
            }

            foreach (Tag tag in _tags.OrderBy(t => t.Description).ToList())
            {
                ListViewItem tagItem = new();
                tagItem.Content = tag.Description;
                tagItem.Tag = tag;
                lvTags.Items.Add(tagItem);
            }
        }

        // Updates UI and UI elements availability if signed in username = the recipes user username 
        private void btnUnlock_Click(object sender, RoutedEventArgs e)
        {
            if (_signedInUserName == _recipe.User.Username)
            {
                txtRecipeName.IsReadOnly = false;
                txtIngredient.IsReadOnly = false;
                txtQuantity.IsReadOnly = false;
                txtTag.IsReadOnly = false;
                lvIngredients.UnselectAll();
                lvTags.UnselectAll();

                btnUnlock.Visibility = Visibility.Hidden;
                btnSaveRecipe.Visibility = Visibility.Visible;

                MessageBox.Show("Recipe has been unlocked!", "Success!");
            }
            else
            {
                MessageBox.Show("Unlocking other users recipes is not allowed!", "Error!");
            }
        }

        // Creates and adds Ingredient to ingredients list 
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

        // Deletes Ingredient from ingredients list 
        private void btnDeleteIngredientEnabled_Click(object sender, RoutedEventArgs e)
        {
            ListViewItem ingredientItem = (ListViewItem)lvIngredients.SelectedItem;
            Ingredient ingredient = (Ingredient)ingredientItem.Tag;
            _ingredients.Remove(ingredient);

            UpdateListViews();
        }

        // Creates and adds Tag to tags list 
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

        // Deletes Tag from tags list 
        private void btnDeleteTagEnabled_Click(object sender, RoutedEventArgs e)
        {
            ListViewItem tagItem = (ListViewItem)lvTags.SelectedItem;
            Tag tag = (Tag)tagItem.Tag;
            _tags.Remove(tag);

            UpdateListViews();
        }

        // Updates recipe if data has been provided correctly and if recipe name is available 
        private void btnSaveRecipe_Click(object sender, RoutedEventArgs e)
        {
            if (txtRecipeName.Text.Trim().Length > 0 && _ingredients.Count > 1 && _tags.Count > 0)
            {
                using (AppDbContext appContext = new())
                {
                    UnitOfWork uow = new(appContext);

                    // IsUsed returns true if recipe name exists in YellowCarrotDb
                    if (_recipe.Name != txtRecipeName.Text.Trim() && uow.RecipeRepository.IsUsed(txtRecipeName.Text.Trim()))
                    {
                        MessageBox.Show("Recipe name is already used. Please choose another one!", "Error!");
                    }
                    else
                    {
                        List<Tag> newTagList = new();

                        foreach (Tag tag in _tags)
                        {
                            Tag newTag = new()
                            {
                                Description = tag.Description
                            };

                            newTagList.Add(newTag);
                        }

                        // DeleteTags deletes recipes tags in YellowCarrotDb
                        uow.RecipeRepository.DeleteTags(_recipe.Name);

                        // UpdateRecipe updates recipe in YellowCarrotDb
                        uow.RecipeRepository.UpdateRecipe(_oldRecipeName, txtRecipeName.Text.Trim(), _signedInUserName, _ingredients, newTagList);

                        // SaveChanges saves all changes made to YellowCarrotDb
                        uow.SaveChanges();

                        txtIngredient.Clear();
                        txtQuantity.Clear();
                        txtTag.Clear();
                        _oldRecipeName = txtRecipeName.Text.Trim();
                        txtRecipeName.IsReadOnly = true;
                        txtIngredient.IsReadOnly = true;
                        txtQuantity.IsReadOnly = true;
                        txtTag.IsReadOnly = true;
                        lvIngredients.UnselectAll();
                        lvTags.UnselectAll();

                        // SetRecipe populates recipe name textbox and sets field variables related to the recipe 
                        SetRecipe();

                        UpdateListViews();

                        btnUnlock.Visibility = Visibility.Visible;
                        btnSaveRecipe.Visibility = Visibility.Hidden;

                        MessageBox.Show("Recipe has been saved!", "Success!");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please specify recipe name and add at least two ingredients and one tag!", "Error!");
            }
        }

        // Changes button visibility depending on text input 
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

        // Changes button visibility depending on text input 
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

        // Changes button visibility depending on listview selection 
        private void lvIngredients_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (lvIngredients.SelectedItems.Count > 0 && txtRecipeName.IsReadOnly == false)
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

        // Changes button visibility depending on listview selection 
        private void lvTags_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (lvTags.SelectedItems.Count > 0 && txtRecipeName.IsReadOnly == false)
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

        // Creates and opens a RecipeWindow and closes this window 
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            RecipeWindow recipeWindow = new(_signedInUserName);
            recipeWindow.Show();

            this.Close();
        }
    }
}
