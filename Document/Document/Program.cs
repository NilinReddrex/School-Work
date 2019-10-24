using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                HandleFileCreation();
                PromptUserToContinue();
            }
        }

   
        static void HandleFileCreation()
        {
            try
            {
                StreamWriter sw = null;
                string documentName = null;
                string content = null;

                try
                {
                    Console.WriteLine("Document\n");
                    Console.WriteLine("What is the name of the document?");

                    documentName = Console.ReadLine();
                    if (!documentName.EndsWith(".txt"))
                    {
                        documentName = documentName + ".txt";
                    }

                    Console.WriteLine("What content is to be in the document?");

                    content = Console.ReadLine();
                    sw = new StreamWriter(documentName);

                    //Write a line of text
                    sw.WriteLine(content);

                    Console.WriteLine("{0} was successfully saved. The document contains {1} characters.\n", documentName, content.Length);
                }
                finally
                {
                    if (sw != null)
                    {
                        //Close the file
                        sw.Close();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }

        static void PromptUserToContinue()
        {
            while (true)
            {
                Console.WriteLine("Do you want to save another document? Please respond with Yes or No.");
                string response = Console.ReadLine();
                if (response.ToLower() == "no")
                {
                    Environment.Exit(0);
                }
                else if (response.ToLower() == "yes")
                {
                    Console.Clear();
                    break;
                }
            }
        }

    }
}
