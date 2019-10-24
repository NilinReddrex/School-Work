using System;
using System.Collections.Generic;
using System.Text;

namespace Pets
{
    public class Cat : Pet
    {
        public Cat(string name, string owner, double weight) : base("Cat", name, owner, weight)
        {

        }

        public string meow(int count)
        {
            string meow = "";
            for (int i = 0; i < count; i++)
            {
                meow = meow + "meow.";
            }
            return meow;
        }
    }
}

