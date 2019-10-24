using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentMerger
{
    class Program
    {
        const string fileExt = ".txt";

        static void Main(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("DocumentMerger2 <input_file_1> <input_file_2> ... <input_file_n> <output_file>");
                Console.WriteLine("Supply a list of text files to merge followed by the name of the resulting merged text file as command line arguments.");
                Console.ReadLine();
                Environment.Exit(1);
            }

            List<string> documentNames = new List<string>();

            for (int i = 0; i < args.Length - 1; i++)
            {
                documentNames.Add(args[i]);
                
            }
           

            MergeGivenFilesAndNames(documentNames, args.Last());

            if (false)
            {
                ProvideUserAidedExperience();
            }
        }

        static string AttachFileExt(string input)
        {
            if (!input.EndsWith(fileExt))
            {
                input = input + fileExt;
            }

            return input;
        }

        static string RemoveDocExtension(string document)
        {
            if (document.EndsWith(fileExt))
            {
                document = document.Remove(document.LastIndexOf(fileExt));
            }

            return document;
        }

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

        static void PromptUserToContinue()
        {
            while (true)
            {
                Console.WriteLine("Do you want to merge two more files? Please respond with Yes or No.");
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

        static void ProvideUserAidedExperience()
        {
            while (true)
            {
                try
                {
                    string document = null;
                    List<string> documentsNames = new List<string>();

                    Console.WriteLine("Document Merger\n");

                    //Ask user for multiple files to merge.
                    for (int i = 1; true; i++)
                    {
                        bool isRequired = i < 3;
                        Console.WriteLine("What is the name of document #{0} to merge?{1}", i, isRequired ? "" : " (or press Enter to merge)");
                        document = PromptUserForInputFile(isRequired);

                        if (document == "")
                        {
                            break;
                        }
                        else
                        {
                            documentsNames.Add(document);
                        }
                    }

                    MergeGivenFilesAndNames(documentsNames);
                    PromptUserToContinue();

                }
                catch (Exception e)
                {
                    // If an exception is thrown, write out the message and die.
                    Console.WriteLine(e.Message);
                    return;
                }
            }
        }

        private static void MergeGivenFilesAndNames(List<string> documentsNames, string outputFileName = null)
        {
            // Read all files text into a single variable.
            string fileContents = "";
            foreach (string documentname in documentsNames)
            {
                fileContents += File.ReadAllText(AttachFileExt(documentname));
            }

            //Ask user for output file name or use concatenation of all input file names.
            if(outputFileName == null)
            {
                Console.WriteLine("Enter an output name or press enter for default name.");

                outputFileName = Console.ReadLine();

            }

            if (outputFileName == "")
            {
                outputFileName = string.Concat(documentsNames);
            }

            //Write output file and ask user if they want to continue.
            outputFileName = AttachFileExt(outputFileName);
            File.WriteAllText(outputFileName, fileContents);
            Console.WriteLine("{0} was successfully saved.The document contains {1} characters.", outputFileName, fileContents.Length);
        }
    }
}   
   
