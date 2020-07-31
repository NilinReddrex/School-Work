using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatsForDinner.Models
{
    [Serializable]
    public class Apparatus : RecipeComponent
    {
        public Apparatus(string name) : base(name)
        {
        }


    }
}
