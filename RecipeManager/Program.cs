using System;
using System.Collections.Generic;
using System.IO;

namespace RecipeManager
{
    public class RecipeManager
    {
        //this is a list(collections)
        public List<Recipe> 
        Recipes { get; set; }

        public RecipeManager()
        {
            Recipes = new List<Recipe>();
        }

        //adding the recipe
        public void AddRecipe(Recipe recipe)
        {
            Recipes.Add(recipe);
        }

        //deletes the recipe
        public void RemoveRecipe(int index)
        {
            Recipes.RemoveAt(index);
        }

        //this will display the recipe
        public void DisplayRecipes()
        {
            foreach (Recipe recipe in Recipes)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n{0}) {1}", Recipes.IndexOf(recipe) + 1, recipe.Name);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nIngredients: {0}", recipe.Ingredients);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\nInstructions: {0}", recipe.Instructions);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("\nMeal Type: {0}", recipe.MealType);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\nCategory: {0}", recipe.Category + "\n");
            }
        }

        //serialization
        public void SaveRecipes()
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open("recipes.dat", FileMode.Create)))
            {
                foreach (Recipe recipe in Recipes)
                {
                    writer.Write(recipe.Name);
                    writer.Write(recipe.Ingredients);
                    writer.Write(recipe.Instructions);
                    writer.Write(recipe.MealType);
                    writer.Write(recipe.Category);
                }
            }
        }

        //deserialization
        public void LoadRecipes()
        {
            using (BinaryReader reader = new BinaryReader(File.Open("recipes.dat", FileMode.Open)))
            {
                while (reader.PeekChar() > -1)
                {
                    string name = reader.ReadString();
                    string ingredients = reader.ReadString();
                    string instructions = reader.ReadString();
                    string mealtype = reader.ReadString();
                    string category = reader.ReadString();
                    Recipe recipe = new Recipe(name, ingredients, instructions, mealtype, category);
                    Recipes.Add(recipe);
                }
            }
        }

        //edit the recipes
        public void EditRecipe()
        {
            
            DisplayRecipes();
            Console.ResetColor();
            Console.Write("Please enter the number of the recipe you want to edit: ");
            //this is robustness
            try
            {
                int index = int.Parse(Console.ReadLine()) - 1;
                Recipe recipe = Recipes[index];
                Console.Write("\nPlease enter a new name for the recipe: ");
                recipe.Name = Console.ReadLine();
                Console.Write("\nPlease enter new ingredients for the recipe: ");
                recipe.Ingredients = Console.ReadLine();
                Console.Write("\nPlease enter new instructions for the recipe: ");
                recipe.Instructions = Console.ReadLine();
                Console.Write("\nPlease enter the meal type for the recipe: ");
                recipe.MealType = Console.ReadLine();
                Console.Write("\nPlease enter the category of the meal: ");
                recipe.Category = Console.ReadLine();
            }
            catch(Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("An error occurred: {0}", ex.Message);
            }
        }

        //menu
        public void MainMenu()
        {
            Console.ResetColor();
            Console.WriteLine("Welcome to the recipe manager!");
            //main menu
            bool quit = true;
            while (quit)
            {                     
                Console.ResetColor();
                Console.WriteLine("/--------------------------------------------------/\nPlease enter the command to the following options:\n/--------------------------------------------------/");
                Console.WriteLine("Display recipes");
                Console.WriteLine("Add new recipe");
                Console.WriteLine("Edit Recipe");
                Console.WriteLine("Delete recipe");
                Console.WriteLine("Save recipes");
                Console.WriteLine("Load recipes");
                Console.WriteLine("Help ( Enter: '/help')");
                Console.WriteLine("Quit");

                string selection = Console.ReadLine();
                
                Console.Clear();// Clear the main menu after the user has made a selection(but this seems to not work)

                    switch (selection)
                    {
                        case "/show":
                            DisplayRecipes();
                            break;
                        case "/add":
                            Console.Write("Please enter a name for the recipe: ");
                            string name = Console.ReadLine();
                            Console.Write("\nPlease enter the ingredients for the recipe: ");
                            string ingredients = Console.ReadLine();
                            Console.Write("\nPlease enter the instructions for the recipe: ");
                            string instructions = Console.ReadLine();
                            Console.Write("\nPlease enter the meal type for the recipe: ");
                            string mealtype = Console.ReadLine();
                            Console.Write("\nPlease enter the category of the meal: ");
                            string category = Console.ReadLine();
                            Recipe recipe = new Recipe(name, ingredients, instructions, mealtype, category);
                            AddRecipe(recipe);
                            break;
                        case "/edit":
                            EditRecipe();
                            break;
                        case "/delete":
                            DisplayRecipes();
                            Console.ResetColor();
                            Console.Write("Please enter the number of the recipe to delete: ");
                            int index = int.Parse(Console.ReadLine());
                            RemoveRecipe(index - 1);
                            break;
                        case "/save":
                            SaveRecipes();
                            break;
                        case "/load":
                            LoadRecipes();
                            break;
                        case "/help":
                            Console.WriteLine("/------------------------------/\nThese are the following Commands\n/------------------------------/\n");
                            Console.WriteLine("/show:   {Displays recipes} \n/add:    {Add Recipes} \n/edit:   {Edit Recipes} \n/delete: {Delete Recipes} \n/save:   {Save Recipes} \n/load:   {Load Recipes} \n/help:   {Help} \n/quit:   {Quit Program} \n");
                            break;
                        case "/quit":
                            quit = false;
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid selection. Please try again.");
                            break;
                        
                    }
                
            }
        }
    }


    class Program
    {
        static void Main()
        {
            RecipeManager manager = new RecipeManager();
            manager.MainMenu();
        }
    }


}