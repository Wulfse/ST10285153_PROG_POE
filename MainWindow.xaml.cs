using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace PROG6221_Part3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Recipe> recipes = new List<Recipe>();

        public MainWindow()
        {
            InitializeComponent();
        }
        //------------------------------------------------------------------------------------------------------------------//

        /// <summary>
        ///  Method for when the add recipe button is clicked
        /// </summary>
        /// <returns></returns>
        /// --------------------------------------------------------------------------------------------------------------///
        private void AddRecipe_Click(object sender, RoutedEventArgs e)
        {
            // Clear the ListBox
                RecipeListBox.ItemsSource = null;
                RecipeListBox.Items.Clear();

            ClearRecipeDetails();

            AddRecipe();
        }
        //------------------------------------------------------------------------------------------------------------------//

        /// <summary>
        ///  Method for when the display all recipes button is clicked
        /// </summary>
        /// <returns></returns>
        /// --------------------------------------------------------------------------------------------------------------///
        private void DisplayAllRecipes_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Clear the ListBox
                RecipeListBox.ItemsSource = null;
                RecipeListBox.Items.Clear();

                ClearRecipeDetails();

                if (recipes.Count == 0)
                {
                    RecipeListBox.Items.Add("No recipes available.");
                    return;
                }

                // Display all recipes
                RecipeListBox.ItemsSource = recipes.OrderBy(r => r.Name).Select(r => r.Name).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //------------------------------------------------------------------------------------------------------------------//

        /// <summary>
        ///  Method for when the display a specific recipe button is clicked
        /// </summary>
        /// <returns></returns>
        /// --------------------------------------------------------------------------------------------------------------///
        private void DisplaySpecificRecipe_Click(object sender, RoutedEventArgs e)
        {
            // Clear the ListBox
            RecipeListBox.ItemsSource = null;
            RecipeListBox.Items.Clear();

            ClearRecipeDetails();

            if (recipes.Count == 0)
            {
                RecipeDetailsTextBlock.Text = "No recipes available.";
                return;
            }

            string recipeName = Microsoft.VisualBasic.Interaction.InputBox("Enter the recipe name:", "Display Specific Recipe");
            var recipe = recipes.FirstOrDefault(r => r.Name.Equals(recipeName, StringComparison.OrdinalIgnoreCase));
            if (recipe == null)
            {
                RecipeDetailsTextBlock.Text = "Recipe not found.";
                return;
            }

            RecipeDetailsTextBlock.Text = GetRecipeDetails(recipe);
        }
        //------------------------------------------------------------------------------------------------------------------//

        /// <summary>
        ///  Method for when the scale a recipe button is clicked
        /// </summary>
        /// <returns></returns>
        /// --------------------------------------------------------------------------------------------------------------///
        private void ScaleRecipe_Click(object sender, RoutedEventArgs e)
        {
            // Clear the ListBox
            RecipeListBox.ItemsSource = null;
            RecipeListBox.Items.Clear();

            ClearRecipeDetails();

            if (recipes.Count == 0)
            {
                RecipeDetailsTextBlock.Text = "No recipes available.";
                return;
            }

            string recipeName = Microsoft.VisualBasic.Interaction.InputBox("Enter the recipe name to scale:", "Scale Recipe");
            var recipe = recipes.FirstOrDefault(r => r.Name.Equals(recipeName, StringComparison.OrdinalIgnoreCase));
            if (recipe == null)
            {
                RecipeDetailsTextBlock.Text = "Recipe not found.";
                return;
            }

            string factorInput = Microsoft.VisualBasic.Interaction.InputBox("Enter the scaling factor (e.g., 0.5, 2):", "Scale Recipe");
            if (!double.TryParse(factorInput, out double factor) || factor <= 0)
            {
                RecipeDetailsTextBlock.Text = "Invalid input! Please enter a valid numeric value for the scaling factor.";
                return;
            }

            recipe.ScaleRecipe(factor);
            RecipeDetailsTextBlock.Text = "Recipe scaled successfully!";
            RecipeDetailsTextBlock.Text += "\n" + GetRecipeDetails(recipe);
        }
        //------------------------------------------------------------------------------------------------------------------//

        /// <summary>
        ///  Method for when the reset a recipe button is clicked
        /// </summary>
        /// <returns></returns>
        /// --------------------------------------------------------------------------------------------------------------///
        private void ResetRecipe_Click(object sender, RoutedEventArgs e)
        {
            // Clear the ListBox
            RecipeListBox.ItemsSource = null;
            RecipeListBox.Items.Clear();

            ClearRecipeDetails();

            if (recipes.Count == 0)
            {
                RecipeDetailsTextBlock.Text = "No recipes available.";
                return;
            }

            string recipeName = Microsoft.VisualBasic.Interaction.InputBox("Enter the recipe name to reset:", "Reset Recipe");
            var recipe = recipes.FirstOrDefault(r => r.Name.Equals(recipeName, StringComparison.OrdinalIgnoreCase));
            if (recipe == null)
            {
                RecipeDetailsTextBlock.Text = "Recipe not found.";
                return;
            }

            recipe.ResetQuantities();
            RecipeDetailsTextBlock.Text = "Recipe quantities reset successfully!";
            RecipeDetailsTextBlock.Text += "\n" + GetRecipeDetails(recipe);
        }
        //------------------------------------------------------------------------------------------------------------------//

        /// <summary>
        ///  Method for when the delete a recipe button is clicked
        /// </summary>
        /// <returns></returns>
        /// --------------------------------------------------------------------------------------------------------------///
        private void DeleteRecipe_Click(object sender, RoutedEventArgs e)
        {
            // Clear the ListBox
            RecipeListBox.ItemsSource = null;
            RecipeListBox.Items.Clear();

            ClearRecipeDetails();

            if (recipes.Count == 0)
            {
                RecipeDetailsTextBlock.Text = "No recipes available.";
                return;
            }

            string recipeName = Microsoft.VisualBasic.Interaction.InputBox("Enter the recipe name to delete:", "Delete Recipe");
            var recipe = recipes.FirstOrDefault(r => r.Name.Equals(recipeName, StringComparison.OrdinalIgnoreCase));
            if (recipe == null)
            {
                RecipeDetailsTextBlock.Text = "Recipe not found.";
                return;
            }

            recipes.Remove(recipe);
            RecipeDetailsTextBlock.Text = "Recipe deleted successfully!";
        }
        //------------------------------------------------------------------------------------------------------------------//

        /// <summary>
        ///  Method for when the filtering of recipes is done
        /// </summary>
        /// <returns></returns>
        /// --------------------------------------------------------------------------------------------------------------///
        private void FilterRecipes_Click(object sender, RoutedEventArgs e)
        {
            ClearRecipeDetails();

            try
            {
                string filterText = FilterTextBox.Text;

                // Check if an item is selected
                if (FilterComboBox.SelectedItem is ComboBoxItem selectedItem)
                {
                    // Safely get the string representation of Content
                    string? filterType = selectedItem?.Content?.ToString();

                    if (!string.IsNullOrEmpty(filterType))
                    {
                        var filteredRecipes = new List<string>();

                        switch (filterType)
                        {
                            case "Ingredient Name":
                                filteredRecipes = recipes.Where(r => r.Ingredients.Any(i => i.Name.Contains(filterText))).Select(r => r.Name).ToList();
                                break;
                            case "Food Group":
                                filteredRecipes = recipes.Where(r => r.Ingredients.Any(i => i.FoodGroup.Contains(filterText))).Select(r => r.Name).ToList();
                                break;
                            case "Maximum Calories":
                                if (double.TryParse(filterText, out double maxCalories))
                                {
                                    filteredRecipes = recipes.Where(r => r.GetTotalCalories() <= maxCalories).Select(r => r.Name).ToList();
                                }
                                break;
                            default:
                                MessageBox.Show("Invalid filter type selected.");
                                return;
                        }

                        // Set ItemsSource to the filtered recipes
                        RecipeListBox.ItemsSource = filteredRecipes;
                    }
                    else
                    {
                        // Handle case where Content is null or empty
                        MessageBox.Show("Selected item content is null or empty.");
                    }
                }
                else
                {
                    // Handle case where no item is selected
                    MessageBox.Show("No item selected in the ComboBox.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //------------------------------------------------------------------------------------------------------------------//

        /// <summary>
        ///  Method for when the exit button is clicked
        /// </summary>
        /// <returns></returns>
        /// --------------------------------------------------------------------------------------------------------------///
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        //------------------------------------------------------------------------------------------------------------------//

        /// <summary>
        ///  actual Method used and called in the addrecipe_click
        /// </summary>
        /// <returns></returns>
        /// --------------------------------------------------------------------------------------------------------------///
        private void AddRecipe()
        {
            Console.WriteLine("\nCreating a new recipe...");

            string recipeName = Microsoft.VisualBasic.Interaction.InputBox("Enter recipe name:", "Add Recipe");

            int numIngredients;
            do
            {
                string input = Microsoft.VisualBasic.Interaction.InputBox("Enter the number of ingredients:", "Add Recipe");
                if (!int.TryParse(input, out numIngredients) || numIngredients <= 0)
                {
                    MessageBox.Show("Invalid input! Please enter a valid positive integer for the number of ingredients.");
                }
            } while (numIngredients <= 0);

            List<Ingredient> ingredients = new List<Ingredient>();

            for (int i = 0; i < numIngredients; i++)
            {
                string name = Microsoft.VisualBasic.Interaction.InputBox($"Enter name for ingredient {i + 1}:", "Add Recipe");

                double quantity;
                do
                {
                    string input = Microsoft.VisualBasic.Interaction.InputBox($"Enter quantity for ingredient {i + 1}:", "Add Recipe");
                    if (!double.TryParse(input, out quantity) || quantity <= 0)
                    {
                        MessageBox.Show("Invalid input! Please enter a valid numeric value for quantity.");
                    }
                } while (quantity <= 0);

                string unit = Microsoft.VisualBasic.Interaction.InputBox($"Enter unit for ingredient {i + 1}:", "Add Recipe");

                double calories;
                do
                {
                    string input = Microsoft.VisualBasic.Interaction.InputBox($"Enter calories for ingredient {i + 1}:", "Add Recipe");
                    if (!double.TryParse(input, out calories) || calories < 0)
                    {
                        MessageBox.Show("Invalid input! Please enter a valid numeric value for calories.");
                    }
                } while (calories < 0);

                string foodGroup = Microsoft.VisualBasic.Interaction.InputBox($"Enter food group for ingredient {i + 1}:", "Add Recipe");

                ingredients.Add(new Ingredient(name, quantity, unit, calories, foodGroup));
            }

            int numSteps;
            do
            {
                string input = Microsoft.VisualBasic.Interaction.InputBox("Enter the number of steps:", "Add Recipe");
                if (!int.TryParse(input, out numSteps) || numSteps <= 0)
                {
                    MessageBox.Show("Invalid input! Please enter a valid positive integer for the number of steps.");
                }
            } while (numSteps <= 0);

            List<Step> steps = new List<Step>();

            for (int i = 0; i < numSteps; i++)
            {
                string description = Microsoft.VisualBasic.Interaction.InputBox($"Enter description for step {i + 1}:", "Add Recipe");
                steps.Add(new Step(description));
            }

            Recipe recipe = new Recipe(recipeName, ingredients, steps);
            recipe.CaloriesExceeded += NotifyCaloriesExceeded;
            recipes.Add(recipe);

            MessageBox.Show("New recipe created successfully!");
            RecipeDetailsTextBlock.Text = GetRecipeDetails(recipe);
        }
        //------------------------------------------------------------------------------------------------------------------//

        /// <summary>
        ///  Method for notifying user when a recipes calorie count exceeds 300
        /// </summary>
        /// <returns></returns>
        /// --------------------------------------------------------------------------------------------------------------///

        private void NotifyCaloriesExceeded(string message)
        {
            MessageBox.Show(message, "Calories Exceeded", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        //------------------------------------------------------------------------------------------------------------------//

        /// <summary>
        ///  Method used to recieve and display details of a recipe
        /// </summary>
        /// <returns></returns>
        /// --------------------------------------------------------------------------------------------------------------///
        private string GetRecipeDetails(Recipe recipe)
        {
            StringBuilder details = new StringBuilder();
            details.AppendLine("-------------------------------------------------");
            details.AppendLine($"Recipe: {recipe.Name}\n");
            details.AppendLine("-------------------------------------------------");
            details.AppendLine("Ingredients:");
            foreach (var ingredient in recipe.Ingredients)
            {
                details.AppendLine($"{ingredient.Quantity} {ingredient.Unit} of {ingredient.Name} ({ingredient.Calories} calories, {ingredient.FoodGroup})");
            }
            details.AppendLine("-------------------------------------------------");
            details.AppendLine("\nSteps:");
            for (int i = 0; i < recipe.Steps.Count; i++)
            {
                details.AppendLine($"{i + 1}. {recipe.Steps[i].Description}");
            }
            details.AppendLine("-------------------------------------------------");
            details.AppendLine($"Total Calories: {recipe.GetTotalCalories()}");
            details.AppendLine("-------------------------------------------------");
            return details.ToString();
        }
        //------------------------------------------------------------------------------------------------------------------//

        /// <summary>
        ///  Method called for in other methods to clear the recipe details between each user input
        /// </summary>
        /// <returns></returns>
        /// --------------------------------------------------------------------------------------------------------------///
        private void ClearRecipeDetails()
        {
            RecipeDetailsTextBlock.Text = "";
        }

    }
}
//---------------------------------------------------------End of Class-------------------------------------------------------//

