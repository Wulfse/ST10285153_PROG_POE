using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG6221_Part3
{
    /// <summary>
    /// Class representing an ingredient in the recipe
    /// </summary>
    ///------------------------------------------------------------------------------------------------------------///
    public class Ingredient
    {
        //getters and setters for ingredients( name, quantity, unit of measurement, calories and foodgroup)
        public string Name { get; set; }
        public double Quantity { get; set; }
        public string Unit { get; set; }
        public double Calories { get; set; }
        public string FoodGroup { get; set; }

        //--------------------------------------------------------------------------------------------------------//

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="quantity"></param>
        /// <param name="unit"></param>
        ///------------------------------------------------------------------------------------------------------///
        public Ingredient(string name, double quantity, string unit, double calories, string foodGroup)
        {
            Name = name;
            Quantity = quantity;
            Unit = unit;
            Calories = calories;
            FoodGroup = foodGroup;
        }
    }
}

//---------------------------------------------------------End of Class-------------------------------------------------------//