## Programming History

A few languages I can code in are:

- C++
- C#
- Python
- Java Script

C++ was the first programming language I learned. I took a course in it while attending OTC and discovered my interest for programming. Some of my work in a couple of the languages I have worked in are below.

### Python Code
```Python
while True:
    x_intial = float(input("Enter the object's initial position: "))
    v_intial = float(input("Enter the object's initial velocity: "))
    acceleration = float(input("Enter the object's acceleration: "))
    time_passsed = float(input("Enter the amount of time passed: "))
    finalPosition = x_intial + v_intial * time_passsed + 0.5 * acceleration * time_passsed ** 2
    print(finalPosition)
    cont = input("Another one? yes/no > ")
    while cont.lower() not in ("yes","no"):
        cont = input("Another one? yes/no > ")
    if cont == "no":
        break
```

C# Code

```C#
  static string PromptUserForInputFile(bool required = false)
        {
            string document = "";
            bool fileExists = false;
            do
            {
                document = Console.ReadLine();
                document = RemoveDocExtension(document);
                if (!required && document == "")
                {
                    break;
                }

                fileExists = File.Exists(document + fileExt);
                if (!fileExists)
                {
                    Console.WriteLine("The file doesn't exist. Try again.");
                }
            }
            while (!fileExists);

            return document;
        }
```

[Back to Home](README.md) [Previous Page](VideoGames.md) [Next Page](Hiking.md)