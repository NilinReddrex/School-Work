using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatsForDinner.Models
{
    [Serializable]
    public class ViewModel
    {
        private readonly ObservableCollection<Ingredient> allIngredients = new ObservableCollection<Ingredient>();
        private readonly ObservableCollection<Apparatus> allTools = new ObservableCollection<Apparatus>();
        private readonly ObservableCollection<Recipe> recipes = new ObservableCollection<Recipe>();

        public ObservableCollection<Ingredient> AllIngredients
        {
            get
            {
                return this.allIngredients;
            }
        }

        public ObservableCollection<Apparatus> AllTools
        {
            get
            {
                return this.allTools;
            }
        }
        public ObservableCollection<Recipe> Recipes
        {
            get
            {
            return this.recipes;
            }
        }

    }
}
