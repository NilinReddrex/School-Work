using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrimeAnalyzer
{
    public class CrimeStats
    {
        public CrimeStats(int year, int population, int violentCrime, int murder, int rape, int robbery, int aggravatedAssault, int propertyCrime, int burglary, int theft, int motorVehicleTheft)
        {
            this.Year = year;
            this.Population = population;
            this.ViolentCrime = violentCrime;
            this.Murder = murder;
            this.Rape = rape;
            this.Robbery = robbery;
            this.AggravatedAssault = aggravatedAssault;
            this.PropertyCrime = propertyCrime;
            this.Burglary = burglary;
            this.Theft = theft;
            this.MotorVehicleTheft = motorVehicleTheft;
        }

        public int Year
        {
            get;
            set;
        }

        public int Population
        {
            get;
            set;
        }

        public int ViolentCrime
        {
            get;
            set;
        }

        public int Murder
        {
            get;
            set;
        }

        public int Rape
        {
            get;
            set;
        }

        public int Robbery
        {
            get;
            set;
        }

        public int AggravatedAssault
        {
            get;
            set;
        }

        public int PropertyCrime
        {
            get;
            set;
        }

        public int Burglary
        {
            get;
            set;
        }

        public int Theft
        {
            get;
            set;
        }

        public int MotorVehicleTheft
        {
            get;
            set;
        }

        public static CrimeStats BuildFromReadData(Dictionary<string, int> headerLocations, string readString)
        {
            string[] properties = readString.Split(new char[] { ',' });
            return new CrimeStats(
                ConvertToIntIfValid(properties[headerLocations["Year"]]),
                ConvertToIntIfValid(properties[headerLocations["Population"]]),
                ConvertToIntIfValid(properties[headerLocations["Violent Crime"]]),
                ConvertToIntIfValid(properties[headerLocations["Murder"]]),
                ConvertToIntIfValid(properties[headerLocations["Rape"]]),
                ConvertToIntIfValid(properties[headerLocations["Robbery"]]),
                ConvertToIntIfValid(properties[headerLocations["Aggravated Assault"]]),
                ConvertToIntIfValid(properties[headerLocations["Property Crime"]]),
                ConvertToIntIfValid(properties[headerLocations["Burglary"]]),
                ConvertToIntIfValid(properties[headerLocations["Theft"]]),
                ConvertToIntIfValid(properties[headerLocations["Motor Vehicle Theft"]]));
        }

        public static int ConvertToIntIfValid(string inputString)
        {
            return string.IsNullOrWhiteSpace(inputString) ? 0 : Convert.ToInt32(inputString);
        }
    }
}
