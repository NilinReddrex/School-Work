using System;
using System.Collections.Generic;
using System.Text;

namespace Pets
{
    public class Pet
    {
        public string type;
        public string name;
        public string owner;
        public double weight;

        public Pet(string type, string name, string owner, double weight)
        {
            this.type = type;
            this.name = name;
            this.owner = owner;
            this.weight = weight;
        }

        public string getTag()
        {
            return string.Format("If lost, call {0}", this.owner);
        }

    }   

}
