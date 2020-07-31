using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatsForDinner.Models
{
    [Serializable]
    public class Ingredient : RecipeComponent
    {
        public Ingredient(string name) : base(name)
        {
        }

       
    }
}
