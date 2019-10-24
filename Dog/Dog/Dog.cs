using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dog
{
    class Dog
    {
        public enum Gender { Male, Female};
        public string name;
        public string owner;
        public int age;
        public Gender gender;

        public Dog(string name, string owner, int age, Gender gender)
        {
            this.name = name;
            this.owner = owner;
            this.age = age;
            this.gender = gender;
        }

        public void Bark(int numBarks)
        {
            string woofs = "";
            for (int i = 0; i < numBarks; i++)
            {
                woofs = woofs + "Woof!";
            }
                Console.WriteLine(woofs);
        }

        public string GetTag()
        {
            string hisHer;
            string heShe;
            if (this.gender == Gender.Female)
            {
                hisHer = "Her";
                heShe = "she";
            }
            else
            {
                hisHer = "His";
                heShe = "he";
            }
            string year;
            if (this.age >= 2 || this.age == 0)
            {
                year = "years";
            }
            else
            {
                year = "year";
            }
            return string.Format("If lost, call {0}. {1} name is {2} and {3} is {4} {5} old.",
                this.owner, hisHer, this.name, heShe, age, year);

        }
    }
}
