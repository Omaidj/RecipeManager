using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeManager
{
    public class Recipe
    {
        public string Name { get; set; }
        public string Ingredients { get; set; }
        public string Instructions { get; set; }
        public string MealType { get; set; }
        public string Category { get; set; }

        public Recipe(string name, string ingredients, string instructions, string mealtype, string category)
        {
            Name = name;
            Ingredients = ingredients;
            Instructions = instructions;
            MealType = mealtype;
            Category = category;
        }
    }
}
