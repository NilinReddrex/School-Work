using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatsForDinner.Models
{
    [Serializable]
    public class QuantifiedIngredient : Ingredient
    {
        public QuantifiedIngredient(string name) : base(name)
        {

        }

        public string Quantity { get; set; }

        public string Units { get; set; }
    }
}
