using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WhatsForDinner.Controllers;
using WhatsForDinner.Models;

namespace WhatsForDinner.Views
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class CookBookView : UserControl
    {
        /// <summary>
        /// The view model that contains the entire programs state.
        /// </summary>
        private readonly ViewModel viewModel;

        /// <summary>
        /// The cookbook view is the view that allows user to enter in recipes and maintain them.
        /// </summary>
        /// <param name="viewModel"> this is the system state to be used and modified by the view.</param>
        public CookBookView(ViewModel viewModel)
        {
            InitializeComponent();

            this.viewModel = viewModel;

            // Binding listboxes to viewmodel data.
            this.RecipesListBox.ItemsSource = this.viewModel.Recipes;
            this.AllIngredientsListBox.ItemsSource = this.viewModel.AllIngredients;
            this.ApparatusListBox.ItemsSource = this.viewModel.AllTools;
        }

        /// <summary>
        /// Triggered when an item is selected in the <see cref="AllIngredientsListBox"/> , this function updates the enablement of
        /// the <see cref="AllIngredientDelete"/> button.
        /// </summary>
        /// <param name="sender">The <see cref="AllIngredientsListBox"/></param>
        /// <param name="e">Events arguments.</param>
        private void IngredientsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.AllIngredientDelete.IsEnabled = this.AllIngredientsListBox.SelectedItem is Ingredient;
        }

        /// <summary>
        /// Triggered when an item is selected in the <see cref="ApparatusListBox"/> , this function updates the enablement of
        /// the <see cref="AllToolsDelete"/> button.
        /// </summary>
        /// <param name="sender">The <see cref="ApparatusListBox"/></param>
        /// <param name="e">Events arguments.</param>
        private void ApparatusListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.AllToolsDelete.IsEnabled = this.ApparatusListBox.SelectedItem is Apparatus;
        }

        /// <summary>
        /// Triggered when an item is selected in the <see cref="RecipeToolsListBox"/> , this function updates the enablement of
        /// the <see cref="RecipeToolDelete"/> button.
        /// </summary>
        /// <param name="sender">The <see cref="RecipeToolsListBox"/></param>
        /// <param name="e">Events arguments.</param>
        private void RecipeToolsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.RecipeToolDelete.IsEnabled = this.RecipeToolsListBox.SelectedItem is Apparatus;
        }

        /// <summary>
        /// Triggered when an item is selected in the <see cref="RecipeIngredientListBox"/> , this function updates the enablement of
        /// the <see cref="RecipeIngredientDelete"/> button.
        /// </summary>
        /// <param name="sender">The <see cref="RecipeIngredientListBox"/></param>
        /// <param name="e">Events arguments.</param>
        private void RecipeIngredientListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.RecipeIngredientDelete.IsEnabled = this.RecipeIngredientListBox.SelectedItem is QuantifiedIngredient;
        }

        /// <summary>
        /// Enables and populates the recipe form when a recipe is selected in the <see cref="RecipesListBox"/>
        /// </summary>
        /// <param name="sender"><see cref="RecipesListBox"/></param>
        /// <param name="e"> Event arguments</param>
        private void RecipesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.RecipesListBox.SelectedItem is Recipe recipe)
            {
                this.RecipeNameTextBox.IsEnabled = true;
                this.DirectionsTextBox.IsEnabled = true;
                this.RecipeNameTextBox.Text = recipe.Name;
                this.DirectionsTextBox.Text = recipe.Procedure;
                this.RecipeIngredientListBox.ItemsSource = recipe.Ingredients;
                this.RecipeToolsListBox.ItemsSource = recipe.Tools;
            }
            else
            {
                this.RecipeNameTextBox.Text = string.Empty;
                this.DirectionsTextBox.Text = string.Empty;
                this.RecipeNameTextBox.IsEnabled = false;
                this.DirectionsTextBox.IsEnabled = false;
                this.RecipeIngredientListBox.ItemsSource = null;
                this.RecipeToolsListBox.ItemsSource = null;
            }
        }


        /// <summary>
        /// Shows the <see cref="NewIngredientDialog"/> when the <see cref="AddIngredientButton"/> is clicked.
        /// </summary>
        /// <param name="sender"><see cref="AddIngredientButton"/></param>
        /// <param name="e">Event arguments</param>
        private void AddIngredient_Click(object sender, RoutedEventArgs e)
        {
            NewIngredientDialog newIngredientDialog = new NewIngredientDialog(
                true,
                this.HandleDialogClosing);

            this.ShowDialog(newIngredientDialog);
           
        }

        /// <summary>
        /// Shows the <see cref="NewApparatusDialog"/> when the <see cref="AddToolButton"/> is clicked.
        /// </summary>
        /// <param name="sender"><see cref="AddToolButton"/></param>
        /// <param name="e">Event arguments</param>
        private void AddTool_Click(object sender, RoutedEventArgs e)
        {
            NewApparatusDialog newApparatusDialog = new NewApparatusDialog(
                true,
                this.HandleDialogClosing);

            this.ShowDialog(newApparatusDialog);
        }

        /// <summary>
        /// Overload to handle dialog closing when ingredient is created but a quantity is not included. 
        /// Adds <paramref name="outIngredient"/> to the <see cref="AllIngredientsListBox"/> and, when a recipe is selected
        /// then the <paramref name="outIngredient"/> is also added to the <see cref="RecipeIngredientListBox"/>.
        /// </summary>
        /// <param name="isCancelled"> TRUE if dialog is cancelled.</param>
        /// <param name="outIngredient"> Ingredient created by the closing dialog.</param>
        private void HandleDialogClosing(bool isCancelled, QuantifiedIngredient outIngredient)
        {
            if (!isCancelled)
            {
                this.viewModel.AllIngredients.Add(outIngredient);
               
                if (this.RecipesListBox.SelectedItem is Recipe recipe)
                {
                    recipe.Ingredients.Add(outIngredient);
                }
            }
            CloseDialog();
        }

        /// <summary>
        /// Handles dialog closing for an already existing ingredient that is double clicked
        /// which results in a prompt for <see cref="QuantifiedIngredient.Quantity"/> and <see cref="QuantifiedIngredient.Units"/> before adding it to the 
        /// <see cref="RecipeIngredientListBox"/>
        /// </summary>
        /// <param name="isCancelled">TRUE if dialog is cancelled.</param>
        /// <param name="outIngredient">Ingredient created by the closing dialog.</param>
        private void HandleQuantityDialogClosing(bool isCancelled, QuantifiedIngredient outIngredient)
        {
            if (!isCancelled)
            {
                if (this.RecipesListBox.SelectedItem is Recipe recipe)
                {
                    recipe.Ingredients.Add(outIngredient);
                }
            }
            CloseDialog();
        }

        /// <summary>
        /// Overload to handle dialog closing when ingredient is created but a quantity is not included. 
        /// Adds <paramref name="outTool"/> to the <see cref="ApparatusListBox"/> and, when a recipe is selected
        /// then the <paramref name="outTool"/> is also added to the <see cref="RecipeToolsListBox"/>.
        /// </summary>
        /// <param name="isCancelled"> TRUE if dialog is cancelled.</param>
        /// <param name="outTool"> Apparatus created by the closing dialog.</param>
        private void HandleDialogClosing(bool isCancelled, Apparatus outTool)
        {
            if (!isCancelled)
            {
                this.viewModel.AllTools.Add(outTool);

                if (this.RecipesListBox.SelectedItem is Recipe recipe)
                {
                    recipe.Tools.Add(outTool);
                }
            }
            CloseDialog();
        }

        /// <summary>
        /// Closes the currently open dialog in the <see cref="DialogContainer"/>.
        /// </summary>
        private void CloseDialog()
        {
            this.DialogContainer.Visibility = Visibility.Hidden;
            this.DialogContainer.Children.RemoveAt(1);
        }

        /// <summary>
        /// Shows the <paramref name="view"/> in the <see cref="DialogContainer"/>.
        /// </summary>
        /// <param name="view"> the view to be displayed.</param>
        private void ShowDialog(UserControl view)
        {
            this.DialogContainer.Children.Add(view);
            this.DialogContainer.Visibility = Visibility.Visible;
        }


        /// <summary>
        /// When the <see cref="AddRecipeButton"/> is clicked, adds a new recipe to the <see cref="RecipesListBox"/>.
        /// </summary>
        /// <param name="sender"><see cref="AddRecipeButton"/></param>
        /// <param name="e"> events arguments</param>
        private void AddRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            Recipe recipe = new Recipe("New Recipe");
            this.viewModel.Recipes.Add(recipe);
            this.RecipesListBox.SelectedItem = recipe;
        }

        /// <summary>
        /// When the <see cref="DeleteRecipeButton"/> is clicked, removes a the selected recipe from the <see cref="RecipesListBox"/>.
        /// </summary>
        /// <param name="sender"><see cref="DeleteRecipeButton"/></param>
        /// <param name="e"> events arguments</param>
        private void DeleteRecipe_Click(object sender, RoutedEventArgs e)
        {
            Recipe recipe = this.RecipesListBox.SelectedItem as Recipe;
            this.viewModel.Recipes.Remove(recipe);
        }


        /// <summary>
        /// Filters <see cref="RecipesListBox"/> when <see cref="RecipeFilterTextBox"/> text is changed. 
        /// </summary>
        /// <param name="sender"><see cref="RecipeFilterTextBox"/></param>
        /// <param name="e">event arguments</param>
        /// <remarks>The <see cref="RecipeFilterTextBox"/> expects the user to enter ingredients delimitered by commas.</remarks>
        private void RecipeFilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.viewModel != null)
            {
                string filteredIngredient = this.RecipeFilterTextBox.Text;
                if (string.IsNullOrWhiteSpace(filteredIngredient))
                {
                    RecipesListBox.ItemsSource = viewModel.Recipes;


                }
                else
                {
                    string[] ingredients = filteredIngredient.Split(',');

                    // LINQ used here for filtering.
                    ingredients = ingredients.Select(i => i.Trim()).ToArray();
                    IEnumerable<Recipe> recipes = viewModel.Recipes.Where(recipe =>
                    {
                        foreach ( Ingredient ingredient1 in recipe.Ingredients)
                        {
                            if (ingredients.Any(ingredient => string.Compare(ingredient1.Name, ingredient, true) == 0))
                            {
                                return true;
                            }
                        }

                        return false;
                    });

                    RecipesListBox.ItemsSource = recipes;
                   

                }
            }
        }

        /// <summary>
        /// Updates the <see cref="RecipesListBox"/> when the selctred recipes name is changed in the <see cref="RecipeNameTextBox"/>.
        /// </summary>
        /// <param name="sender"><see cref="RecipeNameTextBox"/></param>
        /// <param name="e">event arguments</param>
        private void RecipeNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.RecipesListBox.SelectedItem is Recipe recipe)
            {
                recipe.Name = this.RecipeNameTextBox.Text;
                RecipesListBox.Items.Refresh();
            }
        }

        /// <summary>
        /// Updates the <see cref="Recipe.Procedure"/> when the selected recipes procedure is changed in the <see cref="DirectionsTextBox"/>.
        /// </summary>
        /// <param name="sender"><see cref="DirectionsTextBox"/></param>
        /// <param name="e">event arguments</param>
        private void DirectionsTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(this.RecipesListBox.SelectedItem is Recipe recipe)
            {
                recipe.Procedure = this.DirectionsTextBox.Text;
            }
        }


        /// <summary>
        /// Displays the <see cref="NewIngredientDialog"/>, when an existing ingredient in the <see cref="AllIngredientsListBox"/> is double clicked.
        /// </summary>
        /// <param name="sender"><see cref="AllIngredientsListBox"/></param>
        /// <param name="e"> event arguments</param>
        private void AllIngredientsListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // casting the SelectedItem within the ingredients listbox to an Ingredient (from a basic object) and storing the casted 
            // value in a newly declared local Ingredient typed variable named 'ingredient'.
            Ingredient ingredient = this.AllIngredientsListBox.SelectedItem as Ingredient;
            NewIngredientDialog newIngredientDialog = new NewIngredientDialog(
               ingredient,
               this.HandleQuantityDialogClosing);

            this.ShowDialog(newIngredientDialog);
        }

        /// <summary>
        /// Adds the selected apparatus from the <see cref="ApparatusListBox"/> to the <see cref="Recipe.Tools"/> of the 
        /// selected recipe of the <see cref="RecipesListBox"/>.
        /// </summary>
        /// <param name="sender"><see cref="ApparatusListBox"/></param>
        /// <param name="e">Event Arguments</param>
        private void ApparatusListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Apparatus apparatus = this.ApparatusListBox.SelectedItem as Apparatus;
            if(this.RecipesListBox.SelectedItem is Recipe recipe)
            {
                recipe.Tools.Add(apparatus);
            }
        }

        /// <summary>
        /// Deletes the selected ingredient from the <see cref="RecipeIngredientListBox"/> from the <see cref="Recipe.Ingredients"/>
        /// of the selected recipe from the <see cref="RecipeIngredientDelete"/>.
        /// </summary>
        /// <param name="sender"><see cref="ApparatusListBox"/></param>
        /// <param name="e">Event Arguments</param>
        private void RecipeIngredientDelete_Click(object sender, RoutedEventArgs e)
        {
            Recipe recipe = this.RecipesListBox.SelectedItem as Recipe;
            QuantifiedIngredient quantifiedIngredient = this.RecipeIngredientListBox.SelectedItem as QuantifiedIngredient;
            recipe.Ingredients.Remove(quantifiedIngredient);
        }

        /// <summary>
        /// Deletes the selected ingredient in the <see cref="AllIngredientsListBox"/>.
        /// </summary>
        /// <param name="sender"><see cref="AllIngredientDelete"/></param>
        /// <param name="e">event arguments</param>
        private void AllIngredientDelete_Click(object sender, RoutedEventArgs e)
        {
            Ingredient ingredient = this.AllIngredientsListBox.SelectedItem as Ingredient;
            this.viewModel.AllIngredients.Remove(ingredient);
        }

        /// <summary>
        /// Deletes the selected Apparatus in the <see cref="RecipeToolsListBox"/> from the <see cref="RecipesListBox"/>
        /// when <see cref="RecipeToolDelete"/> is clicked.
        /// </summary>
        /// <param name="sender"><see cref="RecipeToolDelete"/></param>
        /// <param name="e">Event Arguments</param>
        private void RecipeToolDelete_Click(object sender, RoutedEventArgs e)
        {
            Recipe recipe = this.RecipesListBox.SelectedItem as Recipe;
            Apparatus apparatus = this.RecipeToolsListBox.SelectedItem as Apparatus;
            recipe.Tools.Remove(apparatus);
        }

        /// <summary>
        /// Deletes the selected Apparatus from the <see cref="ApparatusListBox"/>.
        /// </summary>
        /// <param name="sender"><see cref="AllToolsDelete"/></param>
        /// <param name="e">event arguments</param>
        private void AllToolsDelete_Click(object sender, RoutedEventArgs e)
        {
            Apparatus apparatus = this.ApparatusListBox.SelectedItem as Apparatus;
            this.viewModel.AllTools.Remove(apparatus);
        }

        
    }
}
