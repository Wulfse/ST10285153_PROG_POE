using PROG6221_Part3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG6221_Part3
{
    /// <summary>
    /// Class representing a recipe
    /// </summary>
    ///-----------------------------------------------------------------------------------------------------------///
    public class Recipe
    {
        public string Name { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<Step> Steps { get; set; }
        private List<Ingredient> OriginalQuantities { get; set; }
        public event Action<string>? CaloriesExceeded;

        //---------------------------------------------------------------------------------------------------------------//

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="ingredients"></param>
        /// <param name="steps"></param>
        ///-------------------------------------------------------------------------------------------------------------///
        public Recipe(string name, List<Ingredient> ingredients, List<Step> steps)
        {
            Name = name;
            Ingredients = ingredients;
            Steps = steps;
            OriginalQuantities = ingredients.Select(i => new Ingredient(i.Name, i.Quantity, i.Unit, i.Calories, i.FoodGroup)).ToList();
        }

        //----------------------------------------------------------------------------------------------------------------//

        /// <summary>
        /// Method to display the recipe( recipe name, ingreidents and steps)
        /// </summary>
        /// -------------------------------------------------------------------------------------------------------------///
        public void DisplayRecipe()
        {
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine($"Recipe: {Name}\n");
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("Ingredients:");
            foreach (var ingredient in Ingredients)
            {
                Console.WriteLine($"{ingredient.Quantity} {ingredient.Unit} of {ingredient.Name} ({ingredient.Calories} calories, {ingredient.FoodGroup})");
            }
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("\nSteps:");
            for (int i = 0; i < Steps.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Steps[i].Description}");
            }
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine($"Total Calories: {GetTotalCalories()}");
            Console.WriteLine("-------------------------------------------------");
        }
        //----------------------------------------------------------------------------------------------------------------//

        /// <summary>
        /// Method to calculate total calories and notify user if its over 300 
        /// </summary>
        /// -------------------------------------------------------------------------------------------------------------///
        public double GetTotalCalories()
        {
            double totalCalories = Ingredients.Sum(i => i.Calories);
            if (totalCalories > 300)
            {
                CaloriesExceeded?.Invoke($"The total calories for the recipe '{Name}' exceed 300 calories!");
            }
            return totalCalories;
        }


        //-----------------------------------------------------------------------------------------------------------------//

        /// <summary>
        ///  Method to scale the recipe by a factor
        /// </summary>
        /// <param name="factor"></param>
        /// ---------------------------------------------------------------------------------------------------------------//
        public void ScaleRecipe(double factor)
        {
            foreach (var ingredient in Ingredients)
            {
                ingredient.Quantity *= factor;
                ingredient.Calories *= factor;
            }
        }
        //-------------------------------------------------------------------------------------------------------------------//

        /// <summary>
        /// Method to reset ingredient quantities to original values
        /// </summary>
        /// ---------------------------------------------------------------------------------------------------------------///
        public void ResetQuantities()
        {
            for (int i = 0; i < Ingredients.Count; i++)
            {
                Ingredients[i].Quantity = OriginalQuantities[i].Quantity;
                Ingredients[i].Calories = OriginalQuantities[i].Calories;
            }
        }
    }
}
//---------------------------------------------------------End of Class------------------------------------------------------//
