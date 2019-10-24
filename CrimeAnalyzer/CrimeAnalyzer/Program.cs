using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimeAnalyzer
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ValidateInputs(args);

                string inputCsvFilePath = args[0];
                string outputReportPath = args[1];

                IEnumerable<string> readLines = ReadInputFile(inputCsvFilePath);
                
                Dictionary<string, int> headerIndexes = CreateHeaderLines(readLines);
                List<CrimeStats> crimeStatsList = ParseCrimeStatsList(readLines, headerIndexes);
                List<string> outputContent = BuildOutPutReport(crimeStatsList);

                WriteOutputToFile(outputReportPath, outputContent);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void WriteOutputToFile(string outputReportPath, List<string> outputContent)
        {
            try
            {
                File.WriteAllLines(outputReportPath, outputContent);
                Console.WriteLine("Successfully wrote report into file {0}", outputReportPath);
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to write to output file.");
            }
        }

        private static List<string> BuildOutPutReport(List<CrimeStats> crimeStatsList)
        {
            List<string> outputContent = new List<string>();
            // 1. What is the range of years included in the data?
            var lower = crimeStatsList.Min(crimeStat => crimeStat.Year);
            var upper = crimeStatsList.Max(crimeStat => crimeStat.Year);

            // 2. How many years of data are included?
            var count = crimeStatsList.Select(crimeStat => crimeStat.Year).Distinct().Count();
            outputContent.Add("Crime Analyzer Report");
            outputContent.Add("");
            outputContent.Add($"Period: {lower} - {upper} ({count} years)");
            outputContent.Add("");

            // 3. What years is the number of murders per year less than 15000?
            var years = from crimeStats in crimeStatsList where crimeStats.Murder < 15000 select crimeStats.Year;
            outputContent.Add($"Years murders per year < 15000: {string.Join(", ", years)}");

            // 4. What are the years and associated robberies per year for years where the number of robberies is greater than 500000 ?
            string robberyOutput = "Robberies per year > 500000: ";
            var roberries = crimeStatsList.Where(crimeStat => crimeStat.Robbery > 500000).Select(crimeStat => crimeStat.Year + " = " + crimeStat.Robbery).ToList();
            robberyOutput = string.Concat(robberyOutput, String.Join(", ", roberries));
            outputContent.Add(robberyOutput);

            // 5. What is the violent crime per capita rate for 2010 ? Per capita rate is the number of violent crimes in a year divided by the size of the population that year.
            var crimeStat10 = crimeStatsList.First(crimeStat => crimeStat.Year == 2010);
            var violentCrimePerCapitaRate10 = Convert.ToDouble(crimeStat10.ViolentCrime) / Convert.ToDouble(crimeStat10.Population);
            string result = $"Violent crime per capita rate (2010): {violentCrimePerCapitaRate10}";
            outputContent.Add(result);

            // 6. What is the average number of murders per year across all years ?
            var avg = crimeStatsList.Average(crimeStat => crimeStat.Murder);
            outputContent.Add($"Average murder per year(all years):{avg}");

            // 7. What is the average number of murders per year for 1994 to 1997 ?
            var avg94To97 = crimeStatsList.Where(crimeStat => crimeStat.Year >= 1994 && crimeStat.Year <= 1997).Average(crimeStat => crimeStat.Murder);
            outputContent.Add($"Average murder per year (1994–1997):{avg94To97}");

            // 8. What is the average number of murders per year for 2010 to 2013 
            var avg10To13 = crimeStatsList.Where(crimeStat => crimeStat.Year >= 2010 && crimeStat.Year <= 2013).Average(crimeStat => crimeStat.Murder);
            outputContent.Add($"Average murder per year (2010–2013): {avg10To13}");

            // 9. What is the minimum number of thefts per year for 1999 to 2004 ?
            var minNumOfThefts = crimeStatsList.Where(crimeStat => crimeStat.Year >= 1999 && crimeStat.Year <= 2004).Min(crimeStat => crimeStat.Theft);
            outputContent.Add($"Minimum thefts per year (1999–2004): {minNumOfThefts}");

            // 10. What is the maximum number of thefts per year for 1999 to 2004 ?
            var maxNumOfThefts = crimeStatsList.Where(crimeStat => crimeStat.Year >= 1999 && crimeStat.Year <= 2004).Max(crimeStat => crimeStat.Theft);
            outputContent.Add($"Maximum thefts per year (1999–2004): {maxNumOfThefts}");

            // 11. What year had the highest number of motor vehicle thefts ?
            var highestMotorVehicleTheft = crimeStatsList.Max(crimestat => crimestat.MotorVehicleTheft);
            var yearHighestMotorVehicleTheft = crimeStatsList.Where(crimestat => crimestat.MotorVehicleTheft == highestMotorVehicleTheft).Select(crimestat => crimestat.Year).First();
            outputContent.Add($"Year of highest number of motor vehicle thefts: {yearHighestMotorVehicleTheft}");

            return outputContent;
        }

        private static List<CrimeStats> ParseCrimeStatsList(IEnumerable<string> readLines, Dictionary<string, int> headerIndexes)
        {
            List<CrimeStats> crimeStatsList = new List<CrimeStats>();

            for (int i = 1; i < readLines.Count(); i++)
            {
                string readLine = readLines.ElementAt(i);
                try
                {
                    CrimeStats c = CrimeStats.BuildFromReadData(headerIndexes, readLine);
                    crimeStatsList.Add(c);



                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to build record from line {0} from input file.", i);
                    Console.WriteLine(e.Message);
                    Environment.Exit(1);

                }
            }

            return crimeStatsList;
        }

        private static Dictionary<string, int> CreateHeaderLines(IEnumerable<string> readLines)
        {
            string headerLine = readLines.First();

            string[] headers = headerLine.Split(new char[] { ',' });
            Dictionary<string, int> headerIndexes = new Dictionary<string, int>();

            for (int i = 0; i < headers.Length; i++)
            {
                headerIndexes.Add(headers[i], i);
            }

            return headerIndexes;
        }

        private static IEnumerable<string> ReadInputFile(string inputCsvFilePath)
        {
            try
            {
                return File.ReadAllLines(inputCsvFilePath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Environment.Exit(1);
            }

            return null;
        }

        private static void ValidateInputs(string[] args)
        {
            if (args.Length < 2 || args.Length > 2)
            {
                Console.WriteLine("You didn't give me 2 paths. Usage:");
                Console.WriteLine("CrimeAnalyzer <crime_csv_file_path> <report_file_path>");
                Environment.Exit(1);
            }
        }
    }
}
