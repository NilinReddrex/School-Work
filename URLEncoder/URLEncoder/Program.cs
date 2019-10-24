using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLEncoder
{
    class Program
    {
        const string UrlFormat = "https://companyserver.com/content/{0}/files/{1}/{1}Report.pdf";

        /// <summary>
        /// Console main method.
        /// </summary>
        /// <param name="args">Console arguments.</param>
        static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("What is the project name?");
                    string projectName = Console.ReadLine();

                    Console.WriteLine("What is the activity name?");
                    string activityName = Console.ReadLine();

                    WebAddress webAddress = new WebAddress(UrlFormat, new string[] { projectName, activityName });
                    Console.WriteLine(webAddress.GetEncodedURL());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                 
                PromptUserToContinue();
            }
        }                                  
                     
        /// <summary>
        /// Asks user if they want to continue.
        /// </summary>
        static void PromptUserToContinue()
        {
            while(true)
            {
                Console.WriteLine("Would you like to do another? (y/n):");
                string response = Console.ReadLine();
                if (response.ToLower() == "n")
                {
                    Environment.Exit(0);
                }
                else if (response.ToLower() == "y")
                {
                    Console.Clear();
                    break;
                }
            }
        }
    }
}




        



