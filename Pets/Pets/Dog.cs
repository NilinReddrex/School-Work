using System;
using System.Collections.Generic;
using System.Text;

namespace Pets
{
    public class Dog : Pet
    {
        public Dog(string name, string owner, double weight) : base("Dog", name, owner, weight)
        {

        }
        public string bark(int count)
        {
            string bark = "";
            for (int i = 0; i < count; i++)
            {
                bark = bark + "Bark!";
            }
            return bark;
        }
    }
}
