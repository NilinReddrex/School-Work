using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatsForDinner.Models
{
    [Serializable]
    public class Recipe
    {
        private readonly ObservableCollection<QuantifiedIngredient> ingredients = new ObservableCollection<QuantifiedIngredient>();
        private readonly ObservableCollection<Apparatus> tools = new ObservableCollection<Apparatus>();

        public Recipe(string name)
        {
            Name = name;
        }

        public string Name
        {
            get;
            set;


        }

        public string Procedure
        {
            get;
            set;
        }

        public ObservableCollection<QuantifiedIngredient> Ingredients
        {
            get
            {
                return this.ingredients;
            }
        }

        public ObservableCollection<Apparatus> Tools
        {
            get
            {
                return this.tools;
            }
        }
    }
}
