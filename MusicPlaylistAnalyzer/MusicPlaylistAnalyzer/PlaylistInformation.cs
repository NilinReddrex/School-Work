using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlaylistAnalyzer
{
    class PlaylistInformation
    {
        public PlaylistInformation(string name, string artist, string album, string genre, int size, int time, int year, int plays)
        {
            this.Name = name;
            this.Artist = artist;
            this.Album = album;
            this.Genre = genre;
            this.Size = size;
            this.Time = time;
            this.Year = year;
            this.Plays = plays;
        }

        public string Name
        {
            get;
            set;
        }

        public string Artist
        {
            get;
            set;
        }

        public string Album
        {
            get;
            set;
        }

        public string Genre
        {
            get;
            set;
        }

        public int Size
        {
            get;
            set;
        }

        public int Time
        {
            get;
            set;
        }

        public int Year
        {
            get;
            set;
        }

        public int Plays
        {
            get;
            set;
        }

        public static PlaylistInformation BuildFromReadData(Dictionary<string, int> headerLocations, string readString)
        {
            string[] properties = readString.Split(new char[] {'\t'});
            return new PlaylistInformation(
                (properties[headerLocations["Name"]]),
                (properties[headerLocations["Artist"]]),
                (properties[headerLocations["Album"]]),
                (properties[headerLocations["Genre"]]),
                ConvertToIntIfValid(properties[headerLocations["Size"]]),
                ConvertToIntIfValid(properties[headerLocations["Time"]]),
                ConvertToIntIfValid(properties[headerLocations["Year"]]),
                ConvertToIntIfValid(properties[headerLocations["Plays"]]));  
        }

        public static int ConvertToIntIfValid(string inputString)
        {
            return string.IsNullOrWhiteSpace(inputString) ? 0 : Convert.ToInt32(inputString);
        }

        public override string ToString()
        {
            return $"Name: {this.Name}, Artist: {this.Artist}, Album: {this.Album}, Genre: {this.Genre}, Size: {this.Size}, Time: {this.Time}, Year: {this.Year}, Plays: {this.Plays}";
        }
    }
}

