using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatsForDinner.Models
{
    [Serializable]
    public class RecipeComponent
    {
        private string name;

        public RecipeComponent(string name)
        {
            this.name = name;
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
    }
}
