using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dog
{
    class Program
    {
        static void Main(string[] args)
        {
            Dog dog = new Dog("Orion", "Shawn", 1, Dog.Gender.Male);
            dog.Bark(3);
            Console.WriteLine(dog.GetTag());

            Dog myDog = new Dog("Lileu", "Dale", 4, Dog.Gender.Female); 
            myDog.Bark(1); 
            Console.WriteLine(myDog.GetTag()); 
        }
    }
}
