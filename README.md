README for Recipe Application
________________________________________
Recipe Application
This is a WPF (Windows Presentation Foundation) application for managing recipes. The application allows users to add, display, scale, reset, delete, and filter recipes.
Table of Contents
1.	Features
2.	Prerequisites
3.	Installation
4.	Building and Running the Application
5.	Usage
________________________________________
Features
•	Add New Recipe: Enter recipe details including ingredients and steps.
•	Display All Recipes: View all recipes in a list.
•	Display Specific Recipe: View detailed information of a specific recipe.
•	Scale Recipe: Adjust the ingredient quantities by a specified factor.
•	Reset Recipe: Restore a scaled recipe to its original quantities.
•	Delete Recipe: Remove a recipe from the list.
•	Filter Recipes: Filter recipes based on ingredient name, food group, or maximum calories.
Prerequisites
•	.NET 5.0 or later
•	Visual Studio 2019 or later
Installation
1.	Clone the Repository:
git clone https://github.com/yourusername/RecipeApp.git
cd RecipeApp
2.	Open the Project in Visual Studio:
•	Launch Visual Studio.
•	Open the solution file (RecipeApp.sln).
Building and Running the Application
1.	Restore NuGet Packages:
•	In Visual Studio, right-click on the solution in the Solution Explorer.
•	Select Restore NuGet Packages.
2.	Build the Solution:
•	Click on Build in the top menu.
•	Select Build Solution (or press Ctrl+Shift+B).
3.	Run the Application:
•	Click on Debug in the top menu.
•	Select Start Debugging (or press F5).
Usage
1.	Main Window:
•	On launching the application, you will see the main window with buttons for different actions (Add New Recipe, Display All Recipes, etc.).
2.	Adding a New Recipe:
•	Click the Add New Recipe button and follow the prompts to enter recipe details.
3.	Displaying Recipes:
•	Click the Display All Recipes button to view all recipes.
•	Click the Display Specific Recipe button and enter the recipe name to view specific details.
4.	Scaling and Resetting Recipes:
•	Click the Scale Recipe button and enter the recipe name and scaling factor.
•	Click the Reset Recipe button and enter the recipe name to reset it.
5.	Deleting a Recipe:
•	Click the Delete Recipe button and enter the recipe name to delete it.
6.	Filtering Recipes:
•	Select a filter type from the dropdown, enter the filter text, and click Apply Filter.

________________________________________
Notes:
Ensure you have the necessary .NET SDK installed on your machine.
The application is currently designed to run on Windows due to its WPF framework dependency.

